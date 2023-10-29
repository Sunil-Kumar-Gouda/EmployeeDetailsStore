using System;
using System.Threading.Tasks;
using EmployeeDetailStore.Model;
using EmployeeDetailStore.Repository.Interfaces;
using EmployeeDetailStore.Utilities;
using EmployeeDetailStore.Utilities.Mapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeDetailStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _employeeService.GetAllEmployeesAsync();
                var employees = result.ConvertAll(x => EmployeeMapper.MapDbToDto(x));
                return Ok(employees);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id:int}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _employeeService.GetEmployeeByIdAsync(id);
                var employee = EmployeeMapper.MapDbToDto(result);
                return Ok(employee);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return NotFound($"Employee with employee id - {id} not found");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return BadRequest(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }

        // GET: api/Employee/5
        [HttpGet("{searchValue}", Name = "Search")]
        public async Task<IActionResult> Get([FromRoute]string searchValue)
        {
            try
            {
                var result = await _employeeService.GetEmployeeBySearchValueAsync(searchValue);
                if (result == null)
                    return NotFound("No employee meet the search criteria.");

                var employee = result.ConvertAll(x => EmployeeMapper.MapDbToDto(x));
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }

        // POST: api/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            try
            {
                await _employeeService.CreateEmployeeAsync(EmployeeMapper.MapDtoToDb(employee));
                return Ok();
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return BadRequest("Invalid data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }

        // PUT: api/Employee/5
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Employee employee)
        {
            try
            {
                await _employeeService.UpdateEmployeeAsync(EmployeeMapper.MapDtoToDb(employee));
                return Ok();
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return NotFound($"Employee with employee id - {employee.EmployeeId} not found");
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return BadRequest("Invalid employee");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }

        // DELETE: api/Employee/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);
                return Ok("Successfully deleted.");
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, nameof(Get));
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(Get));
                return StatusCode(500);
            }
        }
    }
}