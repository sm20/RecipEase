using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RecipEase.Data;
using RecipEase.Models;

namespace RecipEase.Pages.Examples
{
    public class DetailsModel : PageModel
    {
        private readonly RecipEase.Data.RecipEaseContext _context;

        public DetailsModel(RecipEase.Data.RecipEaseContext context)
        {
            _context = context;
        }

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
    }
}
