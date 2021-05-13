using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class Basket
    {


        private List<BasketLine> lineCollection = new List<BasketLine>();

        
        public virtual void AddProduct(Product product, int quantity)
        {
            
            BasketLine newProd = lineCollection
                .Where(p => p.Product.ProductId == product.ProductId)
                .FirstOrDefault();

            if (newProd == null)
            {
                lineCollection.Add(new BasketLine { Product = product, Quantity = quantity });
            }
            else
            {
                newProd.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product) =>
            lineCollection.RemoveAll(i => i.Product.ProductId == product.ProductId);


      
        public virtual void Clear() => lineCollection.Clear();
        public List<BasketLine> Lines => lineCollection;

    }

    public class BasketLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }


}
