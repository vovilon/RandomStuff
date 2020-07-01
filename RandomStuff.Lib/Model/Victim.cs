using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace RandomStuff.Lib.Model
{    public class Victim
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name ="ФИО")]
        [Required(ErrorMessage = "Введите ФИО поциента")]
        public string FullName { get; set; }

        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime BirthDay { get; set; }

        [JsonIgnore]
        public List<Execution> Execution { get; set; }
    }
}
