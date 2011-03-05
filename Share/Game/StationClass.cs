using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// StationsKlasse
    /// </summary>
    [Serializable]
    public class StationClass : IComparable, Interface.DatabaseEntry
    {
        private int id;
        private string name;

        /// <summary>
        /// Benötigte Technologien
        /// </summary>
        public List<Tech> need_tech;
        private string need_tech_temp;

        /// <summary>
        /// Fähigkeiten
        /// </summary>
        public List<Skill> skills;
        private string skills_temp;

        /// <summary>
        /// Bei welchen Rassen verfügbar?
        /// </summary>
        public List<Race> race;
        private string race_temp;

        /// <summary>
        /// Kann welche Schiffe Prodizieren?
        /// </summary>
        public List<ShipClass> buildship;
        private string buildship_temp;

        /// <summary>
        /// Liste der Resourcen die jede Runde erstellt werden
        /// </summary>
        public ResList create_res = new ResList();

        /// <summary>
        /// Zu welcher Station ist ein Update möglich?
        /// </summary>
        public StationClass updateto;
        private int updateto_temp;

        /// <summary>
        /// Bild Datei
        /// </summary>
        public System.Drawing.Bitmap picture;




        /// <summary>
        /// Benötigte Stationen
        /// </summary>
        public System.Collections.Hashtable need = new System.Collections.Hashtable();
        private string need_temp;
        private string need_count_temp;

        /// <summary>
        /// Mögliche Namen
        /// </summary>
        public string names;
        /// <summary>
        /// Globales Limit
        /// </summary>
        public int globallimit;
        /// <summary>
        /// Planetares Limit
        /// </summary>
        public int limit;

        /// <summary>
        /// Bevölkerungsmaximumssteigerung
        /// </summary>
        public int population;
        /// <summary>
        /// Bauzeit
        /// </summary>
        public int time;
        /// <summary>
        /// Leben
        /// </summary>
        public int health;
        /// <summary>
        /// Standardschadem
        /// </summary>
        public int power;
        /// <summary>
        /// Laserschaden
        /// </summary>
        public int power2;
        /// <summary>
        /// Raketenschaden
        /// </summary>
        public int power3;
        /// <summary>
        /// Flackschaden
        /// </summary>
        public int power4;
        /// <summary>
        /// Standardwiederstand
        /// </summary>
        public int resistend1;
        /// <summary>
        /// Laserwiederstand
        /// </summary>
        public int resistend2;
        /// <summary>
        /// Raketenwiederstand
        /// </summary>
        public int resistend3;
        /// <summary>
        /// Flackwiederstand
        /// </summary>
        public int resistend4;

        /// <summary>
        /// Kosten für diese station
        /// </summary>
        public ResList price = new ResList();

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



        #endregion


        private StationClass(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Erzeugt eine neue Stationsklasse
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns></returns>
        public static StationClass create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_stations` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];

            string buildres = (string)Reader["buildres"];
            string res = (string)Reader["res"];

            string buildshipS = (string)Reader["buildship"];
            string buildtroopS = (string)Reader["buildtroop"];

            int power = (int)Reader["Power"];
            int health = (int)Reader["health"];
            int power2 = (int)Reader["Power2"];
            int power3 = (int)Reader["Power3"];
            int power4 = (int)Reader["Power4"];
            int resistend1 = (int)Reader["Resistend1"];
            int resistend2 = (int)Reader["Resistend2"];
            int resistend3 = (int)Reader["Resistend3"];
            int resistend4 = (int)Reader["Resistend4"];


            string need_temp = (string)Reader["need"];
            string need_count_temp = (string)Reader["needcount"];
            int updateto = (int)Reader["updateto"];
            int population = (int)Reader["population"];

            string names = (string)Reader["names"];
            int limit = (int)Reader["limit"];
            int globallimit = (int)Reader["globallimit"];
            int time = (int)Reader["time"];

           

            string need_techS = (string)Reader["need_tech"];
            string skillsS = (string)Reader["skills"];
            string raceS = (string)Reader["race"];


            byte[] picture = (byte[])Reader["picture"];

            StationClass station = new StationClass(ID, name);

            station.names = names;
            station.globallimit = globallimit;
            station.time = time;

            station.need_tech_temp = need_techS;
            station.skills_temp = skillsS;
            station.race_temp = raceS;

           // station.buildtroop_temp = buildtroopS;
            station.buildship_temp = buildshipS;
            station.limit = limit;
            station.need_count_temp = need_count_temp;
            station.need_tech_temp = need_techS;
            station.need_temp = need_temp;
            station.population = population;

            station.skills_temp = skillsS;
            station.updateto_temp = updateto;

            string costs = (string)Reader["costs"];
            string[] costs_A1 = costs.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in costs_A1)
            {
                try
                {
                    string[] costs_A2 = value.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    ResType type = (ResType)Enum.Parse(typeof(ResType), costs_A2[0]);
                    double cost = double.Parse(costs_A2[1]);

                    station.price[type] = cost;


                }
                catch
                {
                    // Unsinnige Mysql Daten
                }
            }  

            station.create_res = Static.ResHelper.getResAdd(buildres, res);


            station.health = health;
            station.power = power;
            station.power2 = power2;
            station.power3 = power3;
            station.power4 = power4;
            station.resistend1 = resistend1;
            station.resistend2 = resistend2;
            station.resistend3 = resistend3;
            station.resistend4 = resistend4;

            try
            {
                station.picture = GraphicLibary.GraphicHelper.getPicture(picture);
            }
            catch
            {
                station.picture = new System.Drawing.Bitmap(1, 1);
            }

            Reader.Close();
            return station;

        }

        /// <summary>
        /// Aktualisiert die Objektrefferenzen, die beim erzeugen des Objektes noch nicht vorhanden waren
        /// </summary>
        public void updateData(GameData data)
        {
            if (updateto_temp != 0)
            {
                updateto = data.getStationType(updateto_temp);
            }

            buildship = new List<ShipClass>();

            string[] split = buildship_temp.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in split)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    ShipClass add = data.getShipType(id2);

                    if (add != null)
                        buildship.Add(add);
                }
            }


            /*
            buildtroop = new List<TroopClass>();

            string[] split2 = buildtroop_temp.Split(new String[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string id in split2)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    TroopClass add = data.getTroopType(id2);

                    if (add != null)
                        buildtroop.Add(add);
                }
            }

            */

            need_tech = new List<Tech>();

            string[] split3 = need_tech_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split3)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    need_tech.Add(data.getTech(id2));
                }
            }

            race = new List<Race>();

            string[] split4 = race_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split4)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    race.Add(data.getRace(id2));
                }
            }

            //TODO: Skills, etc.

            skills = new List<Skill>();

            string[] split5 = skills_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split5)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    skills.Add(data.getSkill(id2));
                }
            }


            need = new System.Collections.Hashtable();
            string[] need_split = need_temp.Split(new string[] { ", " }, StringSplitOptions.None);
            string[] need_count = need_count_temp.Split(new String[] { ", " }, StringSplitOptions.None);


            for (int i = 0; i < need_split.Length; i++)
            {
                if ((need_count[i] != "") && (need_split[i] != ""))
                {

                    need.Add(data.getStationType(int.Parse(need_split[i])), int.Parse(need_count[i]));
                }
            }

        }

        /// <summary>
        /// Wandelt eine Stations Klasse in einen String um
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return id + " - " + this.name;
        }



        #region IComparable Member

        /// <summary>
        /// Vergleicht eine Stationsklasse mit einer anderen
        /// </summary>
        /// <param name="obj">VErgleichsobjekt</param>
        /// <returns>-1 0 oder 1</returns>
        public int CompareTo(object obj)
        {
            if (obj is StationClass)
            {
                StationClass temp = (StationClass)obj;

                return id.CompareTo(temp.id);
            }

            throw new ArgumentException("object is not valid");
        }

        #endregion

        #region DatabaseEntry Member

        /// <summary>
        /// Liefert die DatenbankID
        /// </summary>
        /// <returns>DatenbankID</returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
