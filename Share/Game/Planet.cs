using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// Palaneten Klasse
    /// </summary>
    [Serializable]
    public class Planet
    {


        private string name;

        /// <summary>
        /// Besitzer
        /// </summary>
        public User UID;

        /// <summary>
        /// Planeten Typ
        /// </summary>
        public PlanetClass type;

        /// <summary>
        /// Das Sonnensystem in dem dieser Planet ist
        /// </summary>
        public Solarsystem Solarsystem;


        #region public vars



        /// <summary>
        /// Name des Planeten
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }



        #endregion


        /// <summary>
        /// Erstellt einen neuen Planeten
        /// </summary>
        /// <param name="name">Name</param>
        public Planet(string name)
        {
          
            this.name = name;
        }

        
        /// <summary>
        /// Wandelt einen Planeten in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return  name;
        }



    }
}
