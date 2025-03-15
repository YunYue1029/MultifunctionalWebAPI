using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MyWebApp.Controllers
{
    public class Expense
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public string Category { get; set; } = string.Empty; // "income" or "expense"
    }

    [ApiController]
    [Route("api/expenses")]
    public class ExpensesController : ControllerBase
    {
        private static List<Expense> expenses = new List<Expense>();
        private static int nextId = 1;

        // 取得所有記帳記錄
        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpenses()
        {
            return Ok(expenses);
        }

        // 新增記帳記錄
        [HttpPost]
        public ActionResult<Expense> CreateExpense([FromBody] Expense newExpense)
        {
            if (newExpense.Amount <= 0)
            {
                return BadRequest(new { error = "Amount must be greater than 0." });
            }

            newExpense.Id = nextId++;
            expenses.Add(newExpense);
            return CreatedAtAction(nameof(GetExpenses), new { id = newExpense.Id }, newExpense);
        }

        // 刪除記帳記錄
        [HttpDelete("{id}")]
        public IActionResult DeleteExpense(int id)
        {
            var expense = expenses.FirstOrDefault(e => e.Id == id);
            if (expense == null)
            {
                return NotFound(new { error = "Record not found." });
            }

            expenses.Remove(expense);
            return NoContent();
        }
    }
}