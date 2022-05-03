using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinanceMonitor.Data;
using FinanceMonitor.Dtos;
using FinanceMonitor.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceMonitor.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IExpensesRepository _repository;
        private readonly IMapper _mapper;

        public ExpensesController(IExpensesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadExpenseDto>>> GetAllFinancialOperations()
        {
            var allOperations = await _repository.GetFinancialOperations();
            return Ok(_mapper.Map<IEnumerable<ReadExpenseDto>>(allOperations));
        }

        [HttpGet("{id}", Name = nameof(GetExpenseById))]
        public async Task<ActionResult<ReadExpenseDto>> GetExpenseById(int id)
        {
            var operation = await _repository.GetFinancialOperationById(id);
            if (operation != null)
            {
                return Ok(_mapper.Map<ReadExpenseDto>(operation));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReadExpenseDto>> CreateFinancialOperation(
            CreateExpenseDto createOperationDto)
        {
            var operationModel = _mapper.Map<Expense>(createOperationDto);
            await _repository.CreateFinancialOperation(operationModel);
            var operationReadDto = _mapper.Map<ReadExpenseDto>(operationModel);
            return CreatedAtRoute(nameof(GetExpenseById), new { Id = operationReadDto.Id }, operationReadDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFinancialOperation(int id)
        {
            var operation = await _repository.GetFinancialOperationById(id);
            if (operation != null)
            {
                await _repository.DeleteFinancialOperation(operation);
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFinancialOperation(int id,
            UpdateExpenseDto updatedOperationDto)
        {
            var operationModel = await _repository.GetFinancialOperationById(id);
            if (operationModel == null)
            {
                return NotFound();
            }

            _mapper.Map(updatedOperationDto, operationModel);

            await _repository.UpdateFinancialOperation(operationModel);

            return NoContent();
        }
    }
}