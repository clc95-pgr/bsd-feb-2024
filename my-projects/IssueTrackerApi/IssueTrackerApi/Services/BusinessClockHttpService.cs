namespace IssueTrackerApi.Services;

public class BusinessClockHttpService(HttpClient client)
{
    public async Task<SupportResponse?> GetCurrentSupportInformationAsync()
    {
        var response = await client.GetAsync("/support-info");
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadFromJsonAsync<SupportResponse>();
        return body;
    }
}

public record SupportResponse(string name, string Phone);