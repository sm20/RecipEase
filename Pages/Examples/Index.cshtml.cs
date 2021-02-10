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
    public class IndexModel : PageModel
    {
        private readonly AspDotNetMySqlTemplate.Data.ExampleContext _context;

        public IndexModel(AspDotNetMySqlTemplate.Data.ExampleContext context)
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
