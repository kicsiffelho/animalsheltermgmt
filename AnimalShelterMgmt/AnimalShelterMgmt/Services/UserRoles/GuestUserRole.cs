using AnimalShelterMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public class GuestUserRole : IUserRole
    {
        public bool CanAddAnimal()
        {
            return false;
        }

        public bool CanLogout()
        {
            return false;
        }

        public bool CanShowMyAnimals()
        {
            return false;
        }

        public bool CanShowProfile()
        {
            return false;
        }

        public virtual string GetPermissions()
        {
            return "ViewAnimals";
        }

        public virtual string GetRoleName()
        {
            return "Guest";
        }

        public virtual User GetUser()
        {
            return new User
            {
                Auth0Id = "guest",
                Role = "Guest",
                CreatedAt = DateTime.Now
            };
        }
    }
}
