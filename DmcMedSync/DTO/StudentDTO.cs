using System.ComponentModel.DataAnnotations;

namespace DmcMedSync.DTO
{
    //DiagnosisHistory, ImmunizationHistory, MedicationHistory, AllergyHistory, LabResultHistory, VitalSignsHistory
    //A dto for transferring student medical records data between layers of the application
    public class StudentDTO
    {
        //Data transfer object for student medical records
        [Required (ErrorMessage = "Student ID is required.")]
        public int StudentId { get; set; }
        [Required (ErrorMessage = "Full name is required.")]
        public string FullName { get; set; } = string.Empty;
        [Required (ErrorMessage = "Age is required.")]
        public int Age { get; set; }
        [Required (ErrorMessage = "Gender is required.")]
        public string Gender { get; set; } = string.Empty;
        [Required (ErrorMessage = "Grade level is required.")]
        public int GradeLevel { get; set; }
        [Required (ErrorMessage = "Department is required.")]
        public string Department { get; set; } = string.Empty;

        [Required (ErrorMessage = "Contact number is required.")]
       
        public int ContactNumber { get; set; }
        [Required (ErrorMessage = "Address is required.")]
        public string Address { get; set; } = string.Empty;

        [Required (ErrorMessage = "Medical history is required.")]

        public string MedicalHistory { get; set; } = string.Empty;

        
        

        //Guardian Contact
        [Required (ErrorMessage = "Guardian name is required.")]
        public string GuardianName { get; set; } = string.Empty;
        [Required (ErrorMessage = "Phone number is required.")] 
        public int PhoneNumber { get; set; }
        [Required (ErrorMessage = "Home address is required.")]
        public string HomeAddress { get; set; } = string.Empty;
    }
}
