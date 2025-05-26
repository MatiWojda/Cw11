using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cw11.Models;

public class PrescriptionMedicament {
    [Key, Column(Order = 0)]
    public int IdMedicament { get; set; }
    public Medicament Medicament { get; set; }

    [Key, Column(Order = 1)]
    public int IdPrescription { get; set; }
    public Prescription Prescription { get; set; }

    [Required]
    public int Dose { get; set; }
    [MaxLength(100)]
    public string Description { get; set; }
}