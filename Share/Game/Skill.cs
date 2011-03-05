using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// Fähigkeit
    /// </summary>
    [Serializable]
    public class Skill : IComparable, Interface.DatabaseEntry
    {
            private int id;
        private string name;
        private string beschreibung;

        /// <summary>
        /// PHP Skill
        /// </summary>
        public string skill;
        /// <summary>
        /// Benötigte Technologien
        /// </summary>
        public string need;
        /// <summary>
        /// States
        /// </summary>
        public string states;
        /// <summary>
        /// Pasive Fähigkeit?
        /// </summary>
        public bool passiv;
        /// <summary>
        /// Schiffsfähigkeit?
        /// </summary>
        public bool ship;
        /// <summary>
        /// Stationsfähigkeit?
        /// </summary>
        public bool stat;
        /// <summary>
        /// Truppenfähigkeit?
        /// </summary>
        public bool troop;
        /// <summary>
        /// Abklingzeit
        /// </summary>
        public int time;

      


        #region public vars
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get
            {
                return id;
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
        /// Beschreibung
        /// </summary>
        public string Beschreibung
        {
            get
            {
                return beschreibung;
            }
        }


        #endregion


        private Skill(int id, string name, string beschreibung)
        {
            this.id = id;
            this.name = name;
            this.beschreibung = beschreibung;

        }

        /// <summary>
        /// Erzeugt eine neue Fähigkeit
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>Skill</returns>
        public static Skill create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_skills` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];
            string beschreibung = (string)Reader["beschreibung"];
          

            int time = (int)Reader["time"];

            string need = (string)Reader["need"];

            string states = (string)Reader["states"];
            string skillS = (string)Reader["skill"];

            bool passiv = ((int)Reader["passiv"] ==1);
            bool ship = ((int)Reader["shipp"] == 1);
            bool stat = ((int)Reader["stat"] == 1);
            bool troop = ((int)Reader["troop"] == 1);


            Skill skill = new Skill(ID, name, beschreibung);


            skill.time = time;
            skill.need = need;
            skill.passiv = passiv;
            skill.stat = stat;
            skill.ship = ship;
            skill.skill = skillS;
            skill.states = states;
            skill.troop = troop;


            Reader.Close();

            return skill;
            
        }

        /// <summary>
        /// Wandelt einen Skill in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id+ " - "+name;
        }


        #region IComparable Member

        /// <summary>
        /// Vergleicht einen Skill mit einem anderem
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is Skill)
            {
                Skill temp = (Skill)obj;

                return id.CompareTo(temp.id);
            }

            throw new ArgumentException("object is not valid");   
        }

        #endregion

        #region DatabaseEntry Member

        /// <summary>
        /// Liefert die Datenbank ID
        /// </summary>
        /// <returns></returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
