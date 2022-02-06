using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Patients;
using Application.Meetings;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Application.HangFire;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{

    public class PatientsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> List()
        {
            return await Mediator.Send(new List.Query());
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<PatientDTO>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }
        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Application.Patients.Create.Command command)
        {
            return await Mediator.Send(command);
        }
        [HttpPut("meeting/edit/{id}")]
        public async Task<ActionResult<Unit>> Edit(Guid id, Application.Meetings.Edit.Command command)
        {
            
            command.MeetingId = id;
            return await Mediator.Send(command);
        }
        [HttpPost("meeting/create/{id}")]
        public async Task<ActionResult<Unit>> Create(Guid id, Application.Meetings.Create.Command command)
        {
            command.patientId = id;
            return await Mediator.Send(command);
        }
        [HttpPut("edit/{id}")]
        public async Task<ActionResult<Unit>> EditPatient(Guid id, Application.Patients.Edit.Command command)
        {

            command.Id = id;
            return await Mediator.Send(command);
        }
        [HttpDelete("meetings/{id}")]
        public async Task<ActionResult<Unit>> Meeting_Delete(Guid id)
        {

            return await Mediator.Send(new Application.Meetings.Delete.Command {Id=id});
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {

            return await Mediator.Send(new Application.Patients.Delete.Command { Id = id});
        }
        
    }
}