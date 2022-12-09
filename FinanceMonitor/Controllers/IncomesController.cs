using System;
using System.Collections.Generic;
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
    public class IncomesController : ControllerBase
    {
        private readonly IIncomesRepository _repository;
        private readonly IMapper _mapper;

        public IncomesController(IIncomesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("/IncomeCategories", Name = nameof(GetIncomeCategories))]
        public async Task<ActionResult<IEnumerable<IncomeCategory>>> GetIncomeCategories()
        {
            return Ok(Enum.GetValues<IncomeCategory>());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadIncomeDto>>> GetAllFinancialOperations()
        {
            var allOperations = await _repository.GetFinancialOperations();
            return Ok(_mapper.Map<IEnumerable<ReadIncomeDto>>(allOperations));
        }

        [HttpGet("{id}", Name = nameof(GetIncomeById))]
        public async Task<ActionResult<ReadIncomeDto>> GetIncomeById(int id)
        {
            var operation = await _repository.GetFinancialOperationById(id);
            if (operation != null)
            {
                return Ok(_mapper.Map<ReadIncomeDto>(operation));
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<ReadIncomeDto>> CreateFinancialOperation(
            CreateIncomeDto createOperationDto)
        {
            var operationModel = _mapper.Map<Income>(createOperationDto);
            await _repository.CreateFinancialOperation(operationModel);
            var operationReadDto = _mapper.Map<ReadIncomeDto>(operationModel);
            return CreatedAtRoute(nameof(GetIncomeById), new { Id = operationReadDto.Id }, operationReadDto);
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
            UpdateIncomeDto updatedOperationDto)
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