using FactionFactory.Api.Models;
using FactionFactory.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FactionFactory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FactionsController : ControllerBase
{
    private readonly FactionStore _store;

    public FactionsController(FactionStore store)
    {
        _store = store;
    }

    [HttpGet]
    public ActionResult<List<Faction>> GetAll()
    {
        return Ok(_store.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Faction> GetById(int id)
    {
        var faction = _store.GetById(id);
        if (faction is null) return NotFound();
        return Ok(faction);
    }

    [HttpPost]
    public ActionResult<Faction> Create(Faction faction)
    {
        var createdFaction = _store.Add(faction);
        return CreatedAtAction(nameof(GetById), new { id = createdFaction.Id }, createdFaction);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Faction faction)
    {
        var updated = _store.Update(id, faction);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var deleted = _store.Delete(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}