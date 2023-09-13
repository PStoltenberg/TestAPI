namespace TestAPI.Models
{
    public class Reserved
    {
        public int Id {get; set;}
        public DateTime Date { get; set;}
        public Hall? HallObj { get; set;}
        public int seatId { get; set;} 
        public bool booked { get; set; } // true = free, false = booked
    } 
}
