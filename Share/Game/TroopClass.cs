using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game 
{
    /// <summary>
    /// Truppen Klasse
    /// </summary>
    [Serializable]
    public class TroopClass : IComparable, Interface.DatabaseEntry
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
        /// Bei welchen Rassen verfügbar
        /// </summary>
        public List<Race> race;
        private string race_temp;



        /// <summary>
        /// Globales Limit
        /// </summary>
        public int globallimit;


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
        /// Standardwiederstad
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
        /// Wasser Kosten
        /// </summary>
        public int water;
        /// <summary>
        /// Metall Kosten
        /// </summary>
        public int metal;
        /// <summary>
        /// Naquadah Kosten
        /// </summary>
        public int naquadah;
        /// <summary>
        /// Nahrungsmittel Kosten
        /// </summary>
        public int food;

        /// <summary>
        /// Tarnfaktor
        /// </summary>
        public int hide;

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


        private TroopClass(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Erzeugt einen neuen TruppenTyp
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>TroopClass</returns>
        public static TroopClass create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_troops` WHERE `ID` = '" + ID + "'");
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

            int hide = (int)Reader["hide"];

            int water = (int)Reader["water"];
            int metal = (int)Reader["metal"];
            int food = (int)Reader["food"];
            int naquadah = (int)Reader["naquadah"];



            int globallimit = (int)Reader["globallimit"];
            int time = (int)Reader["time"];

            string need_techS = (string)Reader["need_tech"];
            string skillsS = (string)Reader["skills"];
            string raceS = (string)Reader["race"];


            TroopClass troop = new TroopClass(ID, name);


            troop.globallimit = globallimit;
            troop.time = time;

            troop.need_tech_temp = need_techS;
            troop.skills_temp = skillsS;
            troop.race_temp = raceS;

            troop.health = health;
            troop.power = power;
            troop.power2 = power2;
            troop.power3 = power3;
            troop.power4 = power4;
            troop.resistend1 = resistend1;
            troop.resistend2 = resistend2;
            troop.resistend3 = resistend3;
            troop.resistend4 = resistend4;
            troop.hide = hide;

            troop.metal = metal;
            troop.naquadah = naquadah;
            troop.food = food;
            troop.water = water;

            Reader.Close();
            return troop;

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

        public override string ToString()
        {
            return id + " - " + this.name;
        }


        #region IComparable Member

        public int CompareTo(object obj)
        {
            if (obj is TroopClass)
            {
                TroopClass temp = (TroopClass)obj;

                return id.CompareTo(temp.id);
            }

            throw new ArgumentException("object is not valid");   

        }

        #endregion

        #region DatabaseEntry Member

        public int getID()
        {
            return id;
        }

        #endregion
    }
}
