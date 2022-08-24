using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            //SQL Server
            _products = new List<Product> {
                new Product {ProductId=1, CategoryId=1, ProductName="Bardak",  UnitPrice=15,   UnitsInStock=15},
                new Product {ProductId=2, CategoryId=1, ProductName="Kamera",  UnitPrice=500,  UnitsInStock=3},
                new Product {ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product {ProductId=4, CategoryId=2, ProductName="Klavye",  UnitPrice=150,  UnitsInStock=65},
                new Product {ProductId=5, CategoryId=2, ProductName="Fare",    UnitPrice=85,   UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //hangisinin silineceğini bilmiyorum çünkü hepsinin Id'si farkli o yuzden döngü kullanıp istediğimi bulduruyorum 
            Product ProductToDelete; //silinecek veri demek

            //bu ifade alttaki ifade ile aynı o yüzden bunu kullanmaya gerek yok
            //foreach (var p in _products)
            //{
            //    if (product.ProductId==p.ProductId)
            //    {
            //        ProductToDelete = p;
            //    }
            //}

            ProductToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); //products'ı tek tek dolaşmaya yarar

            _products.Remove(ProductToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products; //benim yazdığım ürünleri döndürüyor
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); //where koşulu içindeki şarta uyan bütün elemanları
                                                                              //yeni bir liste haline getirir ve onu döndürür
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            //güncellenecek referansı bulmam lazım

            //Gönderdiğim ürün Id'sine sahip olan listedeki ürünü bul demek
            Product ProductToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            //güncelliyoruz
            ProductToUpdate.ProductId = product.ProductId;
            ProductToUpdate.ProductName = product.ProductName;
            ProductToUpdate.UnitPrice = product.UnitPrice;
            ProductToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
