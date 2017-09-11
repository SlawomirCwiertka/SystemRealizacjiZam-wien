using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class Access
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public ICollection<User> Users { get; set; }
    }
}