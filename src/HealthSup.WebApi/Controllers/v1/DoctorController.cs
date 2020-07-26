using HealthSup.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HealthSup.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        IDoctorApplicationService DoctorService { get; set; }

        public DoctorController
        (
            IDoctorApplicationService doctorService
        )
        {
            DoctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }
    }
}