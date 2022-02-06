using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.Meetings
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid MeetingId { get; set; }
            public string Titel { get; set; }
            public bool? isDone { get; set; }
            public DateTime? Date { get; set; }


            public Guid PatientId { get; set; }
            
        }
         public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Titel).NotEmpty();
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
                var meeting = await _context.Meetings.FindAsync(request.MeetingId);

                if(meeting == null){
                    throw new Exception("Could not find meeting");
                }
                meeting.Date = request.Date ?? meeting.Date;
                meeting.Titel = request.Titel ?? meeting.Titel;
                meeting.isDone = request.isDone ?? meeting.isDone;
                

                    
                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problems saving changes");
            }


        }
    }
}