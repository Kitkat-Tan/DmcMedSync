namespace DmcMedSync.DTO
{


    //creating list of existing student
    //for updating and deleting records of student medical history
    public class ClinicRecordService
    {
        public List<CreateDiagnosisDTO> DiagnosisHistory { get; set; } = new();
    }
}
