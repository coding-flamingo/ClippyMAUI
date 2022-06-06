using Microsoft.AspNetCore.Components;

namespace ExistingBlazorSite.Client.Pages
{
    public partial class Index
    {
        [Inject]
        HttpClient _httpClient { get; set; }
        private string? _magicalQuote;
        protected override async Task OnInitializedAsync()
        {
            await GetStringFromServerAsync();
        }
        private async Task GetStringFromServerAsync(bool randy= false)
        {
            _magicalQuote = "Getting magical quote from magical server";
            _magicalQuote = await _httpClient.GetStringAsync("/api/Clippy");
        }
    }
}
