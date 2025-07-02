using CalculationAPI.Data;
using CalculationAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CalculationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly CalculatorContext dbContext;

        public CalculatorController(CalculatorContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //Get Method
        [HttpGet]
        public IActionResult GetCalculations()
        {
            var allCalculations = dbContext.Calculations.ToList();
            return Ok(allCalculations);
        }

        //Get Method with Id
        [HttpGet]
        [Route("id:int")]
        public IActionResult GetCalculationsById(int id) {
            var Calculation_result = dbContext.Calculations.Find(id);
            if (Calculation_result is null)
            {
                return NotFound();
            }
            return Ok(Calculation_result);
        }

        //Post Method
        [HttpPost]
        public IActionResult PostCalculations([FromBody] Calculation c)
        {
            var CalculationEntity = new Calculation()
            {
                Operand1 = c.Operand1,
                Operand2 = c.Operand2,
                Operator = c.Operator
            };

            switch (CalculationEntity.Operator)
            {
                case "+":
                    CalculationEntity.Result = CalculationEntity.Operand1 + CalculationEntity.Operand2;
                    break;
                case "-":
                    CalculationEntity.Result = CalculationEntity.Operand1 - CalculationEntity.Operand2;
                    break;
                case "*":
                    CalculationEntity.Result = CalculationEntity.Operand1 * CalculationEntity.Operand2;
                    break;
                case "/":
                    if (CalculationEntity.Operand2 == 0)
                        return BadRequest("Cannot Divide By Zero");
                    CalculationEntity.Result = CalculationEntity.Operand1 / CalculationEntity.Operand2;
                    break;
                default:
                    return BadRequest("Invalid Operator.Use +, -, *, /. ");
            }
            dbContext.Calculations.Add(CalculationEntity);
            dbContext.SaveChanges();
            return Ok(CalculationEntity);
            //return CreatedAtAction(nameof(GetCalculations), new { Id = CalculationEntity.Id }, CalculationEntity);
        }

        //Put Method
        [HttpPut("{id}")]
        public IActionResult UpdateCalculation(int id,[FromBody] Calculation c) {
            var calculation_update = dbContext.Calculations.Find(id);
            if(calculation_update is null)
            {
                return NotFound();
            }
            calculation_update.Operand1 = c.Operand1;
            calculation_update.Operand2 = c.Operand2;
            calculation_update.Operator = c.Operator;

            switch (calculation_update.Operator)
            {
                case "+":
                    calculation_update.Result = calculation_update.Operand1 + calculation_update.Operand2;
                    break;
                case "-":
                    calculation_update.Result = calculation_update.Operand1 - calculation_update.Operand2;
                    break;
                case "*":
                    calculation_update.Result = calculation_update.Operand1 * calculation_update.Operand2;
                    break;
                case "/":
                    if (calculation_update.Operand2 == 0)
                        return BadRequest("Cannot Divide By Zero");
                    calculation_update.Result = calculation_update.Operand1 / calculation_update.Operand2;
                    break;
                default:
                    return BadRequest("Invalid Operator.Use +, -, *, /. ");
            }
            dbContext.SaveChanges();
            return Ok(calculation_update);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCalculation(int id)
        {
            var delete_Calculation = dbContext.Calculations.Find(id);
            if (delete_Calculation is null)
            {
                return NotFound();
            }
            dbContext.Calculations.Remove(delete_Calculation);
            dbContext.SaveChanges();
            return Ok(delete_Calculation);
        }

    }
        
}


   


