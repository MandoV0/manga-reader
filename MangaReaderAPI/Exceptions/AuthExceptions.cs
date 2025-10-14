namespace MangaReaderAPI.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException(string email) : base($"Email '{email}' is already in use.") { }
    }

    public class UsernameAlreadyExistsException : Exception
    {
        public UsernameAlreadyExistsException(string username) : base($"Username '{username}' is already in use.") { }
    }

    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("Invalid email or password") { }
    }
}