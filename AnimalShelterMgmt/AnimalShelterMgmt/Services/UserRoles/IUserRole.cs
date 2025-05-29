using AnimalShelterMgmt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services.UserRoles
{
    public interface IUserRole
    {
        string GetRoleName();
        string GetPermissions();
        User GetUser();

        bool CanAddAnimal();
        bool CanLogout();
        bool CanShowMyAnimals();
        bool CanShowProfile();
    }
}
