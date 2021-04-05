using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using TCRS.Database;
using TCRS.Database.Model;
using TCRS.Server.Tokens;
using TCRS.Shared.Objects.Payment;

namespace TCRS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {

        private readonly IDataAccess _db;
        private readonly DatabaseContext _databaseContext;
        public PaymentController(IDataAccess db, IOptions<DatabaseContext> databaseContext)
        {
            _db = db;
            _databaseContext = databaseContext.Value;
        }

        //TEST IMPLEMENTATION (Need to integrate payment API)
        [HttpGet("PostPayment")]
        public ActionResult PostPayment(PaymentData paymentData)
        {
            var Payment = new Payment
            {
                //citation_number = paymentData.citation_number,
                payment = 3.50,
                payment_date = DateTime.Now,
                made_by = "Joey joe joe",
                payment_method = "Credit Card"
            };

            try
            {
                _db.PayForCitation(Payment, _databaseContext.Server);
                return Ok("Recipt Issued");
            }
            catch
            {
                return BadRequest("Invalid request");
            }
        }

    }
}
