using Moq;
using NUnit.Framework;
using Promotions.Business.Handlers;
using Promotions.Controllers;
using Promotions.Core.Models;
using Promotions.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Promotions.Tests
{
    [TestFixture]
    public class Tests
    {
        private  Mock<ICartPromotionsHandler> _cartPromotionHandlerMock;
        [SetUp]
        public void Setup()
        {
            _cartPromotionHandlerMock = new Mock<ICartPromotionsHandler>();
            _cartPromotionHandlerMock.Setup(x => x.FetchCartValue(It.IsAny<CartQuery>())).Returns(Task.FromResult(new CartValueResponse() { Message="50"}));
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
            PromotionsController promotionsController = new PromotionsController(_cartPromotionHandlerMock.Object);
            var result = await promotionsController.ApplyPromotions(cart);
            Assert.AreEqual(result.Message, "50");
        }
    }
}