using AnimalShelterMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public abstract class UserRoleDecorator : IUserRole
    {
        protected IUserRole _innerRole;

        protected UserRoleDecorator(IUserRole innerRole)
        {
            _innerRole = innerRole;
        }

        public virtual bool CanAddAnimal()
        {
            return false;
        }

        public virtual bool CanLogout()
        {
            return false;
        }

        public virtual bool CanShowMyAnimals()
        {
            return false;
        }

        public virtual bool CanShowProfile()
        {
            return false;
        }

        public virtual string GetPermissions()
        {
            return _innerRole.GetPermissions();
        }

        public virtual string GetRoleName()
        {
            return _innerRole.GetRoleName();
        }

        public virtual User GetUser()
        {
            return _innerRole.GetUser();
        }
    }
}
