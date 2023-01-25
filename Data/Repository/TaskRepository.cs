using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace spa.Data.Repository
{
    public class TaskRepository : ItaskRepository
    {
        private readonly ApplicationDbContext _context;

        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Models.Task>> GetByDepartmentIdAsync(int departmentId)
        {
            return _context.Tasks.Where(x => x.DepartmentId == departmentId).ToListAsync();
        }

        public Task<Models.Task?> GetByIdAsync(int id)
        {
            return _context.Tasks.Include(x => x.Department).Where(x => x.TaskId== id).FirstOrDefaultAsync();
        }
    }
}