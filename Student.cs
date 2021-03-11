using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{
    public class Student:Person
    {

        public string Math { get; set; }
        public string Technology { get; set; }
        public string Physics { get; set; }
        public string Polish { get; set; }
        public string English { get; set; }

        public override string GetInfo()
        {
            return $"{FirstName} {LastName} - Math notes: {Math}.";
        }   
    }
}
