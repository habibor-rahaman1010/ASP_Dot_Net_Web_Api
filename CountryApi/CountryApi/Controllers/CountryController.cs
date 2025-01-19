using CountryApi.DataAccessLayer;
using CountryApi.Model;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        private readonly IValidator<CountryDto> _validator;
        private readonly IValidator<UpdateDto> _updateValidator;
        private readonly ILogger<CountryController> _logger;

        public CountryController(ApplicationDbContext context, 
            IValidator<CountryDto> validator, 
            IValidator<UpdateDto> updateValidator,
            ILogger<CountryController> logger)
        {
            _context = context;
            _validator = validator;
            _updateValidator = updateValidator;
            _logger = logger;
        }

        //insert country
        [HttpPost]
        public async Task<IActionResult> AddCountry([FromBody] CountryDto country)
        {
            try
            {
                var validationResult = await _validator.ValidateAsync(country);
                if (!validationResult.IsValid)
                {
                    return BadRequest(new
                    {
                        Errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                    });
                }
                else
                {
                    var countryData = await country.BuildAdapter().AdaptToTypeAsync<Country>();
                    await _context.AddAsync(countryData);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("Country Added");
                    return Ok(new { Message = "Country successfully added." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{ex.Message}", ex.ToString());
                return StatusCode(500, new { Error = "An internal error occurred. Please try again later." });
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
                    _logger.LogInformation("No countries exist in the database!");
                    return NotFound("No countries exist in the database!");
                }
                else
                {
                    var countryDtos = await countries.BuildAdapter().AdaptToTypeAsync<List<CountryDto>>();
                    _logger.LogInformation("Retrive all countries");
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
                    _logger.LogInformation($"Country ID: {countryDto.Id}");
                    return Ok(countryDto);
                }
                else
                {
                    _logger.LogInformation($"Country not found {id}");
                    return NotFound("Country not found");
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



       /* // Update a country by id
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] UpdateDto countryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                else
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
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }*/




         // Update a country by id
         [HttpPut]
         [Route("{id}")]
         public async Task<IActionResult> UpdateCountry(Guid id, [FromBody] UpdateDto countryDto)
         {
             try
             {
                var validationResult = await _updateValidator.ValidateAsync(countryDto);
                 if (!validationResult.IsValid)
                 {
                     return BadRequest(new
                     {
                         Errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                     });
                 }
                 else
                 {
                     Country? country = await _context.Countries.FirstOrDefaultAsync(y => y.Id == id);
                     if (country != null)
                     {
                         country = await countryDto.BuildAdapter().AdaptToAsync(country);
                         await _context.SaveChangesAsync();
                         return Ok(new { Message = "Country updated successfully." });
                     }
                     else
                     {
                         return NotFound(new { Message = "Country not found by the provided ID." });
                     }
                 }
             }
             catch (Exception error)
             {
                 _logger.LogError(error, "An error occurred while updating the country.");
                 return StatusCode(500, new { Error = "An internal error occurred. Please try again later." });
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

        // Delete all countries
        [HttpDelete]
        [Route("delete-all")]
        public async Task<IActionResult> DeleteAllCountries()
        {
            try
            {
                // Retrieve all countries from the database
                var countries = await _context.Countries.ToListAsync();

                if (countries.Any())
                {
                    // Remove all countries
                    _context.Countries.RemoveRange(countries);
                    await _context.SaveChangesAsync();
                    return Ok("All countries deleted successfully");
                }
                else
                {
                    return NotFound("No countries found to delete");
                }
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
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