using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using RestSharp;

namespace Client.Utils.Services;

public class RestService
{
    public string? Token { get; set; }
    private readonly RestClient _client;

    public RestService()
    {
        _client = new RestClient("https://saveapis.com/api/berufsschule/vokabeltrainer/");
    }


    public async Task<RestResponse<TResult>> Request<TResult>([StringSyntax("uri")] string uri,
        Method method,
        object? data = default)
    {
        var request = new RestRequest(uri, method);

        if (data is not null)
            request.AddJsonBody(data);

        if (!string.IsNullOrEmpty(Token))
        {
            try
            {
                _client.AddDefaultHeader("Authorization", $"Bearer {Token.Replace("\"", "")}");
            }
            catch
            {
                // Ignored
            }
        }

        var result = method switch
        {
            Method.Get => await _client.ExecuteGetAsync<TResult>(request),
            Method.Delete => await _client.ExecuteAsync<TResult>(request),
            Method.Post => await _client.ExecutePostAsync<TResult>(request),
            Method.Put => await _client.ExecutePutAsync<TResult>(request),
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
        return result;
    }
}