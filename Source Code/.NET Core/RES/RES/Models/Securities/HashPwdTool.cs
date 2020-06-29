using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Models.Security
{
    public class HashPwdTool
    {
        /// <summary>
        /// Create hash password from user input password
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns>hasded password which must save to database</returns>
        public static string GeneratePassword(string userPassword)
        {
            userPassword = userPassword + "^XT8~KT.1";

            return BCrypt.HashPassword(userPassword, BCrypt.GenerateSalt());
        }

        /// <summary>
        /// check password which input from user in website and hashed password from database
        /// </summary>
        /// <param name="userEnteredPassword"></param>
        /// <param name="hashedPwdFromDatabase"></param>
        /// <returns>pass word input is true or false</returns>
        public static bool CheckPassword(string userEnteredPassword, string hashedPwdFromDatabase)
        {
            return BCrypt.CheckPassword(userEnteredPassword + "^XT8~KT.1", hashedPwdFromDatabase);
        }
    }
}
