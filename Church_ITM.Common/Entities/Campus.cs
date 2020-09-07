using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Church_ITM.Common.Entities
{
     public class Campus
     {
          public int Id { get; set; }

          [MaxLength(50, ErrorMessage = "The filed {0} must contain less than {1} characteres ")]
          [Required]
          public string Name { get; set; }

          public ICollection<District> Districs { get; set; }

          [DisplayName("Districs Number")]
          public int DistricsNumber => Districs == null ? 0 : Districs.Count;

          public int IdDistrict { get; set; }
          public object Churchs { get; set; }
     }

}
