using System;
using System.Transactions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessEntities;
using DataModel;
using AutoMapper;

namespace BusinessServices
{
    public class ProductServices : IProductServices
    {
        private UnitOfWork unitOfWork;


        public ProductServices()
        {
            unitOfWork = new UnitOfWork();
        }


        public int CreateProduct(ProductEntity productEntity)
        {
            using (var scope = new TransactionScope())
            {
                var product = new product { ProductName = productEntity.ProductName };
                unitOfWork.ProductRepository.Insert(product);
                unitOfWork.Save();
                scope.Complete();
                return product.ProductId;
            }
        }


        public bool DeleteProduct(int productId)
        {
            var success = false;
            if (productId < 0)
            {
                using (var scope = new TransactionScope())
                {
                    var product = unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        unitOfWork.ProductRepository.Delete(product);
                        unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<ProductEntity> GetAllProducts()
        {
            var product = unitOfWork.ProductRepository.GetAll().ToList();
            if (product.Any())
            {
                return Mapper.Map<List<product>, List<ProductEntity>>(product);
            }
            return null;
        }

        public ProductEntity GetProductById(int productId)
        {
            var product = unitOfWork.ProductRepository.GetByID(productId);
            if (product != null)
            {
                return Mapper.Map<product, ProductEntity>(product);
            }
            return null;
        }

        public bool UpdateProduct(int productId, ProductEntity productEntity)
        {
            var success = false;
            if (productEntity != null)
            {
                using (var scope = new TransactionScope())
                {
                    var product = unitOfWork.ProductRepository.GetByID(productId);
                    if (product != null)
                    {
                        product.ProductName = productEntity.ProductName;
                        unitOfWork.ProductRepository.Update(product);
                        unitOfWork.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
