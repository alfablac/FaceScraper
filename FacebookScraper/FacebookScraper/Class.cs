using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacebookScraper
{
    public class Datum
    {
        public string name { get; set; }
        public string id { get; set; }
        public bool administrator { get; set; }
    }

    public class Cursors
    {
        public string before { get; set; }
        public string after { get; set; }
    }

    public class Paging
    {
        public Cursors cursors { get; set; }
        public string next { get; set; }
    }

    public class RootObject
    {
        public List<Datum> data { get; set; }
        public Paging paging { get; set; }
    }
}
