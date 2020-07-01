using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace RandomStuff.Lib.Model
{
    public class Execution
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int Id { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Время посещения")]
        public DateTime ExecutionTime { get; set; }

        [Display(Name = "Врач")]
        public int HealerId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(HealerId))]
        public Healer Healer { get; set; }

        [Display(Name = "Поциэнт")]
        public int VictimId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(VictimId))]
        public Victim Victim { get; set; }        
    }
}
