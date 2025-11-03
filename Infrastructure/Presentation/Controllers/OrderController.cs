using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Servieces.Abstractions;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class OrderController(IServiecManager _serviceManager):ApiBaseController
    {
        #region Create Order
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var Order= await _serviceManager.orderService.CreateOrder(orderDto, GetEmailFromToken());
            return Ok(Order);
        }

        #endregion
        #region Get Delivery Methods
        [HttpGet("DeliveryMethods")]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>>GetAllDeliveryMethods()
        {
            var DeliveryMethods=await _serviceManager.orderService.GetAllDeliveryMethodsAsync();
            return Ok(DeliveryMethods);
        }
        #endregion
        #region GetAllOrderdsByEmail
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersByEmail()
        {
            var Orders = await _serviceManager.orderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(Orders);

        }
        #endregion
        #region GetOrderById
        [Authorize]
        [HttpGet("{Id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var Order = await _serviceManager.orderService.GetOrderByIdAsync(Id);
            return Ok(Order);
        }

        #endregion
    }
}
