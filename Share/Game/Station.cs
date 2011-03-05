using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Game.Game.Interface;

namespace Game.Game
{
    /// <summary>
    /// Stationsklasse
    /// Repräsentiert eine mommentane Station 
    /// </summary>
    [Serializable]
    public class Station : Fighter
    {

        private StationClass type;
        private User uid;
        private string name;
        private string states;

        private Solarsystem position;

        /// <summary>
        /// Ist die Station zerstört
        /// </summary>
        public bool destroyed = false;

        #region public vars

        /// <summary>
        /// StationsTyp
        /// </summary>
        public StationClass Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Besitzer
        /// </summary>
        public User Uid
        {
            get
            {
                return uid;
            }
        }
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }

        /// <summary>
        /// Position
        /// </summary>
        public Solarsystem Position
        {
            get
            {
                return position;
            }
            set
            {
                //TODO: Validierung
                position = value;
            }
        }

        #endregion


        private Station( StationClass type, User UID, string name, string states)
        {
            this.type = type;
            this.uid = UID;
            this.name = name;
            this.states = states;
        }

       
        /// <summary>
        /// Wandelt eine Station in einen String um
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return  name + "  (" + type + ")";
        }






    }
}
