namespace Blazorit.Client.Support.Enums
{
    /// <summary>
    /// This type is for boolean result in razor pages. Boolean result may be true or false, but task awaits result must be None
    /// </summary>
    public enum Tribool
    {
        None = -1,
        False = 0,
        True = 1
    }

    /// <summary>
    /// Helper class for Tribool
    /// </summary>
    public static class Triboolean
    {
        public static Tribool ToTribool(this bool value)
        {
            return value ? Tribool.True : Tribool.False;
        }


        public static bool ToBool(this Tribool value)
        {
            return value == Tribool.True;
        }
    }
}
