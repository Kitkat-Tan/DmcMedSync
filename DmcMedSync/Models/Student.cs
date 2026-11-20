namespace DmcMedSync.Models
{
    public class Student
    {
        //holds the student record and their information

               public int Id { get; set; }
        public int StudentId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public int GradeLevel { get; set; }
        public string Department { get; set; } = string.Empty;
        public int ContactNumber { get; set; }
        public string Address { get; set; } = string.Empty;

        public string MedicalHistory { get; set; } = string.Empty;


        //Guardian Contact

        public string GuardianName { get; set; } = string.Empty;
        public int PhoneNumber { get; set; }
        public string HomeAddress { get; set; } = string.Empty;
    }
}
