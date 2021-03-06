using ProductAPI.Entities;
using ProductAPI.Repositories;
using ProductAPI.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EventBusRabbitMQ.Events;

namespace ProductAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductController> _logger;

        private readonly IQueryCommand _searchCommand;

        private readonly IQueryCommand _filterByColorCommand;

        private readonly IQueryCommand _filterByBranchCommand;

        private readonly IViewCommand _viewCommand;

        public ProductController(IProductRepository repository, ILogger<ProductController> logger, Func<CommandEnum, ProductColumnEnum?, IQueryCommand?> getCommand, IViewCommand viewCommand)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _searchCommand = getCommand(CommandEnum.SEARCH, null) ?? throw new ArgumentNullException(nameof(_searchCommand));
            _filterByColorCommand = getCommand(CommandEnum.FILTER, ProductColumnEnum.Color)?? throw new ArgumentNullException(nameof(_filterByColorCommand));
            _filterByBranchCommand = getCommand(CommandEnum.FILTER, ProductColumnEnum.Branch)?? throw new ArgumentNullException(nameof(_filterByBranchCommand));
            _viewCommand = viewCommand ?? throw new ArgumentNullException(nameof(viewCommand))?? throw new ArgumentNullException(nameof(_viewCommand));
        }

        [Route("Sort/{column}/{direction}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> SortProducts(ProductColumnEnum column, SortDirection direction)
        {
            var products = await _repository.SortProducts(column.ToString(), direction);
            return Ok(products);
        }

        [Route("[action]/{username}/{keyword}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> SearchProducts(string username, string keyword)
        {
            ProductSearchEvent searchEvent = new ProductSearchEvent{
                RequestId = Guid.NewGuid(),
                Keyword = keyword,
                UserName = username,
            };

            await _searchCommand.Execute(searchEvent);
            var products = _searchCommand.Result;
            return Ok(products);
        }

        [Route("[action]/{username}/{value}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProductByColor(string username, string value)
        {
            var filterEvent = new ProductFilterEvent{
                RequestId = Guid.NewGuid(),
                Column = ProductColumnEnum.Color.ToString(),
                Value = value,
                UserName = username,
            };

            await _filterByColorCommand.Execute(filterEvent);
            var products = _filterByColorCommand.Result;
            return Ok(products);
        }

        [Route("[action]/{username}/{value}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> FilterProductByBranch(string username, string value)
        {
            var filterEvent = new ProductFilterEvent{
                RequestId = Guid.NewGuid(),
                Column = ProductColumnEnum.Branch.ToString(),
                Value = value,
                UserName = username,
            };

            await _filterByBranchCommand.Execute(filterEvent);
            var products = _filterByBranchCommand.Result;
            return Ok(products);
        }

        [HttpGet("[action]/{username}/{id}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> ViewProduct(string username, int id)
        {
            var viewEvent = new ProductViewEvent{
                RequestId = Guid.NewGuid(),
                Id = id,
                UserName = username,
            };

            await _viewCommand.Execute(viewEvent);
            var product = _viewCommand.Result;

            if (product == null)
            {
                _logger.LogError($"Product with id: {id} was not found.");
                return NotFound();
            }

            return Ok(product);
        }
    }
}