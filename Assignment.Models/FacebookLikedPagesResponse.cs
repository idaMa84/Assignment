using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Models
{
  
    public class FacebookLikedPagesResponse
    {
        public List<Data> Data { get; set; }
        public Paging Paging { get; set; }
    }    
    public class Cursors
    {
        public string Before { get; set; }
        public string After { get; set; }
    }
    public class Data
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public string Description { get; set; }
        
    }
    public class Paging
    {
        public Cursors Cursors { get; set; }
        public string Next { get; set; }
    }    
}
