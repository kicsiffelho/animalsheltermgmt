using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public class FosterRoleDecorator : UserRoleDecorator
    {
        public FosterRoleDecorator(IUserRole innerRole) : base(innerRole)
        {
        }

        public override string GetPermissions()
        {
            return _innerRole.GetPermissions();
        }
        public override bool CanShowMyAnimals()
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
