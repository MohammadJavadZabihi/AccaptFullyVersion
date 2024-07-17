using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace AccaptFullyVersion.API.Controllers
{
    [Route("api/UserAccount(V1)")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUserServies _userServies;
        private readonly IWalletServies _walletServies;
        private readonly IProductServies _productServies;

        public ProductController(
            IWalletServies walletServies,
            IUserServies userServies,
            IProductServies productServies)
        {
            _userServies = userServies ?? throw new ArgumentException(nameof(userServies));
            _walletServies = walletServies ?? throw new ArgumentException(nameof(walletServies));
            _productServies = productServies ?? throw new ArgumentException(nameof(productServies));
        }

        #region AddProduct

        [HttpPost("ADP(V1)")]
        public async Task<IActionResult> AddProduct(AddProductViewModel addProduct)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            if(addProduct == null)
                return BadRequest("product is null");

            var product = await _productServies.AddProduct(addProduct);

            if (product == null)
                return BadRequest(product);

            return Ok(product);
        }

        #endregion

        #region DeletProduct

        [HttpDelete("DPBN(V1)")]
        public async Task<IActionResult> DeletPrdocut([FromBody] string productName)
        {
            var deletProduct = await _productServies.DeletProduct(productName);

            if (!deletProduct)
                return BadRequest("Some wrong with deleting product");

            return Ok(deletProduct);
        }

        #endregion

        #region UpdateProduct(PUT)

        [HttpPut("UPPR(V1)/{proName}")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateViewModel pro, string proName)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var upProduct = await _productServies.UpdateProduct(pro, proName);

            if (upProduct == null)
                return BadRequest("Wrong with updating Product");

            return Ok(upProduct);
        }

        #endregion

        #region GetAllProducts

        [HttpGet("GALP(V1)/{pahId}/{Take}/{filter}")]
        public async Task<IActionResult> GetAllProduct(int pahId, int Take, string filter)
        {
            var productList = await _productServies.GetProductList(pahId, Take, filter);

            if(productList == null)
                return NotFound();

            return Ok(productList);
        }

        #endregion
    }
}
