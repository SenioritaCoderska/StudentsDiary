using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{
    public class Student : Person
    {
        public Student()
        {
            //Address = new Address();
        }

        [Display(Name ="Class Number",Order =5)]
        public string GroupId { get; set; }
        [Display(Name = "Mathematics", Order = 7)]
        public string Math { get; set; }
        [Display(Name = "Technology", Order = 8)]
        public string Technology { get; set; }
        [Display(Name = "Physics", Order = 9)]
        public string Physics { get; set; }
        [Display(Name = "Polish Language", Order = 10)]
        public string Polish { get; set; }
        [Display(Name = "English Language", Order = 11)]
        public string English { get; set; }
        [Display(Name = "Additional Classes", Order = 6)]
        public bool AdditionalClasses { get; set;}
        //public Address Address { get; set; }

        public override string GetInfo()
        {
            return $"{FirstName} {LastName} - Math notes: {Math}.";
        }
    }
}
