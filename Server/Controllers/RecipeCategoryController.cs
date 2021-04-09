using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipEase.Server.Data;
using RecipEase.Shared.Models.Api;

namespace RecipEase.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    public class RecipeCategoryController : ControllerBase
    {
        private readonly RecipEaseContext _context;

        public RecipeCategoryController(RecipEaseContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Get all recipe categories
        /// </summary>
        /// <remarks>
        ///
        /// Returns a list of all recipe categories in the database.
        ///
        /// This endpoint interacts with all attributes in the `recipecategory` table.
        ///
        /// The endpoint performs a `select *` query.
        ///
        /// </remarks>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiRecipeCategory>>> GetRecipeCategories()
        {
            return await _context.RecipeCategory.Select(category => category.ToApiRecipeCategory()).ToListAsync();
        }
    }
}
