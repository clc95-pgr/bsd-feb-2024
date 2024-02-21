using Microsoft.AspNetCore.Mvc;

namespace IssueTrackerApi.Controllers;

public class IssuesController : ControllerBase
{
    [HttpPost("/issues")]
    public async Task<ActionResult> AddIssueAsync([FromBody] IssuesRequest request)
    {
        var response = new IssueResponse(
            request.Software,
            request.Description,
            DateTimeOffset.Now,
            IssueStatus.Pending
            );
        return Ok(response);
    }
}


public record IssuesRequest(
    string Software,
    string Description
    );

public record IssueResponse(
    string Software,
    string Description,
    DateTimeOffset Logged,
    IssueStatus Status
    );

public enum IssueStatus { Pending };