using Microsoft.EntityFrameworkCore;
using ResolutionSwitcher.Models;

namespace ResolutionSwitcher;
public class DB
{
    private readonly ResolutionSwitcherContext db;

    public DB()
    {
        db = new ResolutionSwitcherContext();
    }

    public List<Mode> GetDisplayModes()
    {
        return db.Modes.Include(d => d.Displays.OrderBy(d => d.DisplayName))
                       .OrderBy(d => d.Name)
                       .ToList();
    }



    public void AddMode(Mode mode)
    {
        var foundName = db.Modes.FirstOrDefault(m => m.Name == mode.Name);

        if (foundName == null)
        {
            db.Modes.Add(mode);
            db.SaveChanges();
        }
    }

    public void DeleteMode(Mode mode)
    {
        db.Modes.Remove(mode);
        db.SaveChanges();
    }



    public void AddDisplay(Display display)
    {
        var foundDisplayName = db.Displays.FirstOrDefault(d => d.DisplayName == display.DisplayName && 
                                                      d.ModeId == display.ModeId);
        var foundModeId = db.Modes.FirstOrDefault(m => m.Id == display.ModeId);

        if (foundDisplayName == null && foundModeId != null)
        {
            db.Displays.Add(display);
            db.SaveChanges();
        }
    }

    public void DeleteDisplay(Display display)
    {
        db.Displays.Remove(display);
        db.SaveChanges();
    }
}
