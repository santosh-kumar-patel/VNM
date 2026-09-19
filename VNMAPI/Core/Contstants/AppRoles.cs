using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contstants
{
    public static class AppRoles
    {
        public const string Admin = "Admin";
        public const string Manager = "Manager";
        public const string User = "User";
        public const string Guest = "Guest";

        // Optional: Grouped access levels
        public static readonly string[] ElevatedRoles = { Admin, Manager };
        public static readonly string[] AllRoles = { Admin, Manager, User, Guest };
    }

}
