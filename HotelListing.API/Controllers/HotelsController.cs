
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Contracts;
using HotelListing.API.Models.Hotels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelsController : ControllerBase
    {
        //Removing directly connecting to the DBContext and adding repository design pattern
        private readonly IHotelRepository _hotelRepository;
        //AutoMapper Injection
        private readonly IMapper _mapper;
        public HotelsController(IHotelRepository hotelRepository,IMapper mapper)
        {
            _hotelRepository=hotelRepository;
            _mapper=mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelDetails>>> GetHotels()
        {
            List<Hotel> hotels = await _hotelRepository.GetAllAsync();
            var records=_mapper.Map<List<HotelDetails>>(hotels);
            return records;
        }

        // GET: api/Hotels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Hotel>> GetHotel(int id)
        {
            var hotel=await _hotelRepository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound();
            }

            return hotel;
        }

        // PUT: api/Hotels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotel updateHotel)
        {
            if (id != updateHotel.Id)
            {
                return BadRequest();
            }
            var record = await _hotelRepository.GetAsync(id);
            if(record==null)
            {
                return NotFound();
            }
            _mapper.Map(updateHotel, record);


            //await _hotelRepository.UpdateAsync(hotelC);

            try
            {
                await _hotelRepository.UpdateAsync(record);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await HotelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Hotels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotel>> PostHotel(AddHotel hotel)
        {
            var record=_mapper.Map<Hotel>(hotel);
            
            _hotelRepository.AddAsync(record);
            //await _context.SaveChangesAsync();

            return record;
        }

        // DELETE: api/Hotels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {

            var hotel = await _hotelRepository.GetAsync(id); 
            if (hotel == null)
            {
                return NotFound();
            }
            _hotelRepository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExists(int id)
        {
            return await _hotelRepository.Exists(id);
        }
    }
}
