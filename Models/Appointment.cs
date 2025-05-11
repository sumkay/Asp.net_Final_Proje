namespace Dis.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime CreatedAt { get; set; }

       
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }


}
