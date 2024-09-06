using Business.I.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace SuivEase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SuivisController : ControllerBase
{
    private readonly ISuiviService _suiviService;
    private readonly IAddressService _addressService;
    private readonly IContactService _contactService;

    public SuivisController(ISuiviService suiviService)
    {
        _suiviService = suiviService;
    }


    //[FromQuery] //[ValidateAntiForgeryToken]

    [HttpGet("/all/{userId}")] // GET: api/Suivis/all/userId
    public async Task<ActionResult<IEnumerable<Suivi>>> GetSuivis(string userId)
    {
        IEnumerable<Suivi> suivis = await _suiviService.GetAll(userId);

        if (suivis == null)
            return NotFound();

        return Ok(suivis);            
    }

    [HttpGet("{id}")] // GET: api/Suivis/id
    public async Task<ActionResult<Suivi>> GetSuivi(int id)
    {
        if(! await _suiviService.Exists(id))
            return NotFound();

        return Ok(await _suiviService.GetById(id));            
    }

    [HttpPut("{id}")] // PUT: api/Suivis/id 
    public async Task<IActionResult> PutSuivi(int id, Suivi suivi)
    {        
        if (suivi.SuiviId != id)
            return BadRequest();

        if (! await _suiviService.Exists(id))
            return NotFound();

        if(await _suiviService.Update(suivi))
            return Ok();
        else
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred when updating.");         
    }

    [HttpPost] // POST: api/Suivis  
    public async Task<ActionResult<Suivi>> PostSuivi(Suivi suivi, Address address, Contact contact)
    {
        // TO DO : Avoir un moyen de pick dans Contact déjà existant

        //if (!await _suiviService.Exists(suivi.SuiviId))
        //    return BadRequest($"This item already exists ! (ID = {suivi.SuiviId})");

        var resAddress = await _addressService.Create(address);
        suivi.AddressId = resAddress.AddressId;

        var resContact = await _contactService.Create(contact);
        suivi.Contacts.Add(resContact);

        var res = await _suiviService.Create(suivi);

        if (res != null)
            return Ok(res);
        else
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred when creating.");
    }

    [HttpDelete("{id}")] // DELETE: api/Suivis/id
    public async Task<IActionResult> DeleteSuivi(int id)
    {
        if (!await _suiviService.Exists(id))
            return NotFound();

        if (await _suiviService.Delete(id))
            return Ok(id);
        else
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred when deleting.");
    }
}
