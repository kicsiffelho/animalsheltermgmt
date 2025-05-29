using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services
{
    internal class SessionService
    {
        public string Auth0UserId { get; set; }

        private static SessionService _instance;
        public static SessionService Instance => _instance ??= new SessionService();
    }
}
