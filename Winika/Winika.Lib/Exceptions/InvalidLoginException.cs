namespace Winika.Lib.Exceptions
{
    /// <summary>
    /// Exception representing an error during the login process.
    /// </summary>
    public class InvalidLoginException : System.Exception
    {
        /// <summary>
        /// Initializes a new <see cref="InvalidLoginException"/> instance.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public InvalidLoginException(string message) : base(message) { }
    }
}
