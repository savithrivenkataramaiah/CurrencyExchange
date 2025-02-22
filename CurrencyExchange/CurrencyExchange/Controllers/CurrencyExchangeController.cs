using CurrencyExchange.Core.Entities;
using CurrencyExchange.Core.Interfaces.Services;
using CurrencyExchange.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyExchange.Controllers
{
    [ApiController]
    [Route("transfers")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ILogger<CurrencyExchangeController> _logger;
        private readonly ICurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeController(ILogger<CurrencyExchangeController> logger, ICurrencyExchangeService currencyExchangeService)
        {
            _logger = logger;
            _currencyExchangeService = currencyExchangeService;
        }

        [HttpPost("quote")]
        public async Task<IActionResult> CreateQuote([FromBody] QuoteRequest quoteRequest)
        {
            try
            {
                var quote = await _currencyExchangeService.CreateQuoteAsync(quoteRequest);
                return StatusCode(StatusCodes.Status201Created, quote);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("quote/{quoteId}")]
        public IActionResult RetrieveQuote(Guid quoteId)
        {
            try
            {
                var quote = _currencyExchangeService.GetQuoteByQuoteId(quoteId);

                if (quote == null)
                {
                    return NotFound($"Details not found for the quote Id:{quoteId}");
                }
                return Ok(quote);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateTransfer([FromBody] TransferRequest transferRequest)
        {
            try
            {
                var transfer = _currencyExchangeService.CreateTransfer(transferRequest);
                return StatusCode(StatusCodes.Status201Created, transfer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return  StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("{transferId}")]
        public IActionResult RetrieveTransfer(Guid transferId)
        {
            try
            {
                var transfer = _currencyExchangeService.GetTransferByTransferId(transferId);

                if (transfer == null)
                {
                    return NotFound($"Details not found for the Transfer Id:{transferId}");
                }
                return Ok(transfer);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
