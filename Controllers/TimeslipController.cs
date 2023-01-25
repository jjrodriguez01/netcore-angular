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
    public class TimeslipController : ControllerBase
    {
        private readonly ITimeslipRepository _timeslipRepository;

        public TimeslipController(ITimeslipRepository timeslipRepository)
        {
            _timeslipRepository = timeslipRepository;
        }
        // GET: api/<TimeslipController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _timeslipRepository.GetAllAsync();
            var dto = result.Select(x => new TimeslipViewDto{
                Comment = x.Comment,
                TimeslipId = x.TimeslipId,
                DateTime = x.DateTime,
                UserName = x.ApplicationUser?.Email,
                DayOfWeek = x.DateTime.DayOfWeek.ToString(),
                ShortDate = x.DateTime.ToShortDateString(),
                Hour = x.DateTime.ToShortTimeString(),
                TaskName = x.Task?.Name,
                DepartmentName = x.Task?.Department?.Name
            });
            return Ok(dto);
        }

        // GET api/<TimeslipController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ts = await _timeslipRepository.GetByIdAsync(id);
            if(ts == null){
                return NotFound($"No se encontro entidad id {id}");
            }
            return Ok(ts);
        }

        // POST api/<TimeslipController>
        [HttpPost]
        public async Task<IActionResult> Post(CreateTimeslipDto dto)
        {
            var ts = new Timeslip() 
            { 
                Comment= dto.Comment,
                DateTime = dto.DateTime,
                ApplicationUserId = dto.UserId,
                TaskId = dto.TaskId
            };
            var result = _timeslipRepository.Insert(ts);
            await _timeslipRepository.SaveAsync();
            return CreatedAtAction(nameof(Get),new {Id=result.TaskId},result);
        }

        // PUT api/<TimeslipController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CreateTimeslipDto dto)
        {
            if(id != dto.TimeslipId){
                return BadRequest("Los Id no coincideN");
            }  
            var ts = await _timeslipRepository.GetByIdAsync(id);
            if(ts == null){
                return NotFound($"No se encontro entidad id {id}");
            }
            ts.Comment = dto.Comment;
            ts.DateTime = dto.DateTime;
            ts.TaskId = dto.TaskId;
            ts.ApplicationUserId = dto.UserId;

            await _timeslipRepository.SaveAsync();
            return Ok(ts.TimeslipId);
        }

        // DELETE api/<TimeslipController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ts = await _timeslipRepository.GetByIdAsync(id);
            if(ts == null){
                return NotFound($"No se encontro entidad id {id}");
            }
            _timeslipRepository.Delete(ts);
            await _timeslipRepository.SaveAsync();
            return Ok(ts.TimeslipId);
        }
    }
}