using AnimalShelterMgmt.Models;
using AnimalShelterMgmt.Services.UserRoles;

namespace AnimalShelterMgmt
{
    public class OwnerRoleDecorator : UserRoleDecorator
    {
        public OwnerRoleDecorator(IUserRole innerRole) : base(innerRole)
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