using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NutritionApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NutritionApp.Models.ViewModels
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();
            basket.Session = session;
            return basket;
        }
        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddProduct(int productId, int quantity)
        {
            base.AddProduct(productId, quantity);
            Session.SetJson("Basket", this);

        }

        //public override void RemoveLine(Product product)
        //{
        //    base.RemoveLine(product);
        //    Session.SetJson("Basket", this);
        //}
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
