using System;

namespace Domain
{
    public class Meeting
    {
        public Guid MeetingId { get; set; }
        public string Titel { get; set; }
        public bool isDone { get; set; }
        public DateTime Date { get; set; }
        public string Description {get;set;}


        public Guid PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}