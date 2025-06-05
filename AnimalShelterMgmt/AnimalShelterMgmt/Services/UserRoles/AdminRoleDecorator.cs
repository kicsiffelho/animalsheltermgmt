using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public class AdminRoleDecorator : UserRoleDecorator
    {
        public AdminRoleDecorator(IUserRole innerRole) : base(innerRole)
        {
        }
        public override string GetPermissions()
        {
            return _innerRole.GetPermissions();
        }
        public override bool CanAddAnimal()
        {
            return true;
        }
        public override bool CanLogout()
        {
            return true;
        }
        public override bool CanShowProfile()
        {
            return true;
        }
    }
}
