using AnimalShelterMgmt.Models;

namespace AnimalShelterMgmt.Stores
{
    public class UserStore
    {
        public User? CurrentUser { get; set; }

        private static UserStore? _instance;
        public static UserStore Instance => _instance ??= new UserStore();
    }
}
