
namespace Kalio.Common
{
    public static class Permissions
    {
        public const string WeatherReadAccess = "weather.access.read";
        public const string RetrieveRolesAccess = "roles.read";
        public const string CreateRoleAccess = "roles.write";
        public const string UpdateRoleAccess = "role.update";
        public const string DeleteRoleAccess = "role.remove";
        

        public const string CreateUserAccess = "user.write";
        public const string RetrieveUserAccess = "users.read";
        public const string UpdateUserAccess = "user.update";
        public const string DeleteUserAccess = "user.remove";
        public const string GetUserClaimAccess = "user.claims.read";
        public const string UpdateUserClaimAccess = "user.claim.update";

        public const string InitiateForgetPasswordAccess = "forgetpassword.init";
        public const string ResetPasswordAccess = "password.reset";
        public const string AddUserInRoleAccess = "user.adduserinrole";

        public const string CreateClaimAccess = "claim.write";
        public const string RetrieveClaimsAccess = "claims.read";
        public const string UpdateClaimAccess = "claim.update";
        public const string DeleteClaimAccess = "claim.remove";

    }
}



