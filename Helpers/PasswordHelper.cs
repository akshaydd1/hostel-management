namespace HostelManagementApi.Helpers
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Hashes a plain text password using BCrypt.
        /// </summary>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verifies a plain text password against a hashed password.
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}
