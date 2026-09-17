using FactionFactory.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace FactionFactory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactionsController : ControllerBase
{
    private static readonly List<Faction> _factions = new()
    {
        new Faction
        {
            Id = 1,
            Name = "The Iron Compact",
            Motto = "Strength Through Unity",
            Description = "A mercenary company known for its ironclad unity.",
            Type = "Company",
            Organisation = "Mercenary Contractor"
        }
    };

    [HttpGet]
    public ActionResult<List<Faction>> GetAll()
    {
        return Ok(_factions);
    }
}