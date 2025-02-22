using CurrencyExchange.Application.Services;
using CurrencyExchange.Core.Entities;
using CurrencyExchange.Core.Interfaces.Repositories;
using CurrencyExchange.Core.Interfaces.Services;
using CurrencyExchange.DTO;
using CurrencyExchange.UnitTest.Payloads;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


namespace CurrencyExchange.UnitTest
{
    [TestClass]
    public class CurrencyExchangeServiceTest
    {
        private Mock<ITransferRepository> _mockTransferRepository;
        private Mock<IQuoteRepository> _mockQuoteRepository;
        private ICurrencyExchangeService _currencyExchangeService;
        private Mock<IExchangeRateService> _mockExchangeRateService;
        private Guid _transferId;

        [TestInitialize]
        public void Setup()
        {
            _mockTransferRepository = new Mock<ITransferRepository>();
            _mockQuoteRepository = new Mock<IQuoteRepository>();
            _mockExchangeRateService = new Mock<IExchangeRateService>();
            _currencyExchangeService = new CurrencyExchangeService(_mockQuoteRepository.Object, _mockExchangeRateService.Object, _mockTransferRepository.Object);
        }

        #region Quote

        [TestMethod]
        public async Task CreateQuote_ShouldCreateQuote_WhenValidDataIsProvided()
        {
            // Arrange
            decimal exchangeRate = 0.678M;
            var quoteRequest = new QuoteRequest
            {
                SellCurrency = "AUD",
                BuyCurrency = "USD",
                Amount = 120
            };
            _mockQuoteRepository.Setup(repo => repo.CreateQuote(It.IsAny<Quote>()))
                                   .Returns(Payloads.Payloads.GetQuote(quoteRequest,exchangeRate));
            _mockExchangeRateService.Setup(ex => ex.GetExchangeRates(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(exchangeRate);
               

            // Act
            var result = await _currencyExchangeService.CreateQuoteAsync(quoteRequest);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.QuoteId);
            Assert.IsTrue(result.ConvertedAmount == Math.Round(exchangeRate*quoteRequest.Amount, 2));
        }

        [TestMethod]
        public void CreateQuote_ShouldNotCreateQuote_WhenInValidDataIsProvided()
        {
            // Arrange

            _mockQuoteRepository.Setup(repo => repo.CreateQuote(It.IsAny<Quote>()))
                                   .Returns(new Quote());

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _currencyExchangeService.CreateTransfer(new TransferRequest()));

            // Assert
            Assert.IsNotNull(exception);
            Assert.IsNotEmpty(exception.Message);
        }

        [TestMethod]
        public void RetrieveQuote_ShouldRetrieveQuote_WhenValidDataIsProvided()
        {
            // Arrange

            _mockQuoteRepository.Setup(repo => repo.GetQuoteByQuoteId(It.IsAny<Guid>()))
                                   .Returns(new Quote());

            // Act
            var result = _currencyExchangeService.GetQuoteByQuoteId(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RetrieveQuote_ShouldBeNull_WhenDataIsNotPresent()
        {
            // Arrange

            _mockQuoteRepository.Setup(repo => repo.GetQuoteByQuoteId(It.IsAny<Guid>()));

            // Act
            var result = _currencyExchangeService.GetQuoteByQuoteId(_transferId);

            // Assert
            Assert.IsNull(result);
        }

        #endregion


        #region Transfer

        [TestMethod]
        public void CreateTransfer_ShouldCreateTransfer_WhenValidDataIsProvided()
        {
            // Arrange
            var transfer = Payloads.Payloads.GetTransferRequest();

            _mockQuoteRepository.Setup(repo => repo.GetQuoteByQuoteId(transfer.QuoteId))
                                .Returns(new Quote());

            _mockTransferRepository.Setup(repo => repo.CreateTransfer(It.IsAny<Transfer>()))
                                   .Returns(Payloads.Payloads.GetTransferObject(transfer));

            // Act
            var result = _currencyExchangeService.CreateTransfer(transfer);
            _transferId = result.TransferId;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(transfer.QuoteId, result.TransferDetails.QuoteId);
            Assert.AreEqual(transfer.Payer.Name, result.TransferDetails.Payer.Name);
        }

        [TestMethod]
        public void CreateTransfer_ShouldNotCreateTransfer_WhenInValidDataIsProvided()
        {
            // Arrange
            var transfer = Payloads.Payloads.GetTransferRequest();
            transfer.Payer = null;

            _mockQuoteRepository.Setup(repo => repo.GetQuoteByQuoteId(transfer.QuoteId))
                                .Returns(new Quote());

            _mockTransferRepository.Setup(repo => repo.CreateTransfer(It.IsAny<Transfer>()))
                                   .Returns(Payloads.Payloads.GetTransferObject(transfer));

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => _currencyExchangeService.CreateTransfer(transfer));

            // Assert
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void RetrieveTransfer_ShouldRetrieveTransfer_WhenValidDataIsProvided()
        {
            // Arrange

            _mockTransferRepository.Setup(repo => repo.GetTransferByTransferId(It.IsAny<Guid>()))
                                   .Returns(Payloads.Payloads.GetTransferObject());

            // Act
            var result = _currencyExchangeService.GetTransferByTransferId(_transferId);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RetrieveTransfer_ShouldBeNull_WhenDataIsNotPresent()
        {
            // Arrange

            _mockTransferRepository.Setup(repo => repo.GetTransferByTransferId(It.IsAny<Guid>()));

            // Act
            var result = _currencyExchangeService.GetTransferByTransferId(_transferId);

            // Assert
            Assert.IsNull(result);
        }

        #endregion
    }
}
