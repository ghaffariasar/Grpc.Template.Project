using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace GrpcClientProject.WebApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly IStudentService _studentService;
        
        [BindProperty]
        internal IFormFile? StudentFile { get; set; }

        [BindProperty]
        public StudentCreateModel? Input { get; set; }


        public CreateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }


        public async Task<IActionResult> OnPostFileAsync()
        {
            if (StudentFile == null || StudentFile.Length == 0)
            {
                TempData["ErrorMessage"] = "No file selected.";
                return Page();
            }

            if (StudentFile.ContentType != "application/json" && !StudentFile.FileName.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Invalid file type. Please upload a JSON file.";
                return Page();
            }

            if (StudentFile.Length > 5 * 1024 * 1024)
            {
                TempData["ErrorMessage"] = "File is too large (max 5 MB).";
                return Page();
            }

            try
            {
                await using var stream = StudentFile.OpenReadStream();
                using var reader = new StreamReader(stream);
                var text = await reader.ReadToEndAsync();
                var students = JsonConvert.DeserializeObject<List<StudentCreateModel>>(text);

                if (students is { Count: > 0 })
                {
                    var ids = await _studentService.CreateAsync(students, HttpContext.RequestAborted);
                    TempData["SuccessMessage"] = $"{ids.Count} students created.";
                    return RedirectToPage("Index");
                }
                TempData["ErrorMessage"] = "No valid student records found in file.";
                return Page();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to process file: {ex.Message}";
                return Page();
            }
        }

        public async Task<IActionResult> OnPostManualAsync()
        {
            if (Input == null)
                return RedirectToPage("Index");

            // Handle comma separated phone numbers if provided via raw form field
            var phoneNumbersRaw = Request.Form["PhoneNumbers"].ToString();
            var numbers = string.IsNullOrWhiteSpace(phoneNumbersRaw)
                ? null
                : phoneNumbersRaw.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();

            var student = new StudentCreateModel
            {
                StudentNumber = Input.StudentNumber,
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Description = Input.Description,
                PhoneNumbers = numbers
            };

            var ids = await _studentService.CreateAsync(new List<StudentCreateModel> { student }, HttpContext.RequestAborted);
            TempData["SuccessMessage"] = ids.Count == 1 ? "Student created." : $"{ids.Count} students created.";
            return RedirectToPage("Index");
        }
    }
}
