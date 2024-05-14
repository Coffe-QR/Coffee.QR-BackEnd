using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.API.DTOs
{
    public enum JobPositionDto
    {
        WAITER,
        BARTENDER,
        CHEF,
        MANAGER
    }
    public class JobApplicationDto
    {
        public long Id { get; set; }
      
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public DateOnly DateOfBirth { get; set; }
        
        public string Address { get; set; }
        public DateOnly ApplicationDate { get; set; }
        
        public long LocalId {get; set; }

        public string ApplicantDescription {  get; set; }
        
        public JobPositionDto Position { get; set; }

    }
}
