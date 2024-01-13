namespace AppointmentScheduler.Models.ViewModels
{
    public class CommonResponsecs<T>
    {
        public int status { get; set; }
        public string message { get; set; }
        public  T dataenum { get; set; }
    }
}
