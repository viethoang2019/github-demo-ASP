using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace FrameWorkASPwithADO.Models
{
    public class FWStudent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("StudentID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student Name is required...")]
        [StringLength(20)]
        [DisplayName("StudentName")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is required...")]
        [StringLength(50)]
        [DataType(DataType.MultilineText)]
        [DisplayName("StudentAddress")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is required...")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Birthday is required...")]
        [StringLength(20)]
        public string Birthday { get; set; }
    }
}