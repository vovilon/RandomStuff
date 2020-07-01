using RandomStuff.Lib.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace RandomStuff.WebSite.ViewModel
{
    public class ExecutionListViewModel
    {
        public ExecutionListViewModel()
        { }

        public ExecutionListViewModel(IEnumerable<Execution> executions, IEnumerable<Healer> allHealers, IEnumerable<Victim> allVictims)
        {
            Executions = 
            executions
                .Select(e => new ExecutionDetails 
                            {
                                Id = e.Id,
                                Healer = allHealers.First(h => h.Id == e.HealerId).FullName,
                                Victim = allVictims.First(v => v.Id == e.VictimId).FullName,
                                Time = e.ExecutionTime
                            }).ToList();
        }

        public int VictimId { get; set; }
        public int HealerId { get; set; }

        public IEnumerable<ExecutionDetails> Executions { get; set; }

        [Display(Name = "Дата")]
        [DataType(DataType.Date)]
        public DateTime ChoosenDate { get; set; } = DateTime.Today;
    }
}
