using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Eine Klasse die Daten über das aktuelle Sonnensystem Bereitstellt
    /// </summary>
    [Serializable]
    public class Solarsystem
    {
        /// <summary>
        /// Name des Sonnensytems
        /// </summary>
        public string name;


        /// <summary>
        /// X Koordiante
        /// </summary>
        public int x;
        /// <summary>
        /// Y Koordinate
        /// </summary>
        public int y;

        /// <summary>
        /// Die Liste aller Verbindungen dieses Sonnensystems
        /// </summary>
        public List<Node> nodes = new List<Node>();

        /// <summary>
        /// Die Liste aller Planeten eines Sonnensytsmes
        /// </summary>
        public List<Planet> planets = new List<Planet>();

        /// <summary>
        /// Handelt es sich um einen Benutzer StartPunkt?
        /// </summary>
        public bool userstart = false;

        /// <summary>
        /// Wandelt ein Sonnensystem in einen String um
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return name;
        }
    }


    /// <summary>
    /// Definiert einen Bereich
    /// </summary>
    [Serializable]
    public class Area
    {
        /// <summary>
        /// Obere Linke Kante - X Koordiante
        /// </summary>
        public int x1;
        /// <summary>
        /// Obere Linke Kante - Y Koordinate
        /// </summary>
        public int y1;
        /// <summary>
        /// Untere Rechte Kante - X Koordinate
        /// </summary>
        public int x2;
        /// <summary>
        /// Untere Rechte Kante - Y Koordiante
        /// </summary>
        public int y2;

    }

    /// <summary>
    /// Die aktuelle Spielkarte
    /// </summary>
    [Serializable]
    public class Map
    {
        /// <summary>
        /// Die Liste aller Sonnensytsme
        /// </summary>
        public List<Solarsystem> solarsystems = new List<Solarsystem>();
        /// <summary>
        /// Der Bereich, in dem Zufällig Planeten erzeugt werden
        /// </summary>
        public Area randomArea = new Area();

        /// <summary>
        /// Die Anzahl an Planeten die Erzeugt werden sollen
        /// </summary>
        public int systemcount = 0;
        /// <summary>
        /// Der Minimale Abstand zwischen zufälligen Planeten
        /// </summary>
        public int min_distance = 30;

    }

}
