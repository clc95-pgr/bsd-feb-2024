namespace BusinessClockApi.UnitTests;
public class StandardBusinessClockTests
{
    [Fact]
    public void ClosedOnSaturday()
    {
        Assert.Fail("N/A");
    }

    [Fact]
    public void ClosedOnSunday()
    {
        Assert.Fail("N/A");
    }

    [Theory]
    [InlineData("2/20/2023 16:25:00")]
    [InlineData("2/20/2023 09:00:00")]
    public void WeAreOpen(string dateTime)
    {
        Assert.Fail("N/A");
    }

    [Theory]
    [InlineData("2/20/2023 17:00:00")]
    [InlineData("2/20/2023 8:59:59")]
    public void WeAreClosed(string dateTime)
    {
        Assert.Fail("N/A");
    }
}
