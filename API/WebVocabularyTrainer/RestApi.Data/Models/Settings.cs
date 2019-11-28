using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApi.Data.Models
{
    public class Settings
    {
        public uint PhrasesUpperLimit { get; set; } = 20;
        public uint Repeats { get; set; } = 3;
        public Mode Mode { get; set; } = Mode.Random;
        public List<string> Subjects { get; set; } = new List<string>();
        public List<string> Sources { get; set; } = new List<string>();

        public Settings() { }

        public Settings(List<string> subjects, List<string> sources)
        {
            Subjects = subjects;
            Sources = sources;
        }
    }
}
