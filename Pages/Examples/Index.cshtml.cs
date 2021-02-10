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
    public class IndexModel : PageModel
    {
        private readonly RecipEase.Data.ExampleContext _context;

        public IndexModel(RecipEase.Data.ExampleContext context)
        {
            _context = context;
        }

        public IList<Example> Example { get;set; }

        public async Task OnGetAsync()
        {
            Example = await _context.Example.ToListAsync();
        }
    }
}
