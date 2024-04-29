using Coffee.QR.BuildingBlocks.Core.Domain;

namespace Coffee.QR.Core.Domain
{
    public class User : Entity
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public UserRole Role { get; private set; }
        public bool IsActive { get; set; }

        public User(string username,string email, string password, string firstName, string lastName, UserRole role, bool isActive)
        {
            Username = username;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Role = role;
            IsActive = isActive;
            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Username)) throw new ArgumentException("Invalid Username");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Invalid Password");
        }

        public string GetPrimaryRoleName()
        {
            return Role.ToString().ToLower();
        }

    }
}

public enum UserRole
{
    Bartender,
    Waiter,
    Manager,
    Client,
    ITsupport
}
