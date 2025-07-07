using CalculationAPI.CalculationDTO;
using CalculationAPI.Data;
using CalculationAPI.Models;
using CalculatorWebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CalculationAPI.CalculationDTO;

namespace CalculatorWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorsController : ControllerBase
    {
        private readonly CalculatorContext _context;

        public CalculatorsController(CalculatorContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public IActionResult Addition([FromBody] CalculationRequest request)
        {
            double result = request.Firstnum + request.Secondnum;

            var calculation = new Calculation
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "+",
                Result = result
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "+",
                Result = result
            });
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] CalculationRequest request)
        {
            double result = request.Firstnum - request.Secondnum;

            var calculation = new Calculation
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "-",
                Result = result
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "-",
                Result = result
            });
        }

        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] CalculationRequest request)
        {
            double result = request.Firstnum * request.Secondnum;

            var calculation = new Calculation
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "*",
                Result = result
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "*",
                Result = result
            });
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] CalculationRequest request)
        {
            double result = request.Firstnum / request.Secondnum;

            var calculation = new Calculation
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "/",
                Result = result
            };

            _context.Calculations.Add(calculation);
            _context.SaveChanges();

            return Ok(new
            {
                Operand1 = request.Firstnum,
                Operand2 = request.Secondnum,
                Operator = "/",
                Result = result
            });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Calculation>>> GetAll()
        {
            return await _context.Calculations.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Calculation>> Get(int id)
        {
            var calc = await _context.Calculations.FindAsync(id);
            if (calc == null) return NotFound();
            return calc;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Calculation calculator)
        {
            if (id != calculator.Id) return BadRequest();
            _context.Entry(calculator).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var calc = await _context.Calculations.FindAsync(id);
            if (calc == null) return NotFound();
            _context.Calculations.Remove(calc);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}





