using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Тестовое_Системы_управления;

[Route("api/[controller]")]
[ApiController]
public class DoctorsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public DoctorsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/Doctors
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DoctorListDto>>> GetDoctors([FromQuery] string sortField = "FullName", [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var query = _context.Doctor
            .Include(d => d.Cabinet)
            .Include(d => d.Specialization)
            .Include(d => d.Uparticipation)
            .Select(d => new DoctorListDto
            {
                Id = d.Id,
                FullName = d.FullName,
                CabinetNumber = d.Cabinet.Number,
                SpecializationName = d.Specialization.Name,
                UparticipationNumber = d.Uparticipation != null ? d.Uparticipation.Number : null
            });

        // Сортировка
        query = sortField switch
        {
            "FullName" => query.OrderBy(d => d.FullName),
            "CabinetNumber" => query.OrderBy(d => d.CabinetNumber),
            "SpecializationName" => query.OrderBy(d => d.SpecializationName),
            _ => query.OrderBy(d => d.FullName)
        };

        var doctors = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return Ok(doctors);
    }

    // GET: api/Doctors/5
    [HttpGet("{id}")]
    public async Task<ActionResult<EditDoctorDto>> GetDoctor(int id)
    {
        var doctor = await _context.Doctor.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        return new EditDoctorDto
        {
            FullName = doctor.FullName,
            CabinetId = doctor.CabinetId,
            SpecializationId = doctor.SpecializationId,
            UparticipationId = doctor.UparticipationId
        };
    }

    // POST: api/Doctors
    [HttpPost]
    public async Task<ActionResult<Doctor>> PostDoctor(EditDoctorDto doctorDto)
    {
        var doctor = new Doctor
        {
            FullName = doctorDto.FullName,
            CabinetId = doctorDto.CabinetId,
            SpecializationId = doctorDto.SpecializationId,
            UparticipationId = doctorDto.UparticipationId
        };

        _context.Doctor.Add(doctor);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetDoctor", new { id = doctor.Id }, doctor);
    }

    // PUT: api/Doctors/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutDoctor(int id, EditDoctorDto doctorDto)
    {
        var doctor = await _context.Doctor.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        doctor.FullName = doctorDto.FullName;
        doctor.CabinetId = doctorDto.CabinetId;
        doctor.SpecializationId = doctorDto.SpecializationId;
        doctor.UparticipationId = doctorDto.UparticipationId;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Doctors/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var doctor = await _context.Doctor.FindAsync(id);

        if (doctor == null)
        {
            return NotFound();
        }

        _context.Doctor.Remove(doctor);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
