using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceMonitor.Models;

namespace FinanceMonitor.Data
{
    public interface IIncomesRepository
    {
        public Task<IList<Income>> GetFinancialOperations();
        public Task<Income> GetFinancialOperationById(int id);
        public Task DeleteFinancialOperation(Income deleteOperation);
        public Task CreateFinancialOperation(Income newOperation);
        public Task UpdateFinancialOperation(Income updateOperation);
    }
}