namespace DmcMedSync.DTO
{

    //Creating a DTO (Data Transfer Object) for user responses. This class is used to transfer user data between different layers of the application, such as from the service layer to the presentation layer.
    //It contains properties that represent the user's information, including their ID, full name, date of birth, home address, phone number, email, username, and role.
    //The properties are initialized with default values to ensure they are not null when accessed.
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string HomeAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
