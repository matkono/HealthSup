﻿using Cardiompp.Application.DataContracts.v1.Requests.Doctor;
using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Cardiompp.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        IDoctorApplicationService DoctorService { get; set; }

        public DoctorController(IDoctorApplicationService doctorService)
        {
            DoctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }
    }
}