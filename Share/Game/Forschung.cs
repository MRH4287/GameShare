using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// Forschungen
    /// </summary>
    [Serializable]
    public class Forschung
    {
        private User user;
        private Tech tech;
        private int started;

        #region Propertys

        /// <summary>
        /// Wann wurde die Forschung gestartet?
        /// Timestamp
        /// </summary>
        public int Started
        {
            get
            {
                return started;
            }
        }

        /// <summary>
        /// Was wird erforscht?
        /// </summary>
        public Tech Tech
        {
            get
            {
                return tech;
            }

        }

        /// <summary>
        /// welcher Benutzer forscht?
        /// </summary>
        public User User
        {
            get
            {
                return user;
            }
        }


        #endregion

        /// <summary>
        /// Erstellt eine neue Forschung
        /// </summary>
        /// <param name="user">Der Benutzer dem die Foschung gehört</param>
        /// <param name="tech">Die Technologie die erforscht wird</param>
        /// <param name="started">Die Zeit wann Sie gestartet wurde</param>
        public Forschung(User user, Tech tech, int started)
        {
            this.user = user;
            this.tech = tech;
            this.started = started;
        }


    }
}