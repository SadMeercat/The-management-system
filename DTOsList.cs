namespace Тестовое_Системы_управления
{
    public class PatientListDto
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string UparticipationNumber { get; set; }
    }

    public class DoctorListDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string CabinetNumber { get; set; }
        public string SpecializationName { get; set; }
        public string UparticipationNumber { get; set; }
    }

}
