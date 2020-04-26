using Cardiompp.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Cardiompp.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        IDoctorService DoctorService { get; set; }

        public DoctorController(IDoctorService doctorService)
        {
            DoctorService = doctorService ?? throw new ArgumentNullException(nameof(doctorService));
        }

        /// <summary>
        /// Get merchant by crm
        /// </summary>
        /// <param name="crm">Doctor identifier</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{crm}")]
        public async Task<IActionResult> GetByCrm(string crm)
        {
            var response = await DoctorService.GetByCrm(crm);

            if (response.Data == null)
                return NotFound(response);

            return Ok(response);
        }
    }
}