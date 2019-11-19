using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace momotest.Pages.Momo.Collections
{
    public class Success : PageModel
    {
        public Guid CollectionId { get; set; }
        
        public void OnGet(Guid collectionId)
        {
            CollectionId = collectionId;
        }
    }
}