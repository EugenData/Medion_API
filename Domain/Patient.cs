using System;
using System.Collections.Generic;

namespace Domain
{
    public class Patient
    {
        public Guid Id {get; set;}
        public string Patient_name {get;set;}
        public string Patient_surname {get;set;}
        public string Phone {get;set;}
        public DateTime Date {get;set;}
        public DateTime Date_created {get;set;}

        public List<Meeting> Meetings {get;set;}
    }
}