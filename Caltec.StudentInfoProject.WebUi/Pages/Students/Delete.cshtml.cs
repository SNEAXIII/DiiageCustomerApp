using Microsoft.AspNetCore.Mvc;
using Caltec.StudentInfoProject.Business;
using Caltec.StudentInfoProject.Business.Dto;

namespace Caltec.StudentInfoProject.WebUi.Pages.Students
{
    public class DeleteModel : StudentModelBase
    {
        public DeleteModel(StudentService service) : base(service)
        {

        }

        [BindProperty]
        public StudentDto Student { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _service.GetOne(id.Value, CancellationToken.None);

            Student = student;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null )
            {
                return NotFound();
            }
            await _service.DeleteStudentAsync(id.Value, CancellationToken.None);

            return RedirectToPage("./Index");
        }
    }
}
