using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Codecool.CodecoolShop.Daos;
using Codecool.CodecoolShop.Models;

namespace Codecool.CodecoolShop.Services
{
    public class ProductService
    {
        private readonly IProductDao productDao;
        private readonly IProductCategoryDao productCategoryDao;
        private readonly ISupplierDao supplierDao;

        public ProductService(IProductDao productDao, IProductCategoryDao productCategoryDao, ISupplierDao supplierDao)
        {
            this.productDao = productDao;
            this.productCategoryDao = productCategoryDao;
            this.supplierDao = supplierDao;
        }

        public ProductCategory GetProductCategory(int categoryId)
        {
            return this.productCategoryDao.Get(categoryId);
        }

        public IEnumerable<Product> GetProductsForCategory(int categoryId)
        {
            ProductCategory category = this.productCategoryDao.Get(categoryId);
            return this.productDao.GetBy(category);
        }
        
        public IEnumerable<Product> GetProductsForCategory(string name)
        {
            ProductCategory category = this.productCategoryDao.Get(name);
            return this.productDao.GetBy(category);
        }

        public IEnumerable<Product> GetProductsForSupplier(string name)
        {
            Supplier supplier = this.supplierDao.Get(name);
            return this.productDao.GetBy(supplier);
        }
        public IEnumerable<ProductCategory> GetProductCategories()
        {
            var categories = this.productCategoryDao.GetAll();
            return categories;
        }

        public IEnumerable<Supplier> GetProductSuppliers()
        {
            var suppliers = this.supplierDao.GetAll();
            return suppliers;
        }

        public Product GetProduct(int productId)
        {
            return this.productDao.Get(productId);
        }
    }
}
