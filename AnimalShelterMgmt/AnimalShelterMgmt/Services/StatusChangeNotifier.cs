using AnimalShelterMgmt.Services.Observers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalShelterMgmt.Services
{
    public class StatusChangeNotifier : ISubject
    {
        private static readonly StatusChangeNotifier _instance = new();
        public static StatusChangeNotifier Instance => _instance;
        private readonly List<IObserver> _observers = new();
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
