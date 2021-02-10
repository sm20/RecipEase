using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AspDotNetMySqlTemplate.Data;
using AspDotNetMySqlTemplate.Models;

namespace AspDotNetMySqlTemplate.Pages.Examples
{
    public class CreateModel : PageModel
    {
        private readonly AspDotNetMySqlTemplate.Data.ExampleContext _context;

        public CreateModel(AspDotNetMySqlTemplate.Data.ExampleContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Example Example { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Example.Add(Example);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
