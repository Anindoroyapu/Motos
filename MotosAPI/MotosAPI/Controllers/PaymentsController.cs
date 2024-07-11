using Microsoft.AspNetCore.Mvc;
using MotosAPI.Data;
using MotosAPI.Utils;
using MotosAPI.Models.BillingPayment;

namespace MotosAPI.Controllers
{
    [Route("Controllers/payments")]
    [ApiController]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private object agencyInfo;
        private IEnumerable<object> query;

        public PaymentsApiControllers(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api2/payments
        [HttpGet]
        public async Task<ActionResult> GetPayments(string? q = null, string? status = null, string? orderby = null, string? sort = null, string? page = null, string? cpp = null)
        {
            //--Verify Auth Data
            var authSt = await AuthorizationCustomer.Verify(_context, Request);
            if (authSt.Status != 0)
            {
                return Ok(ApiResponseHandler.Error(authSt.Title, authSt.ReferenceName));
            }

            //--Collect Payments
            var payments = await query.Where(x => x.UserId == agencyInfo.Id && x.TimeDeleted == 0).ToListAsync();

            if (payments == null)
            {
                return Ok(ApiResponseHandler.NotFound("Payments Not Found"));
            }

            return Ok(ApiResponseHandler.Success("Payments", payments.Select(x => new
            {
                x.Id,
                x.UserId,
                x.InvoiceId,
                x.Gateway,
                x.PaymentId,
                x.Method,
                x.CardType,
                x.Bank,
                x.Amount,
                x.Currency,
                x.TimeInitiate,
                x.TimeSuccess,
                x.TimeFailed,
                x.Status,
            })));
        }

        // GET: api/payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPayments(int Id)
        {
            //--Verify Auth Data
            var authSt = await AuthorizationCustomer.Verify(_context, Request);
            if (authSt.Status != 0)
            {
                return Ok(ApiResponseHandler.Error(authSt.Title, authSt.ReferenceName));
            }

            //--Collect Payment Info
            var payment = await _context.BillingPayment.Where(x => x.Id == Id && x.UserId == agencyInfo.Id && x.TimeDeleted == 0).FirstOrDefaultAsync();
            if (payment == null)
            {
                return Ok(ApiResponseHandler.NotFound("Payment Not Found of You are not authorized."));
            }

            return Ok(ApiResponseHandler.Success("Payment Information", new
            {
                payment.Id,
                payment.UserId,
                payment.InvoiceId,
                payment.Gateway,
                payment.PaymentId,
                payment.Method,
                payment.CardType,
                payment.Bank,
                payment.Amount,
                payment.Currency,
                payment.TimeInitiate,
                payment.TimeSuccess,
                payment.TimeFailed,
                payment.Note,
                payment.Status,
            }));
        }
    }
}
