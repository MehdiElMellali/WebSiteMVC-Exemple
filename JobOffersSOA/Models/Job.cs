using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JobOffersSOA.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Job Title")]
        public string JobTitle { get; set; }

        [AllowHtml]
        [Required]
        [Display(Name = "Job Content")]
        public string JobContent { get; set; }

        [Display(Name = "Job image")]
        public string JobImage { get; set; }

        [Required]
        [Display(Name = "Job Category")]
        public int  CategoryId { get; set; }

        public string UserId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}