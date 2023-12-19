using Microsoft.EntityFrameworkCore;

namespace AntAPIDb.Data
{
    public class DataInitializer
    {
        private readonly Context _context;
        public DataInitializer(Context context)
        {
            _context = context;
        }

        public async Task SeedData()
        {
            await _context.Database.MigrateAsync();
        }
    }
}
