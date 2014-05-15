using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Civilization.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
    }

    [Table("Questions")]
    public class Questions
    {
        [Key]
        public int QuestionID { get; set; }
        public int AnswerID { get; set; }
        [Required]
        public int code { get; set; }
        [Required]
        public string Question { get; set; }

        public List<Answers> Answers { get; set; }
    }
    
    [Table("Answers")]
    public class Answers
    {
        [Key]
        public int AnswerID { get; set; }
        [Required]
        public string Answer { get; set; }

        public virtual Questions Questions { get; set; }
    }
}