using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using Game.Game.Interface;

namespace Game.Game
{
    /// <summary>
    /// Truppen Klasse
    /// Repräsentiert einen mommentan vorhandenen Trupp
    /// </summary>
    [Serializable]
    public class Troop : Fighter, Interface.DatabaseEntry
    {
        private int id;
        private TroopClass type;
        private User uid;
        private string name;
        private string states;

        private WorldPoint position;


        /// <summary>
        /// Tarnfaktor
        /// </summary>
        public int hide;
        /// <summary>
        /// Team
        /// </summary>
        public int team;
        /// <summary>
        /// VErhalten bei Feidkontakt
        /// </summary>
        public string verhalten;
        /// <summary>
        /// Leben
        /// </summary>
        /// 

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
        /// Truppen Klasse
        /// </summary>
        public TroopClass Type
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
        public WorldPoint Position
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


        private Troop(int ID, TroopClass type, User UID, string name, int team, string states)
        {
            id = ID;
            this.type = type;
            this.uid = UID;
            this.name = name;
            this.team = team;
            this.states = states;
        }

        /// <summary>
        /// Erzeugt einen neuen Trupp
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns></returns>
        public static Troop create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_mom-troops` WHERE `ID` = '" + ID + "'");
            Reader.Read();

            int type = (int)Reader["Class"];
            string name = (string)Reader["Name"];
            User UID = data.getUser((int)Reader["UID"]);
            int power = (int)Reader["Power"];
            int health = (int)Reader["health"];
            int team = (int)Reader["team"];
            WorldPoint position = new WorldPoint((string)(Reader["worldpos"]));
            string states = (string)Reader["states"];
            int power2 = (int)Reader["Power2"];
            int power3 = (int)Reader["Power3"];
            int power4 = (int)Reader["Power4"];
            int resistend1 = (int)Reader["Resistend1"];
            int resistend2 = (int)Reader["Resistend2"];
            int resistend3 = (int)Reader["Resistend3"];
            int resistend4 = (int)Reader["Resistend4"];

            int hide = (int)Reader["hide"];
            string verhalten = (string)Reader["verhalten"];



            TroopClass typeClass = data.getTroopType(type);




            Troop troop = new Troop(ID, typeClass, UID, name, team, states);
            troop.position = position;
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
            troop.verhalten = verhalten;

            troop.fighterTyp = FighterType.TROOP;

            Reader.Close();
            return troop;

        }

        /// <summary>
        /// Erzeugt ein Dummy Objekt dieser Klasse, um es für Kampfsimulationen einzusetzten.
        /// </summary>
        /// <param name="type">Typ</param>
        /// <param name="name">Name</param>
        /// <returns>Eins Instanz dieser Klasse</returns>
        public static Troop createDummy(TroopClass type, string name)
        {
            User user = new User(0, "Dummy", "0", new Race(0, "Dummy"));
            Troop objekt = new Troop(0, type, user, name, 0, "");
            objekt.power = type.power;
            objekt.power2 = type.power2;
            objekt.power3 = type.power3;
            objekt.power4 = type.power4;
            objekt.resistend1 = type.resistend1;
            objekt.resistend2 = type.resistend2;
            objekt.resistend3 = type.resistend3;
            objekt.resistend4 = type.resistend4;
            objekt.health = type.health;

            objekt.fighterTyp = FighterType.TROOP;

            return objekt;

        }


        public override string ToString()
        {
            return id + " - " + name + "  (" + type + ")";
        }



        #region DatabaseEntry Member

        public int getID()
        {
            return id;
        }

        #endregion
    }
}
