using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Service
{
    public class BasketService
    {
        IMemoryGenericRepository<Product> _productContext;
        IMemoryGenericRepository<Basket> _basketContext;

        //for identifyin specific cookies 
        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(IMemoryGenericRepository<Product> productcontext, IMemoryGenericRepository<Basket> basketcon)
        {
            _productContext = productcontext;
            _basketContext = basketcon;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            //read the cookie
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId)) {

                    //load the basketid from the  basket data table if cookie is not null
                    basket = _basketContext.GetById(basketId);

                }


                else
                {
                    if (createIfNull) {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }

            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;

        }
    
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            _basketContext.Create(basket);
            _basketContext.Commit();

            //write cookies to user machine 
            HttpCookie cookie = new HttpCookie(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);

            //add cookie response back to user ->add cookie to user
            httpContext.Response.Cookies.Add(cookie);

            return basket;


        }
        //Add product to the basket 
        public void AddBasket(HttpContextBase httpContext, string productId)
        {
            var basket = GetBasket(httpContext, true);
            //laod the basketItems from database from basketId, all product
            //loading basketItems  from database if its basket prduct id same to user product Id 
            BasketItem item = basket.basketItems.FirstOrDefault(b => b.ProductId == productId);// same produt id

            if (item == null)
            {
                //create new item
                item = new BasketItem
                {
                    BasketId=basket.Id,
                    ProductId=productId,
                    Quantity=1,

                };

                basket.basketItems.Add(item);
            }
            //if same productId then increase the same product
            else
            {
                item.Quantity = item.Quantity + 1;
            }

            _basketContext.Commit();
           
        }

        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.basketItems.FirstOrDefault(c => c.Id == itemId);

            if(item != null)
            {
                basket.basketItems.Remove(item);
                _basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext)
        {
            //GET BASKET FROM DATABASE 

            Basket basket = GetBasket(httpContext, false);

            if(basket != null)
            {
                var result = (from b in basket.basketItems
                              join p in _productContext.GetAll() on b.ProductId equals p.Id
                              select new BasketItemViewModel
                              {
                                  Id=basket.Id,
                                  Quantity=b.Quantity,
                                  PrdouctName=p.Name,
                                  Price=p.Price,
                                  Image=p.Image

                              }).ToList();

                return result;
            }



            else
            {
                return new List<BasketItemViewModel>();
            }
        }
    }
}
