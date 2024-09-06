using Business.I.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace SuivEase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }


    [HttpGet("{id}")] // GET: api/Address/id
    public async Task<ActionResult<Suivi>> GetSuivi(int id)
    {
        if (!await _addressService.Exists(id))
            return NotFound();

        return Ok(await _addressService.GetById(id));
    }

    [HttpPut("{id}")] // PUT: api/Address/id 
    public async Task<IActionResult> PutSuivi(int id, Address address)
    {
        if (address.AddressId != id)
            return BadRequest();

        if (!await _addressService.Exists(id))
            return NotFound();

        if (await _addressService.Update(address))
            return Ok();
        else
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred when updating.");
    }
}
