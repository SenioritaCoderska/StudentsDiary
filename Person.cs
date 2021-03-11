using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{

    public interface IPerson
    {
        string GetInfo();
    }
    public abstract class Person
    {
        [Display(Name = "Id", Order = 1)]
        public int Id { get; set; }
        [Display(Name = "First Name", Order = 2)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name", Order = 3)]
        public string LastName { get; set; }
        [Display(Name = "Comments", Order = 4)]
        public string Comments { get; set; }
        public abstract string GetInfo();
    }
}
