using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceMonitor.Data
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly FinanceContext _context;

        public ExpensesRepository(FinanceContext context)
        {
            this._context = context;
        }

        public async Task<IList<Expense>> GetFinancialOperations()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task DeleteFinancialOperation(Expense deleteOperation)
        {
            if (deleteOperation == null)
            {
                throw new ArgumentNullException();
            }

            _context.Expenses.Remove(deleteOperation);
            await _context.SaveChangesAsync();
        }

        public async Task CreateFinancialOperation(Expense newOperation)
        {
            if (newOperation == null)
            {
                throw new ArgumentNullException(nameof(newOperation));
            }

            _context.Expenses.Add(newOperation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFinancialOperation(Expense updateOperation)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Expense> GetFinancialOperationById(int id)
        {
            return await _context.Expenses
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}