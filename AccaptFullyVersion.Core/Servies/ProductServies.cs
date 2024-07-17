using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.Core.Servies.Interface;
using AccaptFullyVersion.DataLayer.Context;
using AccaptFullyVersion.DataLayer.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies
{
    public class ProductServies : IProductServies
    {
        private readonly AccaptContext _context;
        public ProductServies(AccaptContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<object?> AddProduct(AddProductViewModel product)
        {
            try
            {
                Product addProduct = new Product()
                {
                    ProductName = product.ProductName,
                    Color = product.Color,
                    Description = product.Description,
                    PrdoctCount = product.PrdoctCount,
                    ProductPrice = product.ProductPrice,
                };

                await _context.Product.AddAsync(addProduct);
                await _context.SaveChangesAsync();


                return addProduct;
            }
            catch (Exception ex)
            {
                return "Error for Adding Product" + ex;
            }
        }

        public async Task<bool> DeletProduct(string productName)
        {
            if (productName == null)
                return false;

            var exixstProduct = await FindeProductByProductName(productName);

            if (exixstProduct == null)
                return false;

            _context.Remove(exixstProduct);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Product?> FindeProductByProductName(string productName)
        {
            return await _context.Product.FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task<List<ShowProductListItemViewModel>> GetProductList(int pahId = 1, int take = 0, string filter = "")
        {
            if (take == 0)
                take = 8;

            IQueryable<Product> result = _context.Product;

            if(!string.IsNullOrEmpty(filter))
            {
                result = result.Where(p => p.ProductName == filter);
            }

            int skip = (pahId - 1) * take;


            return await result.Select(p => new ShowProductListItemViewModel()
            {
                Color = p.Color,
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice
            }).Skip(skip).Take(take).ToListAsync();
        }

        public async Task<Product?> UpdateProduct(ProductUpdateViewModel product, string proName)
        {
            if (product == null)
                return null;

            var existProduc = await FindeProductByProductName(proName);

            if (existProduc == null)
                return null;

            existProduc.ProductPrice = product.ProductPrice;
            existProduc.ProductName = product.ProductName;
            existProduc.PrdoctCount = product.PrdoctCount;
            existProduc.Description = product.Description;
            existProduc.Color = product.Color;

            _context.Update(existProduc);
            await _context.SaveChangesAsync();

            return existProduc;
        }
    }
}
