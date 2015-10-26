using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlckieBot.Model.Giphy
{
    public class GiphySpamCounter
    {
        public string UserID { get; set; }
        public int GifCounter { get; set; }
        public bool HasBeenWarned { get; set; }        
    }
}
