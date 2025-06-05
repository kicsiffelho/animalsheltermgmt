using AnimalShelterMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public class BaseUserRole : IUserRole
    {
        private readonly User _user;

        public BaseUserRole(User user)
        {
            _user = user;
        }

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
            return _user.Role;
        }

        public User GetUser()
        {
            return _user;
        }

        
    }
}
