using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EE.Ord.Domain.MasterData;
using EE.Ord.Services;
using Microsoft.AspNetCore.Mvc;

namespace EE.Ord.Main.App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }

        [HttpGet]
        public IEnumerable<Patient> Index()
        {
            return _patientsService.RetrieveAllPatients();
        }
    }
}
