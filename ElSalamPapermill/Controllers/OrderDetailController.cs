using ElSalamPapermill.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ElSalamPapermill.Data;
using ElSalamPapermill.Helpers;

namespace ElSalamPapermill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public OrderDetailController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // POST api/<OrderDetailController>
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDetail orderdetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            orderdetail.OrderGuid = Guid.NewGuid();
            await _context.OrderDetail.AddAsync(orderdetail);
            var orderConfirmed = await _context.SaveChangesAsync();

            if (orderConfirmed == 1)
            {
                // Send email
                await _emailSender.SendEmailAsync("omarkdyou@gmail.com", orderdetail.Email, $"Order Request From {orderdetail.CompanyName} - Order Id {orderdetail.OrderGuid}", orderdetail);
            }

            return Ok("Order created successfully");
        }
    }

}
