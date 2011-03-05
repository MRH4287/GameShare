using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// Technologie Klasse
    /// </summary>
    [Serializable]
    public class Tech : IComparable, Interface.DatabaseEntry
    {
        private int id;
        private string name;
        private string beschreibung;


        /// <summary>
        /// Benötigt welche Technologie
        /// </summary>
        public List<Tech> need_tech;
        private string need_tech_temp;

        /// <summary>
        /// Bei welchen Rassen verfügbar?
        /// </summary>
         public List<Race> race;
        private string race_temp;

        /// <summary>
        /// Ermöglicht das Freischalten welcher Updates
        /// </summary>
        public List<Update> update;
        private string update_temp;


        /// <summary>
        /// Entwicklungszeit
        /// </summary>
        public int time;

        /// <summary>
        /// Kosten für diese Technologie
        /// </summary>
        public ResList price;

        /// <summary>
        /// TEchGruppe
        /// </summary>
        public int group;

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


        private Tech(int id, string name, string beschreibung)
        {
            this.id = id;
            this.name = name;
            this.beschreibung = beschreibung;

        }

        /// <summary>
        /// Erezeugt eine neue Technologie
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>Tech</returns>
        public static Tech create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_tech` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];
            string beschreibung = (string)Reader["beschreibung"];
           
            int water = (int)Reader["water"];
            int metal = (int)Reader["metal"];
            int food = (int)Reader["food"];
            int naquadah = (int)Reader["naquadah"];

            int time = (int)Reader["time"];

            string need_techS = (string)Reader["need"];
            string updateS = (string)Reader["update"];
            string raceS = (string)Reader["race"];

            int group = (int)Reader["group"];

            Tech tech = new Tech(ID, name, beschreibung);

            string costs = (string)Reader["costs"];
            string[] costs_A1 = costs.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in costs_A1)
            {
                try
                {
                    string[] costs_A2 = value.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    ResType type = (ResType)Enum.Parse(typeof(ResType), costs_A2[0]);
                    double cost = double.Parse(costs_A2[1]);

                    tech.price[type] = cost;


                }
                catch
                {
                    // Unsinnige Mysql Daten
                }
            }  

            tech.time = time;
            tech.need_tech_temp = need_techS;
            tech.update_temp = updateS;
            tech.race_temp = raceS;
            tech.group = group;

            Reader.Close();

            return tech;
            
        }
        /// <summary>
        /// Aktualisiert die Objektrefferenzen, die beim erzeugen des Objektes noch nicht vorhanden waren
        /// </summary>
        public void updateData(GameData data)
        {
            need_tech = new List<Tech>();

            string[] split1 = need_tech_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split1)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    need_tech.Add(data.getTech(id2));
                }
            }


            race = new List<Race>();

            string[] split2 = race_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split2)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    race.Add(data.getRace(id2));
                }
            }


            update = new List<Update>();

            string[] split3 = update_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split3)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    update.Add(data.getUpdate(id2));
                }
            }


        }

        /// <summary>
        /// Wandelt Tech in einen String
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return id + " - " + name;
        }


        #region IComparable Member

        /// <summary>
        /// Vergleicht eine Technologie mit einer anderen
        /// </summary>
        /// <param name="obj">Vergleichsobjekt</param>
        /// <returns>-1 0 oder 1</returns>
        public int CompareTo(object obj)
        {
            if (obj is Tech)
            {
                Tech temp = (Tech)obj;

                return id.CompareTo(temp.id);
            }

            throw new ArgumentException("object is not valid");   
        }

        #endregion

        #region DatabaseEntry Member

        /// <summary>
        /// Liefert die Datenbank ID
        /// </summary>
        /// <returns>DatenbankID</returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
