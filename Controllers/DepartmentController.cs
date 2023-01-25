using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spa.Data.Repository;
using spa.Models;
using spa.Models.Dto;

namespace spa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _departmentRepository.GetAllAsync();
            return Ok(result);
        }
    }
}