using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using spa.Models;

namespace spa.Data.Repository
{
    public interface ITimeslipRepository
    {
        Task<int> SaveAsync();
        Task<Timeslip?> GetByIdAsync(int id);
        Timeslip Insert(Timeslip timeslip);
        Timeslip Update(Timeslip timeslip);
        void Delete(Timeslip timeslip);
        void DeleteById(int id);
        Task<List<Timeslip>> GetAllAsync();
    }
}