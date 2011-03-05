
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// Updates
    /// </summary>
    [Serializable]
    public class Update : IComparable, Interface.DatabaseEntry
    {
        private int id;
        private string name;


        /// <summary>
        /// Für welchen Schiffstyp ist Update möglich?
        /// </summary>
        public ShipClass shiptype;
        /// <summary>
        /// Für welchen Stationstyp ist Update möglich?
        /// </summary>
        public StationClass stattype;

        /// <summary>
        /// Welche Fähikeiten werdern dazu gewonnen?
        /// </summary>
        public List<Skill> skills;
        private string skills_temp;

        /// <summary>
        /// Bonus auf Standardschaden
        /// </summary>
        public int strength;
        /// <summary>
        /// Bonus auf Laserschaden
        /// </summary>
        public int strength2;
        /// <summary>
        /// Bonus auf Raketenschaden
        /// </summary>
        public int strength3;
        /// <summary>
        /// Bonus auf Flackschaden
        /// </summary>
        public int strength4;
        /// <summary>
        /// Bonus auf Standardwiederstand
        /// </summary>
        public int resistend1;
        /// <summary>
        /// Bonus auf Laserwiederstand
        /// </summary>
        public int resistend2;
        /// <summary>
        /// Bonus auf Raketenwiederstand
        /// </summary>
        public int resistend3;
        /// <summary>
        /// Bonus auf Flackwiederstand
        /// </summary>
        public int resistend4;

        /// <summary>
        /// Bonus auf Reisegeschwindigkeit
        /// </summary>
        public int speed;

        /// <summary>
        /// Liste die Angibt um wie viel die Kosten gesenkt werdn sollen
        /// </summary>
        public ResList reduceCost = new ResList();

        /// <summary>
        /// Reduzierung der Bauzeit
        /// </summary>
        public int time;

        /// <summary>
        /// Bonus auf Tarnfaktor
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


        private Update(int id, string name)
        {
            this.id = id;
            this.name = name;

        }

        /// <summary>
        /// Erzeugt neues Update
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>Update</returns>
        public static Update create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_update` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];
            string skillsS = (string)Reader["skills"];

            int hide = (int)Reader["hide"];
            int speed = (int)Reader["speed"];

            int power = (int)Reader["strength"];
            int power2 = (int)Reader["strength2"];
            int power3 = (int)Reader["strength3"];
            int power4 = (int)Reader["strength4"];
            int resistend1 = (int)Reader["resistend1"];
            int resistend2 = (int)Reader["resistend2"];
            int resistend3 = (int)Reader["resistend3"];
            int resistend4 = (int)Reader["resistend4"];




            int time = (int)Reader["time"];





            Update update = new Update(ID, name);

            foreach (ResType type in Enum.GetValues(typeof(ResType)))
            {
                try
                {
                    update.reduceCost[type] = (int)Reader[type.ToString()];
                }
                catch
                {
                }

            }

            update.time = time;
            update.skills_temp = skillsS;
            update.hide = hide;
            update.speed = speed;
            update.strength = power;
            update.strength2 = power2;
            update.strength3 = power3;
            update.strength4 = power4;

            update.resistend1 = resistend1;
            update.resistend2 = resistend2;
            update.resistend3 = resistend3;
            update.resistend4 = resistend4;
            update.shiptype = data.getShipType((int)Reader["shipptype"]);
            update.stattype = data.getStationType((int)Reader["stattype"]);
            // update.trooptype = data.getTroopType((int)Reader["trooptype"]);


            Reader.Close();

            return update;

        }
        /// <summary>
        /// Aktualisiert die Objektrefferenzen, die beim erzeugen des Objektes noch nicht vorhanden waren
        /// </summary>
        public void updateData(GameData data)
        {


            skills = new List<Skill>();

            string[] split2 = skills_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split2)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    skills.Add(data.getSkill(id2));
                }
            }





        }

        /// <summary>
        /// Liefert die Update Daten als String
        /// </summary>
        /// <returns>Update Daten</returns>
        public override string ToString()
        {
            return id + " - " + name;
        }



        #region IComparable Member

        /// <summary>
        /// Vergleicht ein Update mit einem anderem
        /// </summary>
        /// <param name="obj">Objekt mit dem verglichen werden soll</param>
        /// <returns>-1, 0 oder 1</returns>
        public int CompareTo(object obj)
        {
            if (obj is Update)
            {
                Update update = (Update)obj;

                return id.CompareTo(update.id);
            }

            throw new ArgumentException("object is not valid");
        }

        #endregion

        #region DatabaseEntry Member

        /// <summary>
        /// Liefert die Datenbank ID
        /// </summary>
        /// <returns>Datenbank ID</returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
