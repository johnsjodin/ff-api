using System.Text.Json;
using FactionFactory.Api.Models;

namespace FactionFactory.Api.Services;

public class FactionStore
{
    private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\factions.json");
    private readonly List<Faction> _factions;

    public FactionStore()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _factions = JsonSerializer.Deserialize<List<Faction>>(json) ?? new List<Faction>();
        }
        else
        {
            _factions = new List<Faction>();
        }
    }

    public List<Faction> GetAll() => _factions;

    public Faction? GetById(int id) => _factions.FirstOrDefault(f => f.Id == id);

    public Faction Add(Faction faction)
    {
        faction.Id = _factions.Count == 0 ? 1 : _factions.Max(f => f.Id) + 1;
        _factions.Add(faction);
        SaveToFile();
        return faction;
    }

    public bool Update(int id, Faction updated)
    {
        var index = _factions.FindIndex(f => f.Id == id);
        if (index == -1) return false;

        updated.Id = id;
        _factions[index] = updated;
        SaveToFile();
        return true;
    }

    public bool Delete(int id)
    {
        var index = _factions.FindIndex(f => f.Id == id);
        if (index == -1) return false;

        _factions.RemoveAt(index);
        SaveToFile();
        return true;
    }

    private void SaveToFile()
    {
        var json = JsonSerializer.Serialize(_factions, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }
}