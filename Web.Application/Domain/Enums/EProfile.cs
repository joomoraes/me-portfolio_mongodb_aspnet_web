namespace Web.Application.Domain.Enums
{
    public enum EProfile
    {
        Administrator = 1,
        Visitor = 2,
        Supervision = 3,
        Editor = 4,
    }

    public static class EProfileHelper
    {
        public static EProfile ParseToInt(int value)
        {
            if (Enum.TryParse(value.ToString(), out EProfile profile))
                return profile;

            throw new ArgumentOutOfRangeException("profile");
        }
    }
}
