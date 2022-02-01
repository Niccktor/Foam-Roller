using System.Text;
using System;

namespace Core
{
    public static class UserName
    {
        public static string CheckUser(string user)
        {
            if (string.IsNullOrWhiteSpace(user))
                return ("");
            else
                user = user.ToLowerInvariant();

            if (user.Contains("thomas") && user.Contains("beguin"))
                return ("Bonjour patron");
            else if (user.Contains("hugo") && user.Contains("van eeckhout"))
                return ("Bonjour monsieur ECG");
            else if (user.Contains("paul") && user.Contains("palmier"))
                return ("Bonjour monsieur EMG");
            else if (user.Contains("aymen") && user.Contains("kristou"))
                return ("Bonjour monsieur moteur");
            else
                return ("Je ne vous connais pas !!");
        }
    }
}