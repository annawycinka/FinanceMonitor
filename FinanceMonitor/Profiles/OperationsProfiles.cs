using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceMonitor.Dtos;
using FinanceMonitor.Models;

namespace FinanceMonitor.Profiles
{
    public class OperationsProfiles : Profile
    {
        public OperationsProfiles()
        {
            CreateMap<Expense, ReadIncomeDto>();
            CreateMap<CreateIncomeDto, Expense>();
            CreateMap<UpdateIncomeDto, Expense>();
            CreateMap<Income, ReadIncomeDto>();
            CreateMap<CreateIncomeDto, Income>();
            CreateMap<UpdateIncomeDto, Income>();
        }
    }
}