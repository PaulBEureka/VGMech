using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCryptNet = BCrypt.Net.BCrypt; // If this has error, run this in package manager console: Install-Package BCrypt.Net

namespace VisualMech.Classes
{
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            // Generate a salt
            string salt = BCryptNet.GenerateSalt();

            // Hash the password with the salt
            string hashedPassword = BCryptNet.HashPassword(password, salt);

            return hashedPassword;
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            // Verify the password against the hashed password
            return BCryptNet.Verify(password, hashedPassword);
        }


        
    }
}