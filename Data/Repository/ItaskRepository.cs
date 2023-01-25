using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace spa.Data.Repository
{
    public interface ItaskRepository
    {
        Task<List<Models.Task>> GetByDepartmentIdAsync(int departmentId);
        Task<Models.Task?> GetByIdAsync(int id);
    }
}