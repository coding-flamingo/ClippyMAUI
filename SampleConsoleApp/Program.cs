// See https://aka.ms/new-console-template for more information
using ClientLibrary.Models;
using ClientLibrary.Services;

Console.WriteLine("Hello, MAUI Fest!");

HTTPClientService _httpClient = new(new());

APIResultModel aPIResultModel = await
    _httpClient.CallGenericAsync(AppConstants.HOMESITE
        + "/api/Clippy", "", "", HttpMethod.Get);
if(aPIResultModel.Success)
{
    Console.WriteLine(aPIResultModel.Message);
    FileService.WriteToFile( "sample.txt", aPIResultModel.Message);
}
else
{
    Console.WriteLine("Error: " + aPIResultModel.Message);
}