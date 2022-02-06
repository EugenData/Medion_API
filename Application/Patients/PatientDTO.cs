using System;
using System.Collections.Generic;

namespace Application.Patients
{
    public class PatientDTO
    {
        public Guid Id { get; set; }
        public string Patient_name { get; set; }
        public string Patient_surname { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public DateTime Date_created { get; set; }
        public List<MeetingDTO> Meetings { get; set; }
    }
}