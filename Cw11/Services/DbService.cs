using Microsoft.EntityFrameworkCore;
using Cw11.Data;
using Cw11.DTOs;
using Cw11.Models;

namespace Cw11.Services;

public class DbService : IDbService {
    private readonly DatabaseContext _ctx;
    public DbService(DatabaseContext ctx) {
        _ctx = ctx;
    }

    public async Task<int> AddPrescriptionAsync(PrescriptionRequestDto dto)
        {
            if (dto.Medicaments.Count > 10)
                throw new Exception("Recepta może zawierać maksymalnie 10 leków");

            if (dto.DueDate < dto.Date)
                throw new Exception("DueDate musi być większy lub równy Date");

            var patient = await _ctx.Patients
                .FirstOrDefaultAsync(p => p.FirstName == dto.Patient.FirstName &&
                                          p.LastName == dto.Patient.LastName &&
                                          p.Birthdate == dto.Patient.Birthdate);
            if (patient == null)
            {
                patient = new Patient
                {
                    FirstName = dto.Patient.FirstName,
                    LastName = dto.Patient.LastName,
                    Birthdate = dto.Patient.Birthdate
                };
                _ctx.Patients.Add(patient);
                await _ctx.SaveChangesAsync();
            }

            foreach (var m in dto.Medicaments)
            {
                if (!await _ctx.Medicaments.AnyAsync(x => x.IdMedicament == m.IdMedicament))
                    throw new Exception($"Lek ID {m.IdMedicament} nie istnieje");
            }

            var prescription = new Prescription
            {
                Date = dto.Date,
                DueDate = dto.DueDate,
                IdDoctor = dto.IdDoctor,
                IdPatient = patient.IdPatient
            };
            _ctx.Prescriptions.Add(prescription);
            await _ctx.SaveChangesAsync();

            foreach (var m in dto.Medicaments)
            {
                _ctx.PrescriptionMedicaments.Add(new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    IdPrescription = prescription.IdPrescription,
                    Dose = m.Dose,
                    Description = m.Description
                });
            }

            await _ctx.SaveChangesAsync();
            return prescription.IdPrescription;
        }

        public async Task<PatientResponseDto?> GetPatientDetailsAsync(int idPatient)
        {
            var patient = await _ctx.Patients
                .Include(p => p.Prescriptions)
                    .ThenInclude(r => r.PrescriptionMedicaments)
                        .ThenInclude(pm => pm.Medicament)
                .Include(p => p.Prescriptions)
                    .ThenInclude(r => r.Doctor)
                .FirstOrDefaultAsync(p => p.IdPatient == idPatient);

            if (patient == null) return null;

            return new PatientResponseDto
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate,
                Prescriptions = patient.Prescriptions
                    .OrderBy(p => p.DueDate)
                    .Select(p => new PrescriptionInfoDto
                    {
                        IdPrescription = p.IdPrescription,
                        Date = p.Date,
                        DueDate = p.DueDate,
                        Doctor = new DoctorDto
                        {
                            IdDoctor = p.Doctor.IdDoctor,
                            FirstName = p.Doctor.FirstName,
                            LastName = p.Doctor.LastName,
                            Email = p.Doctor.Email
                        },
                        Medicaments = p.PrescriptionMedicaments.Select(pm => new MedicamentInfoDto
                        {
                            IdMedicament = pm.Medicament.IdMedicament,
                            Name = pm.Medicament.Name,
                            Description = pm.Description,
                            Dose = pm.Dose
                        }).ToList()
                    }).ToList()
            };
        }
}