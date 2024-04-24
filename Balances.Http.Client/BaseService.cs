using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;

public class BaseService<T> where T : class
{
    private readonly HttpClient _httpClient;
    private readonly string _controller;

    public BaseService(HttpClient httpClient, string controller)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _controller = controller ?? throw new ArgumentNullException(nameof(controller));

    }

    public async Task<TResponse?> GetFromJsonAsync<TResponse>(string uri)
    {       
        return await _httpClient.GetFromJsonAsync<TResponse>($"{_controller}/{uri}");
    }

    public async Task<TResponse?> PostAsJsonAsync<TRequest, TResponse>(string uri, TRequest data)
    {
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{_controller}/{uri}", data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task<HttpResponseMessage> DeleteJsonAsync<TRequest>(string uri, TRequest data)
    {
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{_controller}/{uri}");
        request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

        return await _httpClient.SendAsync(request);
    }

    public async Task<HttpResponseMessage> PutAsJsonAsync<TRequest>(string uri, TRequest data)
    {
        return await _httpClient.PutAsJsonAsync($"{_controller}/{uri}", data);
    }

    
    public async Task<TResponse?> GetByIdAsync<TResponse>(string id)
    {
        return await _httpClient.GetFromJsonAsync<TResponse>($"{_controller}/{id}");
    }

    public async Task<IEnumerable<TResponse>?> GetAllAsync<TResponse>()
    {
        return await _httpClient.GetFromJsonAsync<IEnumerable<TResponse>>($"{_controller}");
    }
}

