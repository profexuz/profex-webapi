using Profex.Domain.Constants;

namespace Profex.DataAccsess.Common.Helpers;

public class TimeHelper
{
    public static DateTime GetDateTime()
    {
        var dtTime = DateTime.UtcNow;
        dtTime.AddHours(TimeConstants.UTC);

        return dtTime;
    }
}
