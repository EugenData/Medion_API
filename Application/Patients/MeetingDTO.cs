using System;

namespace Application.Patients
{
    public class MeetingDTO
    {
        public Guid MeetingId {get;set;}
         public string Titel { get; set; }
        public bool isDone { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }
}