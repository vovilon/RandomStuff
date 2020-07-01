using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using System.Web.Mvc;

namespace RandomStuff.Lib.Model
{
    public class Healer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "ФИО")]
        [Required(ErrorMessage = "Введите имя врача")]
        public string FullName { get; set; }

        [Display(Name = "Специальность")]
        public int SpecialityId { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(SpecialityId))]
        public Speciality Speciality { get; set;}

        [JsonIgnore]
        public List<Execution> Execution { get; set; }
    }
}
