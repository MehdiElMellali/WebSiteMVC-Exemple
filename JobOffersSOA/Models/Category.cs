using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JobOffersSOA.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Type job")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Description job")]
        public string CategoryDescription { get; set; }

        public virtual ICollection<Job> jobs { get; set; }
    }
}