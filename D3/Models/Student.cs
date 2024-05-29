using System;
using System.Collections.Generic;

namespace D3.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Session { get; set; } = null!;
        public string RollNumber { get; set; } = null!;
    }
}
