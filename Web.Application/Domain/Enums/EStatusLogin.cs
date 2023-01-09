using System.Net.NetworkInformation;

namespace Web.Application.Domain.Enums
{
    public enum EStatusLogin
    {
        Online = 1,
        Offline = 2,
        Missing = 3,
        Occuped =4
    }

    public static class EStatusLoginHelper
    {
        public static EStatusLogin ParseToInt(int value)
        {
            if (Enum.TryParse(value.ToString(), out EStatusLogin loginstatus))
                return loginstatus;

            throw new ArgumentOutOfRangeException("loginStatus");
        }
    }
}
