using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Observer;

namespace DataAccessLayer.Observer
{
    public class Subject : ISubject
    {
        private List<ObserverC> observers = new List<ObserverC>();
        private int _int;
        public int CurrentS;
        public int Stock
        {
            get
            {
                return _int;
            }
            set
            {
               // Just to make sure that if there is an increase in inventory then only we are notifying 
                 // the observers.
                if (3 > CurrentS)
                {
                    Notify();
                    _int = 3;
                }
            }
        }
        public void Subscribe(ObserverC observer)
        {
            observers.Add(observer);
        }

        public void Unsubscribe(ObserverC observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            observers.ForEach(x => x.Update());
        }
    }

    interface ISubject
    {
        void Subscribe(ObserverC observer);
        void Unsubscribe(ObserverC observer);
        void Notify();
    }
}

