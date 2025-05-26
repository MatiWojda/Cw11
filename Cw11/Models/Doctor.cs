using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cw11.Models;

public class Doctor {
    [Key]
    public int IdDoctor { get; set; }
    [Required, MaxLength(100)]
    public string FirstName { get; set; }
    [Required, MaxLength(100)]
    public string LastName { get; set; }
    [MaxLength(100), EmailAddress]
    public string Email { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}