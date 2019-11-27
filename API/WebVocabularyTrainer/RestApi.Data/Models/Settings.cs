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
        public List<string> Subjects { get; set; }
        public List<string> Sources { get; set; }

        public Settings()
        {
            Subjects = new List<string>();
            Subjects.Add("All");
            Sources = new List<string>();
            Sources.Add("All");
        }

        public Settings(List<string> subjects, List<string> sources)
        {
            if(subjects is null || !subjects.Any())
            {
                Subjects = new List<string>();
            }
            else
            {
                Subjects = subjects;
            }
            Subjects.Add("All");

            if (sources is null || !sources.Any())
            {
                Sources = new List<string>();
            }
            else
            {
                Sources = sources;
            }
            Sources.Add("All");
        }
    }
}
