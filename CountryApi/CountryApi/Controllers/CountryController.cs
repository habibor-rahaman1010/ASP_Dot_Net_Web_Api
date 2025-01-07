using CountryApi.DataAccessLayer;
using CountryApi.Model;
using Mapster;
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
        public async Task<IActionResult> AddCountry(CountryDto country)
        {
            try
            {
                var countryData = await country.BuildAdapter().AdaptToTypeAsync<Country>();
                await _context.AddAsync(countryData);
                await _context.SaveChangesAsync();
                return Ok("Country Added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Get all countries
        [HttpGet]
        public async Task<ActionResult<List<CountryDto>>> GetAllCountries()
        {
            try
            {
                List<Country> countries = await _context.Countries.ToListAsync();
                if (countries.Count == 0)
                {
                    return NotFound("No countries exist in the database!");
                }
                else
                {
                    var countryDtos = await countries.BuildAdapter().AdaptToTypeAsync<List<CountryDto>>(); 
                    return Ok(countryDtos);
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
        public async Task<ActionResult<CountryDto>> GetCountryById(Guid id)
        {
            try
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
                if (country != null)
                {
                    var countryDto = await country.BuildAdapter().AdaptToTypeAsync<CountryDto>();
                    return Ok(countryDto);
                }
                else
                {
                    return NotFound("Country not found");
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
        public async Task<IActionResult> UpdateCountry(Guid id, UpdateDto countryDto)
        {
            try
            {
                Country? country = await _context.Countries.FirstOrDefaultAsync(y => y.Id == id);
                if (country != null)
                {
                    country = await countryDto.BuildAdapter().AdaptToAsync(country);
                    await _context.SaveChangesAsync();
                    return Ok("Country Updated Successfully");
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

        // Delete a country by id
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
}
