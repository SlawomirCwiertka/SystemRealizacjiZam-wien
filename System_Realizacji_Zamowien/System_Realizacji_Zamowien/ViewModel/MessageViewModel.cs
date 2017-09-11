using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace System_Realizacji_Zamowien.ViewModel
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        [Display(Name="Zawartość")]
        public string Content { get; set; }
        [Display(Name = "Data")]
        public DateTime Date { get; set; }
        public string Url { get; set; }
        public bool IsRead { get; set; }
    }
}
