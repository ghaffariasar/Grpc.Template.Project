using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClientProject.WebApp.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<EditModel> _logger;

        [BindProperty]
        public StudentUpdateModel Input { get; set; }

        public EditModel(IStudentService studentService, ILogger<EditModel> logger)
        {
            _studentService = studentService;
            _logger = logger;
            Input = new StudentUpdateModel
            {
                Id = 0,
                FirstName = string.Empty,
                LastName = string.Empty,
                Description = null
            };
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var student = await _studentService.GetByIdAsync(id, HttpContext.RequestAborted);
            if (student == null)
            {
                TempData["ErrorMessage"] = "Student not found.";
                return RedirectToPage("Index");
            }

            Input = new StudentUpdateModel
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Description = student.Description
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var ok = await _studentService.UpdateAsync(Input, HttpContext.RequestAborted);
            if (ok)
            {
                TempData["SuccessMessage"] = "Student updated.";
            }
            else
            {
                TempData["ErrorMessage"] = "Update failed.";
            }
            return RedirectToPage("Index");
        }
    }
}


