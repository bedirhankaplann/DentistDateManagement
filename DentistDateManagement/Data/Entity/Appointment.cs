using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DentistDateManagement.Data.Entity
{
    public class Appointment
    {
        public int Id { get; set; }

        /// <summary>
        /// AppUser ile 1-n ilişki olduğunu anlayacak.  USER ID FK
        /// </summary>
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PatientName { get; set; }
        public string PatientSurrname { get; set; }
        public string Description { get; set; }
    }
}
