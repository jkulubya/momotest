using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MtnMomo.NET;

namespace momotest.Pages.Momo.Collections
{
    
    public class CheckStatus : PageModel
    {
        private readonly CollectionsClient _collections;
        
        public CheckStatus(CollectionsClient collections)
        {
            _collections = collections;
        }
        
        public Guid Id { get; set; }
        public Collection Collection { get; set; }
        
        public async Task<IActionResult> OnGet(Guid id)
        {
            Id = id;

            Collection = await _collections.GetCollection(id);

            return Page();
        }
    }
}