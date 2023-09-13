using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Diagnostics.CodeAnalysis;
using TestAPI.Interfaces;
using TestAPI.Models;
using TestAPI.Repository.Repository;

namespace TestAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HallController : ControllerBase
    {
        IHallRepository _hallRepository { get; set; }

        public HallController(IHallRepository iHallRepository)
        {
            _hallRepository = iHallRepository;
        }

        [HttpGet]
        public async Task<Hall> readSeatsInHall(int hallId)
        {
            return await _hallRepository.readSeatsInHall(hallId);
        }

        [HttpPost]
        public Task create(int rowCount, int colCount)
        {
            return _hallRepository.create(rowCount, colCount);
        }

        [HttpDelete]
        public Task delete(bool choice)
        {
            return _hallRepository.delete(choice);
        }

        [HttpGet("reserved")]
        public async Task<List<Reserved>> readReservedSeats()
        {
            return await _hallRepository.readReservedSeats();
        }

        [HttpGet("dateTime")]
        public async Task<List<Reserved>> reserved(DateTime whatYouWant)
        {
            return await _hallRepository.reserved(whatYouWant);
        }

        [HttpDelete("hallid")]
        public Task deleteByHallId(bool choice, int hallId)
        {
            return _hallRepository.deleteByHallId(choice, hallId);
        }




    }
}
