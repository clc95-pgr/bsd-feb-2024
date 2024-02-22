using Alba;
using BusinessClockApi.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace BusinessClockApi.ContractTests;
public class GettingSupportInfo
{

    [Fact]
    public async Task Ready()
    {
        var host = await AlbaHost.For<Program>();
        var response = await host.Scenario(api =>
        {
            api.Get.Url("/support-info");
            api.StatusCodeShouldBeOk();
        });
    }

    [Fact]
    public async Task CorrectType()
    {
        var host = await AlbaHost.For<Program>();
        Assert.IsType<StandardBusinessClock>(host.Services.GetRequiredService<IProvideBusinessClock>());
        Assert.IsType<SystemTime>(host.Services.GetRequiredService<ISystemTime>());
    }

    [Fact]
    public async Task WhenWeAreOpen()
    {
        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureServices(sp =>
            {
                var fakeOpenClock = Substitute.For<IProvideBusinessClock>();
                fakeOpenClock.IsOpen().Returns(true);
                sp.AddScoped(sp => fakeOpenClock);
            });
        });

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/support-info");
            api.StatusCodeShouldBeOk();
        });

        var expected = new SupportInfoResponse("Graham", "555-1212");
        var actual = response.ReadAsJson<SupportInfoResponse>();
        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task WhenWeAreClosed()
    {
        var host = await AlbaHost.For<Program>(config =>
        {
            config.ConfigureServices(sp =>
            {
                var fakeOpenClock = Substitute.For<IProvideBusinessClock>();
                fakeOpenClock.IsOpen().Returns(false);
                sp.AddScoped(sp => fakeOpenClock);
            });
        });

        var response = await host.Scenario(api =>
        {
            api.Get.Url("/support-info");
            api.StatusCodeShouldBeOk();
        });

        var expected = new SupportInfoResponse("TechSupportPros", "800-STUF-BROKE");
        var actual = response.ReadAsJson<SupportInfoResponse>();
        Assert.Equal(expected, actual);
    }
}
