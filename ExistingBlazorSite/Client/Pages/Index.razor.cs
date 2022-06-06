using Microsoft.AspNetCore.Components;

namespace ExistingBlazorSite.Client.Pages
{
    public partial class Index
    {
        [Inject]
        HttpClient _httpClient { get; set; }
        private string? _magicalQuote;

        protected override void OnInitialized()
        {
            _magicalQuote = "Hello MAUI Fest!";
        }
        private async Task GetStringFromServerAsync(bool randy= false)
        {
            _magicalQuote = await _httpClient.GetStringAsync("/api/Clippy");
        }
    }
}
