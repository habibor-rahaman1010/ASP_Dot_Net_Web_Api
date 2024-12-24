using CountryApi.DataAccessLayer;
using CountryApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CountryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        //insert country
        [HttpPost]
        public async Task<IActionResult> AddCountry(Country country)
        {
            try
            {
                await _context.AddAsync(country);
                await _context.SaveChangesAsync();
                return Ok("Country Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /*//insert multiple country
        [HttpPost]
        public async Task<IActionResult> AddCountries(IList<Country> countries)
        {
            try
            {
                if (countries == null || countries.Count == 0)
                {
                    return BadRequest("No countries provided.");
                }

                await _context.Countries.AddRangeAsync(countries);
                await _context.SaveChangesAsync();

                return Ok("Countries added successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/

        //Get all countries
        [HttpGet]
        public async Task<ActionResult<List<Country>>> GetAllCountries()
        {
            try
            {
                List<Country> countries = await _context.Countries.ToListAsync();
                if (countries.Count < 0)
                {
                    return NotFound("Not any countries are exist in database!");
                }
                else
                {
                    return Ok(countries);
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        //Get a country by id
        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Country>> GetCountryById(Guid id)
        {
            try
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
                if (country != null)
                {
                    return Ok(country);
                }
                else
                {
                    return NotFound("Country is not found");
                }
            }

            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        // Update a country by id
        [HttpPut]
        [Route("{id}")]

        public async Task<IActionResult> UpdateCountry(Guid id, Country c)
        {
            try
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(y => y.Id == id);
                if (country != null)
                {
                    country.Name = c.Name;
                    country.Capital = c.Capital;
                    country.Population = c.Population;
                    country.Area = c.Area;
                    country.Currency = c.Currency;
                    await _context.SaveChangesAsync();
                    return Ok("Country Updated Succesfully");
                }
                else
                {
                    return NotFound("Country Not Found By Your ID");
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        //Delete a country by id
        [HttpDelete]
        [Route("{id}")]

        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            try
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(y => y.Id == id);
                if (country != null)
                {
                    _context.Countries.Remove(country);
                    await _context.SaveChangesAsync();
                    return Ok("Country Deleted Successfully");
                }
                else
                {
                    return NotFound("Country Not Found");
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}
