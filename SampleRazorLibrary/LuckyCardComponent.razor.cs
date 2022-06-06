using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleRazorLibrary
{
    public partial class LuckyCardComponent
    {
        [Parameter] public string? MessageToDisplay { get; set; }
        [Parameter] public EventCallback<bool> GetNewQuote { get; set; }

        private async Task GetNewQuoteAsync()
        {
            await GetNewQuote.InvokeAsync(true);
        }
    }
}
