using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Eine VErbindung zwischen zwei Systemen
    /// </summary>
    [Serializable]
    public class Node
    {
        /// <summary>
        /// Erster Verbundener Punkt
        /// </summary>
        public Solarsystem pointa;

        /// <summary>
        /// Zweiter Verbundener Punkt
        /// </summary>
        public Solarsystem pointb;

        /// <summary>
        /// Die Entfernung zwischen Punkt A und B
        /// </summary>
        public double distance;

        /// <summary>
        /// Überprüfung ob zwei Knoten gleich sind
        /// </summary>
        /// <param name="obj">Vergleichsobjekt</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Node)) { return false; }

            Node b = (Node)obj;

            return ((pointa == b.pointa) && (pointb == b.pointb) && (distance == b.distance));
        }

        /// <summary>
        /// Liefert den Hash Wert des Knotens
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return pointa.GetHashCode() + pointb.GetHashCode() + distance.GetHashCode();
        }

        /// <summary>
        /// Wandelt einen Knoten in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return pointa.name + "->" + pointb.name;
        }

    }

    /// <summary>
    /// Die Route, die ein Schiff fliegen kann
    /// </summary>
    [Serializable]
    public class Route
    {
        /// <summary>
        /// Der StartPunkt
        /// </summary>
        public Solarsystem start;
        /// <summary>
        /// Der Endpunkt der Route
        /// </summary>
        public Solarsystem end;

        /// <summary>
        /// Die Länge der Route
        /// </summary>
        public double distance;

        /// <summary>
        /// Die Liste aller benutzten Verbindungen
        /// </summary>
        public List<Node> nodelist = new List<Node>();
        /// <summary>
        /// Die Liste aller besuchter Planeten
        /// </summary>
        public List<Solarsystem> systems = new List<Solarsystem>();

        /// <summary>
        /// Entspricht eine Route einer andren
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Route))
            {
                return false;
            }
            Route b = (Route)obj;

            bool ok = true;

            foreach (Node node in nodelist)
            {
                if (!b.nodelist.Contains(node))
                {
                    ok = false;
                    break;
                }
            }


            foreach (Node node in b.nodelist)
            {
                if (!nodelist.Contains(node))
                {
                    ok = false;
                    break;
                }
            }

            return ((start == b.start) && (end == b.end) && ok && (distance == b.distance));



        }

        /// <summary>
        /// Liefert den Hash Wert einer Route
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return start.GetHashCode() + end.GetHashCode() + distance.GetHashCode() + nodelist.GetHashCode();
        }

        /// <summary>
        /// Wandelt eine Route in einen String um
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return start.name + "->" + end.name;
        }

    }


}
