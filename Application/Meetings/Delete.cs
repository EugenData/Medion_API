using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using Hangfire;
using MediatR;
using Persistence;

namespace Application.Meetings
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id {get;set;}
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
                var meeting = await _context.Meetings.FindAsync(request.Id);

                if (meeting == null){
                    throw new RestException(HttpStatusCode.NotFound, new { meeting = "Not found" });
                }
                if(meeting.Description != "" ){
                    BackgroundJob.Delete(meeting.Description);
                }
                _context.Remove(meeting);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                throw new Exception("Problems saving changes");
            }
        }
    }
}