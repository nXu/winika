namespace Winika.Lib.VersionTools
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Tool for reading the version of an assembly-
    /// </summary>
    public static class VersionReader
    {
        /// <summary>
        /// Gets the version of the current assembly.
        /// </summary>
        /// <returns>Version of the current assembly.</returns>
        public static Version GetVersion()
        {
            return GetVersion(Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Gets the version of a given assembly.
        /// </summary>
        /// <param name="assembly">Assembly to get the version of.</param>
        /// <returns>Version of the provided assembly.</returns>
        public static Version GetVersion(Assembly assembly)
        {
            return assembly.GetName().Version;
        }
    }
}
