namespace Blazorit.Client.Support.Components {
    /// <summary>
    /// Support methods container for components 
    /// </summary>
    public static partial class Methods {
        /// <summary>
        /// Method returns single css class string from component class and other classes 
        /// </summary>
        /// <param name="compClass">Component class</param>
        /// <param name="otherClasses">Other additional classes</param>
        /// <returns></returns>
        public static string GetClass(string? compClass, string? otherClasses) {
            if (string.IsNullOrEmpty(compClass)) {
                if (string.IsNullOrEmpty(otherClasses)) {
                    return string.Empty;
                }
                return otherClasses;
            } else if (string.IsNullOrEmpty(otherClasses)) {
                return $"{compClass}";
            }

            return $"{compClass} {otherClasses}";
        }
    }
}
