namespace DmcMedSync.DTO
{
    //Creatine recent diagnosis for a student
    public class CreateDiagnosisDTO : StudentRecordDTO
    {
        public DateTime DateRecord { get; set; }
        public string ChiefComplaint { get; set; } = string.Empty;
        public string Diagnosis { get; set; } = string.Empty;

        public string AttendedBy { get; set; } = string.Empty;


    }
}
