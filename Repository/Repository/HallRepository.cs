using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using TestAPI.Interfaces;
using TestAPI.Models;
using TestAPI.Repository.Database;

namespace TestAPI.Repository.Repository
{
    public class HallRepository : IHallRepository
    {

        public DatabaseContext _context { get; set; }

        public HallRepository(DatabaseContext context)
        {
            _context = context;
        }


        public Task create(int rowCount, int colCount)
        {


            Hall hall = new Hall();
            
            hall.Name = "Hall 1";
            hall.Seat = new List<Seat>();
            

            _context.Hall.Add(hall);
            _context.SaveChanges();



            for (int r = 1; r < rowCount + 1; r++)
            {

                for (int c = 1; c < colCount + 1; c++)
                {
    
                    Seat seat = new Seat();

                    seat.SeatRow = r;
                    seat.SeatCol = c;
                    _context.Seat.Add(seat);
                    hall.Seat.Add(seat);
                    
                    
                }

            }
            
            _context.SaveChanges();
            createReserved(hall);
            return Task.CompletedTask;


        }

        public void createReserved(Hall hall)
        {

            for (int i = 0; i < hall.Seat.Count; i++)
            {


                // Her laver jeg en reserved udfra den liste jeg har lavet i create
                Reserved reservedSeat = new Reserved();

                // Tilføjer dataen til Hall objectet
                reservedSeat.Date = DateTime.Now;
                reservedSeat.HallObj = hall;
                reservedSeat.seatId = hall.Seat[i].Id;
                reservedSeat.booked = true;

                // 
                _context.Reserved.Add(reservedSeat);

            }

            // Gemmer til databasen
            _context.SaveChanges();
        }

        public Task delete(bool choice)
        {
            List<Seat> seat = _context.Seat.ToList();

            if (choice == true)
            {


                for (int i = seat.Count - 1 ; i >= 0; i--)
                {
                    // Den dividere med 2, og vil altid få nul hvis det er et lige tal
                    if (seat[i].Id % 2 == 0) 
                    {
                        _context.Seat.Remove(seat[i]);
                    }
                }

            }
            else
            {

                for (int i = seat.Count - 1; i >= 0; i--)
                {
                    if (seat[i].Id % 2 != 0) // Slet elementer med ulige Id, hvis choice er false
                    {
                        _context.Seat.Remove(seat[i]);
                    }
                }

            }
            
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public async Task<List<Reserved>> readReservedSeats()
        {
            return await _context.Reserved.Where(x => x.booked == true).ToListAsync();

        }

        public async Task<Hall> readSeatsInHall(int hallId)
        {   
            return await _context.Hall.Include(s => s.Seat).FirstOrDefaultAsync(x => x.Id == hallId);
        }

        public async Task<List<Reserved>> reserved(DateTime whatYouWant)
        {
            return await _context.Reserved.Where(x => x.Date > whatYouWant).ToListAsync();
        }

        public Task deleteByHallId(bool choice, int hallId)
        {

            throw new NotImplementedException();
            //List<Seat> seat = _context.Seat.ToList();
            //Hall hall = new Hall();

            //if (choice == true)
            //{


            //    for (int i = seat.Count - 1; i >= 0; i--)
            //    {
            //        // Den dividere med 2, og vil altid få nul hvis det er et lige tal
            //        if (seat[i].Id % 2 == 0 && hall.Id == hallId)
            //        {
            //            _context.Seat.Remove(seat[i]);
            //        }
            //    }

            //}
            //else
            //{

            //    for (int i = seat.Count - 1; i >= 0; i--)
            //    {
            //        if (seat[i].Id % 2 != 0 && hall.Id == hallId) // Slet elementer med ulige Id, hvis choice er false
            //        {
            //            _context.Seat.Remove(seat[i]);
            //        }
            //    }

            //}

            //_context.SaveChanges();
            //return Task.CompletedTask;
        }
    }
}
