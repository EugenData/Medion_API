using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using MediatR;
using Persistence;

namespace Application.Patients
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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

                _context.Remove(patient);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}