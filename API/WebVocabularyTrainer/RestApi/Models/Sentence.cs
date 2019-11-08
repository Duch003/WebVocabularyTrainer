using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Models
{
    public class Sentence
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Foreign { get; set; }

        [Required]
        public string Primary { get; set; }

        public string Description { get; set; }
        public string Examples { get; set; }

        [NotMapped]
        public string[] ExamplesArray 
        {
            get
            {
                if (string.IsNullOrEmpty(Examples))
                {
                    return null;
                }
                else
                {
                    return Examples.Split(";");
                }
            }
            set
            {
                Examples = string.Join(';', value);
            }
        }

        [Range(0, 1)]
        public double LevelOfRecognition { get; set; }

        [NotMapped]
        public int AttemptsLeft { get; set; }

        public string Source { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}
