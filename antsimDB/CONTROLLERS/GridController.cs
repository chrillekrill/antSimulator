using $safeprojectname$.Data;
using $safeprojectname$.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace $safeprojectname$.Controllers
{
    [ApiController]
    public class GridController : Controller
    {
        private readonly Context _context;
        public GridController(Context context)
        {
            _context = context;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> UploadGrid([FromBody] int[] flatGrid)
        {
            var anyGridExists = await _context.Grid.AnyAsync();

            if (anyGridExists)
            {
                var existingGrid = await _context.Grid.FirstAsync();
                return Ok(existingGrid.FlatGrid);
            }

            var gridModel = new GridModel
            {
                FlatGrid = flatGrid,
            };

            await _context.Grid.AddAsync(gridModel);
            await _context.SaveChangesAsync();

            return Ok("File uploaded successfully!");
        }
        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> GetGrid()
        {
            var grid = await _context.Grid.FirstOrDefaultAsync();

            if(grid == null)
            {
                return NotFound();
            }

            var gridModel = new GridModel
            {
                FlatGrid = grid.FlatGrid
            };

            return Ok(gridModel.FlatGrid);
        }
    }
}
