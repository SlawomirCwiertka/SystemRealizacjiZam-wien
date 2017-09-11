using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public virtual Firm Employer { get; set; }
        public ICollection<ItemCard> ShoppingCart { get; set; }
        public virtual Access Access { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string NIP { get; set; }
        public ICollection<Message> Messages{ get; set; }
        public ICollection<State> States{ get; set; }
    }
}