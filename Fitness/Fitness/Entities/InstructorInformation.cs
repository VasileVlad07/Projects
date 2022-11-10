using System;
using System.Collections.Generic;

namespace Fitness.Entities
{
    public partial class InstructorInformation
    {
        public int UserId { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? InstructorId { get; set; }
        public int Hours { get; set; }
    }
}
