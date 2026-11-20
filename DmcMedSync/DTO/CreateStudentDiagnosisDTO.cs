using System.ComponentModel.DataAnnotations;

namespace DmcMedSync.DTO
{
    //Creating recent diagnosis for a student
    public class CreateStudentDiagnosis : StudentDTO
    {
        [Required (ErrorMessage = "Date of record is required.")]
        public DateTime DateRecord { get; set; }
       
        [Required (ErrorMessage = "Cannot be empty.")]
        public string ChiefComplaint { get; set; }
        [Required (ErrorMessage = "Cannot be empty.")]
        public string Diagnosis { get; set; }
        [return: Required (ErrorMessage = "Cannot be empty.")]
        public string AttendedBy { get; set; }


    }
}
