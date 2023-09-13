using TestAPI.Models;

namespace TestAPI.Interfaces
{
    public interface IHallRepository
    {
        public Task<Hall> readSeatsInHall(int hallId);
        public Task<List<Reserved>> readReservedSeats();
        public Task delete(bool choice);
        public Task deleteByHallId(bool choice, int hallId);
        public Task create(int rowCount, int colCount);
        public void createReserved(Hall hall);
        public Task<List<Reserved>> reserved(DateTime whatYouWant);
    }
}
