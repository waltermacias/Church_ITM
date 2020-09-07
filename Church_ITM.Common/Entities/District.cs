using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Text;

namespace Church_ITM.Common.Entities
{
     public class District
     {
          public readonly object Campuses;

          public int Id { get; set;}

          [MaxLength(50, ErrorMessage = "The filed {0} must contain less than {1} characteres ")]
          [Required]
          public string Name { get; set; }
          public ICollection<Church> Churchs { get; set; }

          [DisplayName("Churchs Number")]
          public int ChurchsNumber => Churchs == null ? 0 : Churchs.Count;

          [JsonIgnore]
          [NotMapped]
          public int IdCampus { get; set; }
     }
}