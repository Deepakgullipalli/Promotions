﻿using Microsoft.AspNetCore.Mvc;
using Promotions.Business.Handlers;
using Promotions.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Promotions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PromotionsController : ControllerBase
    {
        private readonly ICartPromotionsHandler _cartPromotionsHandler;
        public PromotionsController(ICartPromotionsHandler cartPromotionsHandler)
        {
            _cartPromotionsHandler = cartPromotionsHandler;
        }
        
        [HttpPut]
        public async Task<CartValueResponse> ApplyPromotions(CartQuery cart)
        {
            return await _cartPromotionsHandler.FetchCartValue(cart);
        }
    }
}
