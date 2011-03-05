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
   public class Build_Stats
    {
        /// <summary>
        /// Der Benutzer dem der Bauautrag gehört
        /// </summary>
        public User User;
        /// <summary>
        /// Die Stationsklasse die gebaut wird
        /// </summary>
        public StationClass Class;
        /// <summary>
        /// Wann wurde der Bau gestartet
        /// </summary>
        public int started;
        /// <summary>
        /// Die Länge des Baues
        /// </summary>
        public int time;
        /// <summary>
        /// Der Name der Station
        /// </summary>
        public string name;
        /// <summary>
        /// Die Position des Baus
        /// </summary>
        public Solarsystem position;


        private bool is_update = false;
        private Station update = null;

        /// <summary>
        /// Handelt es sich um ein Update
        /// </summary>
        public Station Update
        {
            set
            {
                is_update = true;
                update = value;
            }
            get
            {
                if ((update != null) && (!update.destroyed))
                {
                    return update;
                }
                else
                {
                    return null;
                }
            }

        }

        /// <summary>
        /// Handelt es sich um ein Update
        /// </summary>
        public bool IsUpdate
        {
            get
            {
                return is_update;
            }
        }


    }
}
