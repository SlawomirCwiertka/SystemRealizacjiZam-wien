using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System_Realizacji_Zamowien.Models
{
    public class State
    {
        public int Id { get; set; }
        public Order Order { get; set; }
        public Message Message { get; set; }
        public User User { get; set; }
        public Flag Flag{ get; set; }
    }
    public enum Flag
    {
        IsRead,
        IsBlocked,
        IsNotVisible,
        IsVisible
    }
}