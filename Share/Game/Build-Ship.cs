using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Repräsentiert die Bauliste
    /// </summary>
    [Serializable]
    public class Build_Ship
    {
        /// <summary>
        /// Der Benutzer dem der Bauauftrag gehört
        /// </summary>
        public User User;
        /// <summary>
        /// Die Schiffsklasse die gebaut werden soll
        /// </summary>
        public ShipClass Class;
        /// <summary>
        /// Der Startzeitunkt
        /// </summary>
        public int started;
        /// <summary>
        /// Die Länge des Bauauftrages
        /// </summary>
        public int time;
        /// <summary>
        /// Der Name des Schiffes das gebaut werden soll
        /// </summary>
        public string name;
        /// <summary>
        /// Von welcher Station wird dieses Schiff gebaut
        /// </summary>
        private Station buildBy;

        /// <summary>
        /// Von welcher Station wird dieses Schiffes gebaut
        /// </summary>
        public Station station
        {
            set
            {
                this.buildBy = value;
            }
            get
            {
                if ((buildBy != null) && (!buildBy.destroyed))
                {
                    return buildBy;
                }
                else
                {
                    buildBy = null;
                    return null;
                }
            }
        }


    }
}
