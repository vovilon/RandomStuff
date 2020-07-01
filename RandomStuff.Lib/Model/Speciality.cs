using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RandomStuff.Lib.Model
{
    public class Speciality
    {
        public int Id { get; set; }

        public string name { get; set; }

        [JsonIgnore]
        public List<Healer> Healers { get; set; }
    }
}
