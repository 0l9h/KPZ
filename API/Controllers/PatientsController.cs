using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.ViewModels;
using AutoMapper;
using API.Db;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly DentalClinicContext _context;
        private readonly IMapper _mapper;

        public PatientsController(DentalClinicContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all patients
        /// </summary>
        /// <returns>patients list</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientViewModel>>> GetPatientsAndDiseases()
        {
            var patients = await _context.Patients.ToListAsync();
            return Ok(_mapper.Map<List<PatientViewModel>>(patients));
        }

        /// <summary>
        /// Get specific patient by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>patient</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientViewModel>> GetPatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PatientViewModel>(patient));
        }

        /// <summary>
        /// Alter patient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> AlterPatient(int id, UpdatePatientViewModel patientViewModel)
        {
            if (id != patientViewModel.Id)
            {
                return BadRequest();
            }

            var patient = _mapper.Map<Patient>(patientViewModel);

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PatientExistsAsync(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Create patient
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(UpdatePatientViewModel patientViewModel)
        {
            var patient = _mapper.Map<Patient>(patientViewModel);

            _context.Patients.Add(patient);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        /// <summary>
        /// Delete patient
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> PatientExistsAsync(int id)
        {
            return await _context.Patients.AnyAsync(e => e.Id == id);
        }
    }
}
