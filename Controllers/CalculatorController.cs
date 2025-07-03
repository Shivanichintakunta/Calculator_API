using CalculationAPI.Data;
using CalculationAPI.Models;
using CalculatorWebAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task<ActionResult<Calculation>> Add(double a, double b)
        {
            var calc = new Calculation { Operand1 = a, Operand2 = b, Operator = "+", Result = a + b };
            _context.Calculations.Add(calc);
            await _context.SaveChangesAsync();
            return Ok(calc);
        }

        [HttpPost("subtract")]
        public async Task<ActionResult<Calculation>> Subtract(double a, double b)
        {
            var calc = new Calculation { Operand1 = a, Operand2 = b, Operator = "-", Result = a - b };
            _context.Calculations.Add(calc);
            await _context.SaveChangesAsync();
            return Ok(calc);
        }

        [HttpPost("multiply")]
        public async Task<ActionResult<Calculation>> Multiply(double a, double b)
        {
            var calc = new Calculation { Operand1 = a, Operand2 = b, Operator = "*", Result = a * b };
            _context.Calculations.Add(calc);
            await _context.SaveChangesAsync();
            return Ok(calc);
        }

        [HttpPost("divide")]
        public async Task<ActionResult<Calculation>> Divide(double a, double b)
        {
            if (b == 0) return BadRequest("Cannot divide by zero.");
            var calc = new Calculation { Operand1 = a, Operand2 = b, Operator = "/", Result = a / b };
            _context.Calculations.Add(calc);
            await _context.SaveChangesAsync();
            return Ok(calc);
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





