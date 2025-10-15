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

    public class InvalidPasswordResetTokenException : Exception
    {
        public InvalidPasswordResetTokenException() : base("Invalid password reset token.") { }
    }

    public class ExpiredPasswordResetTokenException : Exception
    {
        public ExpiredPasswordResetTokenException() : base("Password reset token has expired.") { }
    }
}