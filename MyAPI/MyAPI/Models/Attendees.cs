using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.Models
{
    public enum Type
    {
        Attendee = 0,
        Leader = 1
    }
    public class Attendees
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        public Type TYPE { get; set; }
    }
}
