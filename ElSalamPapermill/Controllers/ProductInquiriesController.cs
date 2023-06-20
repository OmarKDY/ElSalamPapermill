using ElSalamPapermill.Data;
using ElSalamPapermill.Domain.Entities;
using ElSalamPapermill.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ElSalamPapermill.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInquiriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ProductInquiriesController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        [HttpPost]
        [HttpPost("addProductInquiry")]
        public async Task<IActionResult> Create([Bind("InquiryId,ClientName,ClientEmail,ProductType,ClientMobile,ClientMsg")] ProductInquiry productInquiry)
        {
            _context.Add(productInquiry);
            var inquirySent = await _context.SaveChangesAsync();
            if (inquirySent == 1)
            {
                // Send email
                await _emailSender.SendEmailAsync("omarkdyou@gmail.com", productInquiry.ClientEmail, $"Product Inquiry From {productInquiry.ClientName}", productInquiry, false);
            }
            return Ok("Product Inquiry Sent Successfully");
        }
    }
}
