using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_homework.Models
{
    public class KeepAccountsViewModel
    {
        public int Type { get; set; }
        public DateTime Date { get; set; }
        public decimal Money { get; set; }
    }
}