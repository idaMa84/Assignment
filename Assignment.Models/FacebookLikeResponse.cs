using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
  
    public class FacebookLikeResponse
    {
        public List<Data> data { get; set; }
        public Paging paging { get; set; }
    }

    
    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Data
    {
        public string about { get; set; }
        public string description { get; set; }
        public string id { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    

}
