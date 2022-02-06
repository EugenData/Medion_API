using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using Hangfire;
using Application.HangFire;
using FluentValidation;
using TimeZoneConverter;

namespace Application.Meetings
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid meetingId { get; set; }
            public string titel { get; set; }
            public DateTime Date { get; set; }
            public bool isDone { get; set; }
            public Guid patientId { get; set; }
            public string Description { get; set; }
        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.titel).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                string jobID = "";
                TimeZoneInfo KievZone = TZConvert.GetTimeZoneInfo("FLE Standard Time");
                var patient = await _context.Patients.FindAsync(request.patientId);
                var reminder = new Reminder();

                

                var dateToRemind = new DateTime(request.Date.Year, request.Date.Month, request.Date.Day, 12, 00, 00);
                var dateNow = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 00);

                TimeSpan timeToRemind = dateToRemind.AddDays(-3) - dateNow;

                if (timeToRemind.TotalHours >= 0)
                {
                    jobID = BackgroundJob.Schedule(() => reminder.DelayReminder(request.titel, request.Date, patient.Patient_name, patient.Patient_surname, patient.Phone), TimeSpan.FromMinutes(timeToRemind.TotalMinutes));
                }


                var meeting = new Meeting
                {
                    MeetingId = request.meetingId,
                    Titel = request.titel,
                    isDone = request.isDone,
                    Date = request.Date,
                    // Date = TimeZoneInfo.ConvertTimeFromUtc(
                    //     new DateTime(
                    //         request.Date.Year,
                    //         request.Date.Month,
                    //         request.Date.Day,
                    //         request.Date.Hour,
                    //         request.Date.Minute,
                    //         00//sec
                    // ), KievZone),

                    PatientId = request.patientId,
                    Description = jobID
                };

                _context.Meetings.Add(meeting);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problems saving changes");
            }
        }
    }
}