namespace IBKR_Service.Config
{
    public interface IApiMessenger {
        Task<string> PostAsyncJsonResponse(string url, string jsonBody);
        Task<string> GetAsyncJsonResponse(string url);
        Task<HttpResponseMessage> GetAsync(string url);
    }
}
