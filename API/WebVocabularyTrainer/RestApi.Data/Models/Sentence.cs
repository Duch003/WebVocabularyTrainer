using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestApi.Data.Models
{
    public class Sentence
    {
        [Required]
        [Key]
        public int ID { get; set; }

        [Required]
        [RegularExpression(@"\S+")]
        public string Foreign { get; set; }

        [Required]
        [RegularExpression(@"\S+")]
        public string Primary { get; set; }

        public string Description { get; set; }
        public string Examples { get; set; }

        [NotMapped]
        public string[] ExamplesArray 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Examples))
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
        [RegularExpression(@"\S+")]
        public string Subject { get; set; }
    }
}
