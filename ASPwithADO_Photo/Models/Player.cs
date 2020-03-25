using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPwithADO_Photo.Models
{
    public class Player
    {   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Name is required....")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="Name is between 5 and 50 characters")]
        public string Name { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Range(18,35,ErrorMessage ="Age is between 18 and 35 age....")]
        public int Age { get; set; }

        [Required(ErrorMessage ="Position is required.....")]
        public string Position { get; set; }

        public string Photo { get; set; }
    }
}
