using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Data.Models
{
    public class Game
    {
        public IEnumerable<Sentence> Sentences { get; set; }
        public Mode Mode { get; set; }
    }
}
