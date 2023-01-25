using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using spa.Models;

namespace spa.Data.Repository
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
    }
}