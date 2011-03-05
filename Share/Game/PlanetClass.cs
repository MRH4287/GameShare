using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace Game.Game
{
    /// <summary>
    /// PlanetenKlasse
    /// </summary>
    [Serializable]
    public class PlanetClass : IComparable, Interface.DatabaseEntry
    {
        private int id;
        private string name;

        /// <summary>
        /// Welche Schiffe werden Produziert
        /// </summary>
        public List<ShipClass> buildship;
        private string buildship_temp;

        /// <summary>
        /// Liste der Resourcen die jede Runde erstellt werden
        /// </summary>
        public ResList create_res = new ResList();

        /// <summary>
        /// Bild Datei
        /// </summary>
        public System.Drawing.Bitmap picture;


        /// <summary>
        /// ist der Planet bewohnbar?
        /// </summary>
        public bool bewohnbar;

        /// <summary>
        /// Welche Namen kann der Planet haben? (nicht benutzt)
        /// </summary>
        public string names;

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



        private PlanetClass(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        /// <summary>
        /// Erstellt eine neue PlanetenKlasse
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="data">GameData</param>
        /// <returns>PlanetClass</returns>
        public static PlanetClass create(int ID, GameData data)
        {
            MySqlDataReader Reader = data.Query("SELECT * FROM `PX_planeten` WHERE `ID` = '" + ID + "'");
            Reader.Read();


            string name = (string)Reader["Name"];

            string buildres = (string)Reader["buildres"];
            string res = (string)Reader["res"];

            string buildshipS = (string)Reader["buildship"];
            string buildtroopS = (string)Reader["buildtroop"];
            int bewohnbar = (int)Reader["bewohnbar"];


            string names = (string)Reader["names"];

            byte[] picture = (byte[])Reader["bild"];


            PlanetClass planet = new PlanetClass(ID, name);

            planet.names = names;

            planet.create_res = Static.ResHelper.getResAdd(buildres, res);


            // planet.buildtroop_temp = buildtroopS;
            try
            {
                planet.picture = GraphicLibary.GraphicHelper.getPicture(picture);
            }
            catch
            {
                planet.picture = new System.Drawing.Bitmap(1, 1);
            }
            planet.buildship_temp = buildshipS;
            planet.bewohnbar = (bewohnbar == 1);

            Reader.Close();
            return planet;

        }

        /// <summary>
        /// Aktualisiert die Objektrefferenzen, die beim erzeugen des Objektes noch nicht vorhanden waren
        /// </summary>
        public void updateData(GameData data)
        {
            buildship = new List<ShipClass>();

            string[] split = buildship_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    buildship.Add(data.getShipType(id2));
                }
            }

            /*

            buildtroop = new List<TroopClass>();

            string[] split2 = buildtroop_temp.Split(new String[] { ", " }, StringSplitOptions.None);
            foreach (string id in split2)
            {
                if (id != "")
                {
                    int id2 = int.Parse(id);

                    buildtroop.Add(data.getTroopType(id2));
                }
            }

            */
        }


        /// <summary>
        /// Wandelt eine Planeten Klasse in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return id + " - " + this.name;
        }



        #region IComparable Member

        /// <summary>
        /// Vergleicht eine PlanetenKlasse mit einer anderen
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj is PlanetClass)
            {
                PlanetClass temp = (PlanetClass)obj;

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
