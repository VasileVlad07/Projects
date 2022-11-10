using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fitness.Entities
{
    public partial class ClientInformation
    {
        public int ClientId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? PhoneNumber { get; set; }
        public int Hours { get; set; }
        public int InstructorID { get; set; }
        public string? FullName
        {
            get { return LastName + " " + FirstName; }
        }


        [Required]
        public string CategoryString
        {
            get { return this.Categories.ToString(); }
            set { Categories = (Categories)Enum.Parse(typeof(Categories), value, true); } 
        }

        public Categories Categories { get; set; }

    }

    public enum Categories
    {
        Adult = 0,
        Elev = 1,
        Student = 2
    }

}
