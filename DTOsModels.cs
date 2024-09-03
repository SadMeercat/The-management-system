namespace Тестовое_Системы_управления
{
    public class EditPatientDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int UparticipationId { get; set; }
    }

    public class EditDoctorDto
    {
        public string FullName { get; set; }
        public int CabinetId { get; set; }
        public int SpecializationId { get; set; }
        public int? UparticipationId { get; set; }
    }

}
