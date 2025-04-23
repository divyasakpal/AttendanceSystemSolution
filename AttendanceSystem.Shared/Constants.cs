using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceSystem.Shared
{
    public static class Constants
    {
        public const string JwtKey = "Jwt:Key";
        public const string JwtIssuer = "Jwt:Issuer";
        public const string JwtAudience = "Jwt:Audience";
        public const string JwtExpiryPeriodInDays = "Jwt:ExpiryPeriodInDays";

        public const string ProjectName = "Employee";
        public const string Authorization = "Authorization";
        public const string Oauth2 = "Oauth2";
        public const string RequireDigit = "RequireDigit";
        public const string RequireLowercase = "RequireLowercase";
        public const string RequireNonAlphanumeric = "RequireNonAlphanumeric";
        public const string RequireUppercase = "RequireUppercase";
        public const string RequiredLength = "RequiredLength";
        public const string RequiredUniqueChars = "RequiredUniqueChars";

        public const string ErrorIncorrectUserName = "Username is incorrect";
        public const string ErrorUnauthorizedUser = "User is not Authorized";
        public const string ErrorIncorrectPassword = "Password is incorrect";
        public const string ErrorSomethingWentWrong = "something went wrong.";
        public const string ErrorOccured = "Error occured while processing the request";
        public const string ErrorOccuredCheckLogs = "Error occured while processing the request,please check the log file for further information.";
        public const string ErrorNoRequestedData = "Requested data was not found.";
        public const string UserRegistered = "User was registered.";



    }
}
