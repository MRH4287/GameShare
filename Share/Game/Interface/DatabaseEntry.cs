using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game.Interface
{
    /// <summary>
    /// Das Interface für alle Datenbank Einträge
    /// </summary>
   public interface DatabaseEntry
    {
       /// <summary>
       /// Gibt die Datenbank ID Zurürck
       /// </summary>
       /// <returns>Id in der Datenbank</returns>
        int getID();

    }
}
