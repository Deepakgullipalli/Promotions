using NUnit.Framework;
using Promotions.Business.Handlers;
using Promotions.Controllers;
using Promotions.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotions.Tests
{
    [TestFixture]
    public class Tests
    {
        private readonly ICartPromotionsHandler _cartPromotionHandler = new CartPromotionsHandler();
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task When_Request_Is_Sent_To_Apply_Promotions_On_Cart_Which_Is_Ineligible_Returns_Original_Value_of_The_Cart()
        {
            CartQuery cart = new CartQuery
            {
                CartItems = new List<CartItem>()
            };
            cart.CartItems.Add(new CartItem()
            {
                CartItemCode = Code.A,
                Quantity = 1
            });
            PromotionsController promotionsController = new PromotionsController(_cartPromotionHandler);
            var result = await promotionsController.ApplyPromotions(cart);
            Assert.AreEqual(result, 50);
        }
    }
}