using RandomStuff.Lib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RandomStuff.WebSite.ViewModel
{
    public class ExecutionDetails
    {
        public int Id { get; set; }

        [Display(Name="Дохтур")]
        public string Healer { get; set; }
        
        [Display(Name="Больной")]
        public string Victim { get; set; }

        [Display(Name = "Прием")]
        public DateTime Time { get; set; }

        public class ExecutionBilder
        {
            private readonly int _id;
            private DateTime _time;
            private string _healer;
            private string _victim;

            public ExecutionBilder(int id)
            {
                _id = id;
            }

            public ExecutionBilder WithHealer(int id, IEnumerable<Healer> healers)
            {
                return WithHealer(healers.FirstOrDefault(h => h.Id == id)?.FullName);
            }

            public ExecutionBilder WithHealer(string name)
            {
                _healer = name;
                return this;
            }

            public ExecutionBilder WithVictim(int id, IEnumerable<Victim> victims)
            {
                return WithVictim(victims.First(h => h.Id == id).FullName);
            }

            public ExecutionBilder WithVictim(string name)
            {
                _victim = name;
                return this;
            }

            public ExecutionBilder OnTime(DateTime time)
            {
                _time = time;
                return this;
            }

            public ExecutionDetails Build()
            {
                return new ExecutionDetails
                {
                    Id = _id,
                    Healer = _healer,
                    Victim = _victim,
                    Time = _time
                };
            }
        }
    }
}
