using Cw11.DTOs;

namespace Cw11.Services;

public interface IDbService {
    Task<int> AddPrescriptionAsync(PrescriptionRequestDto dto);
    Task<PatientResponseDto?> GetPatientDetailsAsync(int idPatient);
}
