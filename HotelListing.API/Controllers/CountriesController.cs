using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing.API.Data;
using HotelListing.API.Models.Country;
using AutoMapper;
using HotelListing.API.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CountriesController : ControllerBase
    {
        //private readonly HotelListingDBContext _context;
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository1;

        public CountriesController(IMapper mapper,ICountryRepository countryRepository)
        {
            _countryRepository1= countryRepository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<IEnumerable<GetCountryDetails>>> GetCountries()
        {
            List<Country> countries = await _countryRepository1.GetAllAsync();
            var records=_mapper.Map<List<GetCountryDetails>>(countries);
            return records;
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        [Authorize(Roles ="User")]
        public async Task<ActionResult<CountryDetails>> GetCountry(int id)
        {
          
            var country = await _countryRepository1.GetDetails(id);
            //.Include(e => e.Hotels).FirstOrDefaultAsync(q => q.Id == id);

            if (country == null)
            {
                //return NotFound();
                return null;
            }
            var records = _mapper.Map<CountryDetails>(country);
            return records;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles ="Administrator")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountry updateCountry)
        {
            if (id != updateCountry.Id)
            {
                return BadRequest();
            }

            var country = await _countryRepository1.GetAsync(id);
            if(country == null) {
                return NotFound();
            }
            _mapper.Map(updateCountry, country);
            //_context.Entry(updateCountry).State = EntityState.Modified;

            try
            {
                await _countryRepository1.UpdateAsync(country);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExists(id))
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

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<Country>> PostCountry(AddCountry addCountry)
        {
           Country country=_mapper.Map<Country>(addCountry);
          if (_countryRepository1 == null)
          {
              return Problem("Entity set 'HotelListingDBContext.Countries'  is null.");
          }
            _countryRepository1.AddAsync(country);
            //await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteCountry(int id)
        {



            _countryRepository1.DeleteAsync(id);
        

            return NoContent();
        }
        [Authorize(Roles ="User")]
        private async Task<bool> CountryExists(int id)
        {
           return await _countryRepository1.Exists(id);
            
        }

    }
}
