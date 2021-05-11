using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Json.Net.Model
{
    public class Address
    {
        public string Calle { get; set; }
        public int Number { get; set; }
        public City City { get; set; }
    }
}
