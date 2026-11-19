namespace DmcMedSync.DTO
{
    //Creatine recent diagnosis for a student
    public class CreateDiagnosisDTO : StudentRecordDTO
    {
        public DateTime DateRecord { get;set }
        public string ChiefComplaint { get; set }
        public string Diagnosis { get;set }
        
        public string AttendedBy { get; set; }


    }
}
