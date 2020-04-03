using Microsoft.AspNetCore.Mvc;
using MyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Data
{
    public class DataAccess
    {

        public async Task<int> GetUserCountAsync()
        {
            int lReturn = 0;

            await Task.Run(() =>
            {
                lReturn = 1;
            });

            return lReturn;
        }

        public async Task<TruckLocation> GetTruckLocationAsync()
        {
            TruckLocation temp = new TruckLocation();

            await Task.Run(() =>
            {
                temp.STREET = "1234 Apple Street";
                temp.CITY = "AnyTown";
                temp.STATE = "NE";
                temp.ZIP = "55512";
                temp.START = DateTime.Parse($"{DateTime.Now.ToShortDateString()} 11:00");
                temp.STOP = DateTime.Parse($"{DateTime.Now.ToShortDateString()} 15:00");
            });

            return temp;
        }

        public async Task<List<Attendees>> GetAttendeesAsync()
        {
            List<Attendees> temp = new List<Attendees>();

            await Task.Run(() =>
            {
                temp.Add(new Attendees()
                {
                    ID = 1,
                    NAME = "Terry Turtle",
                    TYPE = Models.Type.Leader
                });

                temp.Add(new Attendees()
                {
                    ID = 2,
                    NAME = "Sammy Squirrel",
                    TYPE = Models.Type.Attendee
                });

                temp.Add(new Attendees()
                {
                    ID = 3,
                    NAME = "Cammie Cat",
                    TYPE = Models.Type.Attendee
                });
            });

            return temp;
        }
    }
}
