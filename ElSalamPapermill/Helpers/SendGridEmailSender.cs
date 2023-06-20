using Azure.Core;
using ElSalamPapermill.Domain.Entities;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Mail.Model;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ElSalamPapermill.Helpers
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string fromEmail, string toEmail, string subject, object body, bool hasOrder);
    }

    public class SendGridEmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<SendGridSettings> _options;
        public bool hadOrder { get; set; }

        public SendGridEmailSender(IConfiguration configuration, IOptions<SendGridSettings> options)
        {
            _configuration = configuration;
            _options = options;
        }

        public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, object body, bool hasOrder)
        {
            hadOrder = hasOrder;
            //var apiKey = Environment.GetEnvironmentVariable("SENDGRID_Environment");
            string? apiKey = _options.Value.ApiKey;
            var sendGridClient = new SendGridClient(apiKey);
            var From = new EmailAddress(fromEmail);
            var To = new EmailAddress(toEmail);
            var plainTextContent = GetPlainTextContent(body);
            var htmlContent = GetHtmlContent(body);
            var message = MailHelper.CreateSingleEmail(From, To, subject, plainTextContent, htmlContent);
            try
            {
                var response = await sendGridClient.SendEmailAsync(message);
                Console.WriteLine("Email sent successfully. Status code: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
                throw;
            }
        }

        private string GetPlainTextContent(object body)
        {
            if (hadOrder == true)
            {
                var orderDetail = (OrderDetail)body;
                var plainTextContent =
                    $"Order Id: {orderDetail.OrderGuid}\n" +
                    $"Company Name: {orderDetail.CompanyName}\n" +
                    $"Email: {orderDetail.Email}\n" +
                    $"Website: {orderDetail.Website}\n" +
                    $"Phone: {orderDetail.Phone}\n" +
                    $"Company Name: {orderDetail.ApplicantsName}\n" +
                    $"Email: {orderDetail.ApplicantsPhone}\n" +
                    $"Website: {orderDetail.Country}\n" +
                    $"Phone: {orderDetail.City}\n" +
                    $"Notes: {orderDetail.Area}" +
                    $"Email: {orderDetail.DetailedAddress}\n" +
                    $"Website: {orderDetail.CardboardType}\n" +
                    $"Phone: {orderDetail.CutTypes}\n" +
                    $"Company Name: {orderDetail.LengthAndDiameter}\n" +
                    $"Email: {orderDetail.Width}\n" +
                    $"Website: {orderDetail.QuantityType}\n" +
                    $"Phone: {orderDetail.Quantity}\n" +
                    $"Notes: {orderDetail.Notes}";
                return plainTextContent;
            }
            else
            {
                var productInquiry = (ProductInquiry)body;
                var plainTextContent =
                    $"Client Name: {productInquiry.ClientName}\n" +
                    $"Client Email: {productInquiry.ClientEmail}\n" +
                    $"Product Type: {productInquiry.ProductType}\n" +
                    $"Client Mobile: {productInquiry.ClientMobile}\n" +
                    $"Client Msg: {productInquiry.ClientMsg}\n";
                return plainTextContent;
            }

        }

        private string GetHtmlContent(object body)
        {
            if (hadOrder == true)
            {
                var orderDetail = (OrderDetail)body;
                var htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Email Template</title>
            <link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
<style>
        body {
            background-color: #f7f7f7;
            color: #333;
        }

        .container {
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 20px;
        }

        h2 {
            color: #333;
            margin-top: 0;
        }

        hr {
            border-color: #ccc;
        }

        .row {
            margin-bottom: 20px;
        }

        .col-md-6 {
            border-right: 1px solid #ccc;
            padding-right: 20px;
        }

        h4 {
            color: #333;
            margin-top: 0;
            margin-bottom: 10px;
        }

        strong {
            color: #333;
        }

        .highlight {
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
        </head>
        <body>
            <div class=""container"">
                <h2>Order Details</h2>
                <hr>
                <div class=""row"">
                    <div class=""col-md-6"">
                        <h4>Company Information</h4>
                        <div class=""highlight""><strong>Company Name:</strong> " + orderDetail.CompanyName + @"</div>
                        <div class=""highlight""><strong>Email:</strong> " + orderDetail.Email + @"</div>
                        <div class=""highlight""><strong>Website:</strong> " + orderDetail.Website + @"</div>
                        <div class=""highlight""><strong>Phone:</strong> " + orderDetail.Phone + @"</div>
                        <div class=""highlight""><strong>Applicant's Name:</strong> " + orderDetail.ApplicantsName + @"</div>
                        <div class=""highlight""><strong>Applicant's Phone:</strong> " + orderDetail.ApplicantsPhone + @"</div>
                    </div>
                <hr>
                    <div class=""col-md-6"">
                        <h4>Shipping Address</h4>
                        <div class=""highlight""><strong>Country:</strong> " + orderDetail.Country + @"</div>
                        <div class=""highlight""><strong>City:</strong> " + orderDetail.City + @"</div>
                        <div class=""highlight""><strong>Area:</strong> " + orderDetail.Area + @"</div>
                        <div class=""highlight""><strong>Detailed Address:</strong> " + orderDetail.DetailedAddress + @"</div>
                    </div>
                </div>
                <hr>
                <div class=""row"">
                    <div class=""col-md-6"">
                        <h4>Order Information</h4>
                        <div class=""highlight""><strong>Cardboard Type:</strong> " + orderDetail.CardboardType + @"</div>
                        <div class=""highlight""><strong>Cut Types:</strong> " + orderDetail.CutTypes + @"</div>
                        <div class=""highlight""><strong>Length and Diameter:</strong> " + orderDetail.LengthAndDiameter + @"</div>
                        <div class=""highlight""><strong>Width:</strong> " + orderDetail.Width + @"</div>
                        <div class=""highlight""><strong>Quantity Type:</strong> " + orderDetail.QuantityType + @"</div>
                        <div class=""highlight""><strong>Quantity:</strong> " + orderDetail.Quantity + @"</div>
                        <div class=""highlight""><strong>Notes:</strong> " + orderDetail.Notes + @"</div>
                    </div>
                </div>
            </div>
        </body>
        </html>";
                return htmlTemplate;
            }
            else
            {
                var productInquiry = (ProductInquiry)body;
                var htmlTemplate = @"
        <!DOCTYPE html>
        <html>
        <head>
            <title>Email Template</title>
            <link rel=""stylesheet"" href=""https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"">
<style>
        body {
            background-color: #f7f7f7;
            color: #333;
        }

        .container {
            background-color: #fff;
            border: 1px solid #ccc;
            border-radius: 5px;
            padding: 20px;
        }

        h2 {
            color: #333;
            margin-top: 0;
        }

        hr {
            border-color: #ccc;
        }

        .row {
            margin-bottom: 20px;
        }

        .col-md-6 {
            border-right: 1px solid #ccc;
            padding-right: 20px;
        }

        h4 {
            color: #333;
            margin-top: 0;
            margin-bottom: 10px;
        }

        strong {
            color: #333;
        }

        .highlight {
            background-color: #f5f5f5;
            padding: 10px;
            border-radius: 5px;
        }
    </style>
        </head>
        <body>
            <div class=""container"">
                <h2>Inquiry Details</h2>
                <hr>
                <div class=""row"">
                    <div class=""col-md-6"">
                        <h4>Product Inquiry Information</h4>
                        <div class=""highlight""><strong>Company Name:</strong> " + productInquiry.ClientName + @"</div>
                        <div class=""highlight""><strong>Email:</strong> " + productInquiry.ClientEmail + @"</div>
                        <div class=""highlight""><strong>Website:</strong> " + productInquiry.ProductType + @"</div>
                        <div class=""highlight""><strong>Phone:</strong> " + productInquiry.ClientMobile + @"</div>
                        <div class=""highlight""><strong>Applicant's Name:</strong> " + productInquiry.ClientMsg + @"</div>
                    </div>
            </div>
        </body>
        </html>";
                return htmlTemplate;
            }
        }
    }
}
