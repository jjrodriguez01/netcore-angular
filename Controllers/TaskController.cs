using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spa.Data.Repository;

namespace spa.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ItaskRepository _taskRepository;

        public TaskController(ItaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("by-department/{departmentId}")]
        public async Task<IActionResult> Get(int departmentId)
        {
            var result = await _taskRepository.GetByDepartmentIdAsync(departmentId);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var ts = await _taskRepository.GetByIdAsync(id);
            if(ts == null){
                return NotFound($"No se encontro entidad id {id}");
            }
            return Ok(ts);
        }
    }
}