namespace Тестовое_Системы_управления
{
    public class Uparticipation
    {
        public int Id { get; set; }
        public string Number { get; set; }
    }

    public class Specialization
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Cabinet
    {
        public int Id { get; set; }
        public string Number { get; set; }
    }

    public class Patient
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Gender { get; set; }

        public int UparticipationId { get; set; }
        public Uparticipation Uparticipation { get; set; }
    }

    public class Doctor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public int CabinetId { get; set; }
        public Cabinet Cabinet { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public int? UparticipationId { get; set; } // Участок может быть null
        public Uparticipation Uparticipation { get; set; }
    }

}
