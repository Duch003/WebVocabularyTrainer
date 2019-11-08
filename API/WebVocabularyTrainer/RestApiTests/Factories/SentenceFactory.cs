using RestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestApiTests.Factories
{
    public static class SentenceFactory
    {
        private static object _lock = new object();
        private static IQueryable<Sentence> _instance = null;
        public static IQueryable<Sentence> GetSentences()
        {
            lock (_lock)
            {
                if(_instance == null)
                {
                    var entries = new List<Sentence>();
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "House",
                        Primary = "Dom",
                        Description = "The place where person lives daily.",
                        Examples = "My home is where's my bed is.",
                        LevelOfRecognition = 0.7,
                        Subject = "Daily",
                        Source = "Dictinary",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Spoon",
                        Primary = "Łyżka",
                        Description = "A thing often used to eat soups and liquid dishes.",
                        Examples = "Sorry! I have spilled some soup from my spoon!;Spoons are Cadabra's main attribute.",
                        LevelOfRecognition = 0.23,
                        Subject = "Kitchen",
                        Source = "Dictinary",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Fork",
                        Primary = "Widelec",
                        Description = "A thing often used to eat solid dishes.",
                        Examples = "I have bought a bunch of new forks.",
                        LevelOfRecognition = 1,
                        Subject = "Kitchen",
                        Source = "Dictinary",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Oven",
                        Primary = "Piekarnik",
                        Description = "Baking machine.",
                        Examples = "I have buns in the oven.",
                        LevelOfRecognition = 0.34,
                        Subject = "Kitchen",
                        Source = "Dictinary",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Fridge",
                        Primary = "Lodówka",
                        Description = null,
                        Examples = null,
                        LevelOfRecognition = 0.92,
                        Subject = "Kitchen",
                        Source = "Dictinary",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Table",
                        Primary = "Stół",
                        Description = "During sunday's dinners the whole family met sitting in front of that.",
                        Examples = "Can you set the table, please?;This table is built of mahogany.",
                        LevelOfRecognition = 0,
                        Subject = "Kitchen",
                        Source = "Your house in a nutshell.",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Table",
                        Primary = "Biurko",
                        Description = "A furniture designed to work on it.",
                        Examples = "He is doing he's homework on the table.",
                        LevelOfRecognition = 0.1,
                        Subject = "Office",
                        Source = "Your house in a nutshell.",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Computer",
                        Primary = "Komputer",
                        Description = null,
                        Examples = "I am software engineer so I am working on computers.",
                        LevelOfRecognition = 0.12,
                        Subject = "Office",
                        Source = "Daily work",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Chair",
                        Primary = "Krzesło",
                        Description = "Furtniture on which people are sitting.",
                        Examples = "Look, how I restored this chair. Is not it beautiful?;Oh man, I was really. I needed to sit for a while.",
                        LevelOfRecognition = 0.87,
                        Subject = "Office",
                        Source = "Daily work",
                        AttemptsLeft = 0
                    });
                    entries.Add(new Sentence
                    {
                        ID = 0,
                        Foreign = "Dinning room",
                        Primary = "Jadalnia",
                        Description = "Part of house where people eat's their food.",
                        Examples = null,
                        LevelOfRecognition = 0.5,
                        Subject = "Kitchen",
                        Source = "Your house in a nutshell.",
                        AttemptsLeft = 0
                    });

                    _instance = new List<Sentence>(entries).AsQueryable();
                }
                return _instance;
            }
        }
    }
}
