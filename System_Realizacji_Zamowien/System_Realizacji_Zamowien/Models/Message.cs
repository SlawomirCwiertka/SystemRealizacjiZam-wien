using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System_Realizacji_Zamowien.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<State> States{ get; set; }
    }
}
