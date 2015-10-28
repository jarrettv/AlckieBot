using AlckieBot.Model.GroupMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Model
{
    public class Quote
    {
        public string Member { get; set; }
        public DateTime SavedAt { get; set; }
        public string Message { get; set; }
    }
}
