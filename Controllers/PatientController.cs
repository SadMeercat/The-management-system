using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Тестовое_Системы_управления;

[Route("api/[controller]")]
[ApiController]
public class PatientsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PatientsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Patients
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PatientListDto>>> GetPatients([FromQuery] string sortField = "LastName", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Patient
            .Include(p => p.Uparticipation)
            .Select(p => new PatientListDto
            {
                Id = p.Id,
                LastName = p.LastName,
                FirstName = p.FirstName,
                UparticipationNumber = p.Uparticipation.Number
            });

        // Сортировка
        query = sortField switch
        {
            "LastName" => query.OrderBy(p => p.LastName),
            "FirstName" => query.OrderBy(p => p.FirstName),
            "UparticipationNumber" => query.OrderBy(p => p.UparticipationNumber),
            _ => query.OrderBy(p => p.LastName)
        };

        var patients = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(patients);
    }

    // GET: api/Patients/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EditPatientDto>> GetPatient(int id)
    {
        var patient = await _context.Patient.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        return new EditPatientDto
        {
            LastName = patient.LastName,
            FirstName = patient.FirstName,
            MiddleName = patient.MiddleName,
            Address = patient.Address,
            BirthDate = patient.BirthDate,
            Gender = patient.Gender,
            UparticipationId = patient.UparticipationId
        };
    }

    // POST: api/Patients
    [HttpPost]
    public async Task<ActionResult<Patient>> PostPatient(EditPatientDto patientDto)
    {
        var patient = new Patient
        {
            LastName = patientDto.LastName,
            FirstName = patientDto.FirstName,
            MiddleName = patientDto.MiddleName,
            Address = patientDto.Address,
            BirthDate = patientDto.BirthDate,
            Gender = patientDto.Gender,
            UparticipationId = patientDto.UparticipationId
        };

        _context.Patient.Add(patient);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
    }

    // PUT: api/Patients/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPatient(int id, EditPatientDto patientDto)
    {
        var patient = await _context.Patient.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        patient.LastName = patientDto.LastName;
        patient.FirstName = patientDto.FirstName;
        patient.MiddleName = patientDto.MiddleName;
        patient.Address = patientDto.Address;
        patient.BirthDate = patientDto.BirthDate;
        patient.Gender = patientDto.Gender;
        patient.UparticipationId = patientDto.UparticipationId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Patients/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePatient(int id)
    {
        var patient = await _context.Patient.FindAsync(id);

        if (patient == null)
        {
            return NotFound();
        }

        _context.Patient.Remove(patient);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
