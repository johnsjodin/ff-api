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

    [HttpGet("{id}")]
    public ActionResult<Faction> GetById(int id)
    {
        var faction = _factions.FirstOrDefault(f => f.Id == id);
        if (faction is null) return NotFound();
        return Ok(faction);
    }

    [HttpPost]
    public ActionResult<Faction> Create(Faction faction)
    {
        faction.Id = _factions.Count == 0 ? 1 : _factions.Max(f => f.Id) + 1;
        _factions.Add(faction);
        return CreatedAtAction(nameof(GetById), new { id = faction.Id }, faction);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Faction faction)
    {
        var index = _factions.FindIndex(f => f.Id == id);
        if (index == -1) return NotFound();

        faction.Id = id;
        _factions[index] = faction;
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var index = _factions.FindIndex(f => f.Id == id);
        if (index == -1) return NotFound();

        _factions.RemoveAt(index);
        return NoContent();
    }
}