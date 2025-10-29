using GrpcClientProject.Domain.Models;
using GrpcClientProject.Domain.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GrpcClientProject.WebApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;
        internal IEnumerable<StudentModel> Students { get; set; }

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task OnGetAsync()
        {
            Students = await _studentService.GetAllAsync(HttpContext.RequestAborted);
        }
    }
}
