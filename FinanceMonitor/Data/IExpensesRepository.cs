using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinanceMonitor.Models;

namespace FinanceMonitor.Data
{
    public interface IExpensesRepository
    {
        public Task<IList<Expense>> GetFinancialOperations();
        public Task<Expense> GetFinancialOperationById(int id);
        public Task DeleteFinancialOperation(Expense deleteOperation);
        public Task CreateFinancialOperation(Expense newOperation);
        public Task UpdateFinancialOperation(Expense updateOperation);
    }
}