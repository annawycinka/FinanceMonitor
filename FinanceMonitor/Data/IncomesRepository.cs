using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceMonitor.Data
{
    public class IncomesRepository : IIncomesRepository
    {
        private readonly FinanceContext _context;

        public IncomesRepository(FinanceContext context)
        {
            this._context = context;
        }

        public async Task<IList<Income>> GetFinancialOperations()
        {
            return await _context.Incomes.ToListAsync();
        }

        public async Task DeleteFinancialOperation(Income deleteOperation)
        {
            if (deleteOperation == null)
            {
                throw new ArgumentNullException();
            }

            _context.Incomes.Remove(deleteOperation);
            await _context.SaveChangesAsync();
        }

        public async Task CreateFinancialOperation(Income newOperation)
        {
            if (newOperation == null)
            {
                throw new ArgumentNullException(nameof(newOperation));
            }

            _context.Incomes.Add(newOperation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFinancialOperation(Income updateOperation)
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Income> GetFinancialOperationById(int id)
        {
            return await _context.Incomes
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}