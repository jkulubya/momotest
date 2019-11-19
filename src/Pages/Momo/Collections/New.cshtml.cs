using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MtnMomo.NET;

namespace momotest.Pages.Momo.Collections
{
    public class New : PageModel
    {
        private readonly CollectionsClient _collections;
        
        public New(CollectionsClient collections)
        {
            _collections = collections;
        }
        
        [BindProperty]
        public Model Model { get; set; }
        
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            var result = await _collections.RequestToPay(
                Model.Amount,
                "EUR",
                Model.ExternalId,
                new Party(Model.PhoneNumber, PartyIdType.Msisdn),
                Model.PayerMessage,
                Model.PayeeNote
            );

            return RedirectToPage("Success", new {collectionId = result});
        }
    }

    public class Model
    {
        public string PhoneNumber { get; set; }
        public decimal Amount { get; set; }
        public string PayerMessage { get; set; }
        public string PayeeNote { get; set; }
        public string ExternalId { get; set; }
    }
}