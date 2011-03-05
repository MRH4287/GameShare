using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;


namespace Game.Game
{
    /// <summary>
    /// SchiffsKlasse
    /// </summary>
    [Serializable]
    public class ShipClass : IComparable, Interface.DatabaseEntry
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
        public List<Race> race = new List<Race>();
        private string race_temp;

        /// <summary>
        /// Mögliche Namen
        /// </summary>
        public string names;

        /// <summary>
        /// Reisegeschwindigkeit
        /// </summary>
        public int speed;
        /// <summary>
        /// Globales Limit
        /// </summary>
        public int globallimit;

        /// <summary>
        /// Bild Datei
        /// </summary>
        public System.Drawing.Bitmap picture;

        /// <summary>
        /// Bauzeit
        /// </summary>
        public int time;
        /// <summary>
        /// Leben
        /// </summary>
        public int health;
        /// <summary>
        /// Standardschaden
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
        /// Kosten für dieses Schiff
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


        private ShipClass(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Erzeugt eine neue SchiffsKlasse
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>ShippClass</returns>
        public static ShipClass create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_shipps` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];

            int power = (int)Reader["Power"];
            int health = (int)Reader["health"];
            int power2 = (int)Reader["Power2"];
            int power3 = (int)Reader["Power3"];
            int power4 = (int)Reader["Power4"];
            int resistend1 = (int)Reader["Resistend1"];
            int resistend2 = (int)Reader["Resistend2"];
            int resistend3 = (int)Reader["Resistend3"];
            int resistend4 = (int)Reader["Resistend4"];



            string names = (string)Reader["names"];
            int speed = (int)Reader["speed"];
            int globallimit = (int)Reader["globallimit"];
            int time = (int)Reader["time"];

            string need_techS = (string)Reader["need_tech"];
            string skillsS = (string)Reader["skills"];
            string raceS = (string)Reader["race"];

            byte[] picture = (byte[])Reader["bild"];

            ShipClass ship = new ShipClass(ID, name);

            ship.names = names;
            ship.speed = speed;
            ship.globallimit = globallimit;
            ship.time = time;

            ship.need_tech_temp = need_techS;
            ship.skills_temp = skillsS;
            ship.race_temp = raceS;

            ship.health = health;
            ship.power = power;
            ship.power2 = power2;
            ship.power3 = power3;
            ship.power4 = power4;
            ship.resistend1 = resistend1;
            ship.resistend2 = resistend2;
            ship.resistend3 = resistend3;
            ship.resistend4 = resistend4;

            string costs = (string)Reader["costs"];
            string[] costs_A1 = costs.Split(new string[] { "&" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string value in costs_A1)
            {
                try
                {
                    string[] costs_A2 = value.Split(new string[] { "=" }, StringSplitOptions.RemoveEmptyEntries);

                    ResType type = (ResType)Enum.Parse(typeof(ResType), costs_A2[0]);
                    double cost = double.Parse(costs_A2[1]);

                    ship.price[type] = cost;


                }
                catch
                {
                    // Unsinnige Mysql Daten
                }
            }         



            try
            {
                ship.picture = GraphicLibary.GraphicHelper.getPicture(picture);
            }
            catch
            {
                ship.picture = new System.Drawing.Bitmap(1, 1);
            }



            Reader.Close();
            return ship;

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


            skills = new List<Skill>();

            string[] split3 = skills_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split3)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    skills.Add(data.getSkill(id2));
                }
            }




        }

        /// <summary>
        /// Wandelt eine Schiffsklasse in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.id + " - " + this.name;
        }


        #region IComparable Member

        /// <summary>
        /// Vergleicht eine Schiffsklasse mit einer anderen
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is ShipClass)
            {
                ShipClass temp = (ShipClass)obj;

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
