using System;
using System.Collections.Generic;
using System.Text;

namespace Test_Json.Net.Model
{
    public class Contact
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
