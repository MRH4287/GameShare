using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using Game.Game.Interface;

namespace Game.Game
{
    /// <summary>
    /// Shipp Klasse
    /// Repräsentiert ein mommentan exsistierendes Schiff
    /// </summary>
    [Serializable]
   public class Ship : Fighter
    {

        private ShipClass type;
        private User uid;
        private string name;
        private int fleet;
        private string states;

        private Solarsystem position;

        /// <summary>
        /// Die Geschwindigkeit eines Schiffes
        /// </summary>
        public int speed;

        #region public vars


        /// <summary>
        /// Schiffstyp
        /// </summary>
        public ShipClass Type
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
        /// Flotte
        /// </summary>
        public int Fleet
        {
            get
            {
                return fleet;
            }
        }
        /// <summary>
        /// aktuelle Position
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

        
        /// <summary>
        /// Erstellt ein neues Schiff
        /// </summary>
        /// <param name="type">Typ des Schiffes</param>
        /// <param name="UID">Der Besitzer des Schiffes</param>
        /// <param name="name">Der Name des Schiffes</param>
        /// <param name="fleet">Die Flotte, in der das Schiff ist</param>
        /// <param name="states">Die Zustände die ein Schiff hat</param>
        public Ship(ShipClass type, User UID, string name, int fleet, string states)
        {
 
            this.type = type;
            this.uid = UID;
            this.name = name;
            this.fleet = fleet;
            this.states = states;
        }

        


        /// <summary>
        /// Wandelt ein Schiff in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return  name + "  (" + type + ")";
        }




    }
}
