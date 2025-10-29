using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClientProject.WebApp.Pages.Students
{
    public class DeleteModel : PageModel
    {
        private readonly IStudentService _studentService;
        private readonly ILogger<DeleteModel> _logger;

        [BindProperty]
        public int Id { get; set; }
        public StudentModel? Student { get; set; }

        public DeleteModel(IStudentService studentService, ILogger<DeleteModel> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;
            Student = await _studentService.GetByIdAsync(id, HttpContext.RequestAborted);
            if (Student == null)
                return RedirectToPage("Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Id <= 0)
                return RedirectToPage("Index");

            var ok = await _studentService.DeleteAsync(Id, HttpContext.RequestAborted);
            TempData[ok ? "SuccessMessage" : "ErrorMessage"] = ok ? "Student deleted." : "Delete failed.";
            return RedirectToPage("Index");
        }
    }
}


