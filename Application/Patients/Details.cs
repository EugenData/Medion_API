using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Error;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
    public class Details
    {
        public class Query : IRequest<PatientDTO>
        {

            public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Query, PatientDTO>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<PatientDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                
                var patient = await _context.Patients
                .Include(x => x.Meetings)
                .SingleOrDefaultAsync(x => x.Id == request.Id);

                if (patient == null)
                    throw new RestException(HttpStatusCode.NotFound, new { patient = "Not found" });


                var patientToReturn =  _mapper.Map<Patient,PatientDTO>(patient);
                return patientToReturn;
            }
        }
    }
}