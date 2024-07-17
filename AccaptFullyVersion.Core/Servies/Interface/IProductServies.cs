using AccaptFullyVersion.Core.DTOs;
using AccaptFullyVersion.DataLayer.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccaptFullyVersion.Core.Servies.Interface
{
    public interface IProductServies
    {
        Task<object?> AddProduct(AddProductViewModel product);
        Task<bool> DeletProduct(string productName);
        Task<Product?> UpdateProduct(ProductUpdateViewModel product, string proName);
        Task<Product?> FindeProductByProductName(string productName);
        Task<List<ShowProductListItemViewModel>> GetProductList(int pahId = 1, int take = 0, string filter = "");
    }
}
