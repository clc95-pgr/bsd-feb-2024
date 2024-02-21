namespace BusinessClockApi.Services;

public class AdvancedBusinessClock(ISystemTime time) : IProvideBusinessClock
{
    public bool IsOpen()
    {
        throw new NotImplementedException();
    }
}
