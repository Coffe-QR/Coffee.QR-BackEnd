using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class JobApplication : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }   
        public string Phone {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Address { get; set; }
        public DateOnly ApplicationDate {  get; set; }

        public JobApplication(string firstName, string lastName, string email, string phone, DateOnly dateOfBirth, string address, DateOnly applicationDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            DateOfBirth = dateOfBirth;
            Address = address;
            ApplicationDate = applicationDate;
        }
    }
}
