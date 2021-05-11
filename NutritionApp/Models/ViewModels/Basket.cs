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

        
        public virtual void AddProduct(int productId, int quantity)
        {
            
            BasketLine newProd = lineCollection
                .Where(p => p.ProductId == productId)
                .FirstOrDefault();

            if (newProd != null)
            {
                lineCollection.Add(new BasketLine { ProductId = productId, Quantity = quantity });
            }
            else
            {
                newProd.Quantity += quantity;
            }
        }

        //public virtual void RemoveLine(Product product) =>
        //    lineCollection.RemoveAll(i => i.Product.ProductId == product.ProductId);

      //  public decimal ComputeTotalValue() => lineCollection.Sum(e => e.Product.Price * e.Quantity);
        public virtual void Clear() => lineCollection.Clear();
        public List<BasketLine> Lines => lineCollection;

    }

    public class BasketLine
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }


}
