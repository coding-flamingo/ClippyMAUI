using ClientLibrary.Models;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Services;

public class HTTPClientService
{
    private readonly HttpClient _httpClient;
    private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;
    public HTTPClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        HttpStatusCode[] httpStatusCodesWorthRetrying = {
           HttpStatusCode.RequestTimeout, // 408
           HttpStatusCode.InternalServerError, // 500
           HttpStatusCode.BadGateway, // 502
           HttpStatusCode.ServiceUnavailable, // 503
           HttpStatusCode.GatewayTimeout // 504
        };
        _retryPolicy = Policy
            .Handle<HttpRequestException>()
            .OrInner<TaskCanceledException>()
            .OrResult<HttpResponseMessage>(r => httpStatusCodesWorthRetrying.Contains(r.StatusCode))
              .WaitAndRetryAsync(new[]
              {
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(8)
              });
    }

    public async Task<APIResultModel> CallGenericAsync(string url, string jsonPayload, string token,
        HttpMethod method)
    {
        APIResultModel apiResult = new ();
        HttpResponseMessage responseMessage;
        HttpRequestMessage requestMessage = new (method, url);
        requestMessage.Headers.Authorization = 
            new AuthenticationHeaderValue("Bearer", token);
        try
        {
            if(!string.IsNullOrWhiteSpace(jsonPayload))
            {
                requestMessage.Content = new StringContent(jsonPayload,
                    Encoding.UTF8, "application/json");
            }
            responseMessage = await _retryPolicy.ExecuteAsync(async () =>
                      await SendMessageAsync(requestMessage));
            apiResult.Message = await responseMessage.Content.ReadAsStringAsync();
            apiResult.Success = responseMessage.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            apiResult.Success = false;
            if (ex.Message.Equals("The request message was already sent. Cannot send the same request message multiple times."))
            {
                apiResult.Message = "Error contacting Server Please try again later";
            }
            else
            {
                apiResult.Message = ex.Message;
            }
        }
        return apiResult;
    }
    
    private async Task<HttpResponseMessage> SendMessageAsync(HttpRequestMessage requestMessage)
    {
        HttpResponseMessage response;
        response = await _httpClient.SendAsync(requestMessage);
        return response;
    }
}