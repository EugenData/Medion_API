using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Patients
{
    public class List
    {
        public class Query : IRequest<List<PatientDTO>> { }

        public class Handler : IRequestHandler<Query, List<PatientDTO>>
        {
            private DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
            }

            public async Task<List<PatientDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var patients = await _context.Patients.Include(x => x.Meetings).ToListAsync();
                var patientsToReturn = _mapper.Map<List<Patient>, List<PatientDTO>>(patients);

                return patientsToReturn;
            }
        }
    }
}