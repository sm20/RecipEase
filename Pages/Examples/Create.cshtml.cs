using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecipEase.Data;
using RecipEase.Models;

namespace RecipEase.Pages.Examples
{
    public class CreateModel : PageModel
    {
        private readonly RecipEase.Data.RecipEaseContext _context;

        public CreateModel(RecipEase.Data.RecipEaseContext context)
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
