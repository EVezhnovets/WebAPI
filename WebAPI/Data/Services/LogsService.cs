using WebAPI.Data.Models;

namespace WebAPI.Data.Services
{
    public class LogsService
    {
        private AppDbContext _context;

        public LogsService(AppDbContext context)
        {
            _context = context;
        }

        public List<Log> GetAllLogsFromDb() => _context.Logs.ToList();
    }
}