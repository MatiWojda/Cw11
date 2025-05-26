using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cw11.DTOs;

public class PrescriptionRequestDto
{
    public PatientDto Patient { get; set; } = null!;
    public int IdDoctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentRequestDto> Medicaments { get; set; } = new();
}

public class PrescriptionInfoDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public DoctorDto Doctor { get; set; } = null!;
    public List<MedicamentInfoDto> Medicaments { get; set; } = new();
}

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
    public List<PrescriptionInfoDto> Prescriptions { get; set; } = new();
}

public class PatientDto
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime Birthdate { get; set; }
}

public class MedicamentRequestDto
{
    public int IdMedicament { get; set; }
    public int Dose { get; set; }
    public string Description { get; set; } = null!;
}

public class MedicamentInfoDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int Dose { get; set; }
}

public class DoctorDto
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!; 
}