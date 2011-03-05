using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Rassen Klasse
    /// </summary>
    [Serializable]
    public class Race : IComparable, Interface.DatabaseEntry
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id;

        /// <summary>
        /// Name
        /// </summary>
        public string name;

        /// <summary>
        /// Konstruktor der Klasse Race
        /// </summary>
        /// <param name="id">ID</param>
        /// <param name="name">Name</param>
        public Race(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Wandelt eine Rasse in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + " - " + this.name;
        }


        #region IComparable Member

        /// <summary>
        /// Vergleicht eine Rasse mit einer anderen
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Race)
            {
                Race temp = (Race)obj;

                return id.CompareTo(temp.id);
            }

            throw new ArgumentException("object is not valid");
        }

        #endregion

        #region DatabaseEntry Member
        /// <summary>
        /// Liefert die DatenbankID
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
