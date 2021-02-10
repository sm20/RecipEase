using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AspDotNetMySqlTemplate.Data;
using AspDotNetMySqlTemplate.Models;

namespace AspDotNetMySqlTemplate.Pages.Examples
{
    public class DeleteModel : PageModel
    {
        private readonly AspDotNetMySqlTemplate.Data.ExampleContext _context;

        public DeleteModel(AspDotNetMySqlTemplate.Data.ExampleContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Example Example { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Example = await _context.Example.FirstOrDefaultAsync(m => m.ID == id);

            if (Example == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Example = await _context.Example.FindAsync(id);

            if (Example != null)
            {
                _context.Example.Remove(Example);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
