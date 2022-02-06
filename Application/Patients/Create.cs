using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;
using FluentValidation;

namespace Application.Patients
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Patient_name { get; set; }
            public string Patient_surname { get; set; }
            public string Phone { get; set; }
            public DateTime Date { get; set; }
            public DateTime Date_created { get; set; }
            public List<Meeting> Meetings { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x=>x.Patient_name).NotEmpty();
                RuleFor(x=>x.Patient_surname).NotEmpty();
                RuleFor(x=>x.Phone).NotEmpty();
                RuleFor(x=>x.Date).NotEmpty();
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
                var patient = new Patient
                {
                    Id = request.Id,
                    Patient_name = request.Patient_name,
                    Patient_surname = request.Patient_surname,
                    Date_created = DateTime.Today,
                    Phone = request.Phone,
                    Date = request.Date,
                    Meetings = request.Meetings
                };
                _context.Patients.Add(patient);
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problems saving changes");
            }
        }
    }
}