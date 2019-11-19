using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MtnMomo.NET;

namespace momotest.Controllers
{
    [Route("api")]
    public class ApiController : Controller
    {
        private readonly CollectionsClient _collectionsClient;
        private readonly DisbursementsClient _disbursementsClient;
        private readonly RemittancesClient _remittancesClient;
        
        public ApiController(CollectionsClient collectionsClient, DisbursementsClient disbursementsClient, RemittancesClient remittancesClient)
        {
            _collectionsClient = collectionsClient;
            _disbursementsClient = disbursementsClient;
            _remittancesClient = remittancesClient;
        }
        
        [HttpPost("collections")]
        public async Task<IActionResult> InitiateCollection([FromBody] TransactionDetails details)
        {
            var collectionId = await _collectionsClient.RequestToPay(details.Amount, details.Currency, details.ExternalId,
                new Party(details.PhoneNumber, PartyIdType.Msisdn), details.PayerMessage, details.PayeeMessage);

            return Json(collectionId);
        }

        [HttpGet("collections/{id}")]
        public async Task<IActionResult> CheckCollectionStatus(Guid id)
        {
            var result = await _collectionsClient.GetCollection(id);
            return Json(result);
        }
        
        [HttpPost("disbursements")]
        public async Task<IActionResult> InitiateDisbursement([FromBody] TransactionDetails details)
        {
            var disbursementId = await _disbursementsClient.Transfer(details.Amount, details.Currency, details.ExternalId,
                new Party(details.PhoneNumber, PartyIdType.Msisdn), details.PayerMessage, details.PayeeMessage);

            return Json(disbursementId);
        }

        [HttpGet("disbursements/{id}")]
        public async Task<IActionResult> CheckDisbursementStatus(Guid id)
        {
            var result = await _disbursementsClient.GetDisbursement(id);
            return Json(result);
        }
        
        [HttpPost("remittances")]
        public async Task<IActionResult> InitiateRemittance([FromBody] TransactionDetails details)
        {
            var remittanceId = await _remittancesClient.Transfer(details.Amount, details.Currency, details.ExternalId,
                new Party(details.PhoneNumber, PartyIdType.Msisdn), details.PayerMessage, details.PayeeMessage);

            return Json(remittanceId);
        }

        [HttpGet("remittances/{id}")]
        public async Task<IActionResult> CheckRemittanceStatus(Guid id)
        {
            var result = await _remittancesClient.GetRemittance(id);
            return Json(result);
        }
    }

    public class TransactionDetails
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string ExternalId { get; set; }
        public string PhoneNumber { get; set; }
        public string PayerMessage { get; set; }
        public string PayeeMessage { get; set; }
        public string CallbackUrl { get; set; }
    }
}