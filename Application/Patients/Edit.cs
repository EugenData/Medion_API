using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Domain;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Patients
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Patient_name { get; set; }
            public string Patient_surname { get; set; }
            public string Phone { get; set; }
            public DateTime? Date { get; set; }
            public DateTime Date_created { get; set; }
            public List<Meeting> Meetings { get; set; }

        }
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Patient_name).NotEmpty();
                RuleFor(x => x.Patient_surname).NotEmpty();
                RuleFor(x => x.Phone).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.Date_created).NotEmpty();
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
                var patient = await _context.Patients.FindAsync(request.Id);
                if (patient == null)
                    throw new RestException(HttpStatusCode.NotFound, new { patient = "Not found" });

                patient.Patient_name = request.Patient_name ?? patient.Patient_name;
                patient.Date = request.Date ?? patient.Date;
                patient.Meetings = request.Meetings ?? patient.Meetings;
                patient.Patient_surname = request.Patient_surname;
                patient.Phone = request.Phone;
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problems saving(edit) changes");
            }
        }
    }
}