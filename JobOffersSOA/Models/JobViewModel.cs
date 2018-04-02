using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JobOffersSOA.Models
{
    public class JobViewModel
    {
        public string TitleJob { get; set; }
        public IEnumerable<ApplyForJob> Items { get; set; }
    }
}