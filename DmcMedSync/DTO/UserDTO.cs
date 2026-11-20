namespace DmcMedSync.DTO
{
    public class UserDTO
    {
        
        
            public int Id { get; set; }
            public string FullName { get; set; } 
            public string Username { get; set; } 
            public string PasswordHash { get; set; }   // never leaves the server
            public string Role { get; set; } 
            public string Email { get; set; } 
            public string PhoneNumber { get; set; } 
            public DateTime CreatedAt { get; set; }
        }
    }

