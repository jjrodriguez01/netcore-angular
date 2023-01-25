using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using spa.Models;
using Microsoft.EntityFrameworkCore;

namespace spa.Data.Repository
{
    public class TimeslipRepository : ITimeslipRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeslipRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Timeslip>> GetAllAsync()
        {
            return _context.Timeslips
                .Include(x => x.ApplicationUser)
                .Include(x => x.Task)
                    .ThenInclude(x => x!.Department)
                .ToListAsync();
        }

        public Task<Timeslip?> GetByIdAsync(int id)
        {
            return _context.Timeslips.Include(x => x.ApplicationUser).Where(x => x.TimeslipId== id).FirstOrDefaultAsync();
        }

        public Timeslip Insert(Timeslip timeslip)
        {
            return _context.Timeslips.Add(timeslip).Entity;
        }

        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Timeslip Update(Timeslip timeslip)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Timeslip timeslip){
            _context.Timeslips.Remove(timeslip);
        }
    }
}