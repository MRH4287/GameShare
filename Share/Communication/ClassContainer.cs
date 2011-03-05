using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Communication
{
    /// <summary>
    /// Die Liste aus Klassen, die in einem ClassContainer sein kann
    /// </summary>
    [Serializable]
    public enum ClassType
    {
        /// <summary>
        /// Planeten Klasse
        /// </summary>
        PlanetClass,
        /// <summary>
        /// Rasse
        /// </summary>
        Race,
        /// <summary>
        /// Schiffsklasse
        /// </summary>
        ShipClass,
        /// <summary>
        /// Skill
        /// </summary>
        Skill,
        /// <summary>
        /// Stations Klasse
        /// </summary>
        StationClass,
        /// <summary>
        /// Technologie
        /// </summary>
        Tech,
        /// <summary>
        /// Update
        /// </summary>
        Update,
        /// <summary>
        /// Planet
        /// </summary>
        Planet,
        /// <summary>
        /// Schiff
        /// </summary>
        Ship,
        /// <summary>
        /// Station
        /// </summary>
        Station,
        /// <summary>
        /// Truppen
        /// </summary>
        Troop,
        /// <summary>
        /// User
        /// </summary>
        User,
        /// <summary>
        /// Map
        /// </summary>
        Map,
        /// <summary>
        /// Route
        /// </summary>
        Route,
        /// <summary>
        /// Game Data
        /// </summary>
        GameData,
        /// <summary>
        /// Modul Manager
        /// </summary>
        ModulManager,
        /// <summary>
        /// Main
        /// </summary>
        Main,
        /// <summary>
        /// Keins von diesen
        /// </summary>
        None
    }

    /// <summary>
    /// Der Container der Informationen
    /// </summary>
    [Serializable]
    public class ClassContainer
    {
        /// <summary>
        /// Typ
        /// </summary>
        public Type T = null;
        /// <summary>
        /// Typ der Klasse
        /// </summary>
        public ClassType type = ClassType.None;
        /// <summary>
        /// gespeichertes Objekt
        /// </summary>
        public Object objekt = null;

        /// <summary>
        /// Erstellt einen neuen Classcontainer
        /// </summary>
        /// <typeparam name="T">Typ</typeparam>
        /// <param name="objekt">Objekt</param>
        /// <returns>ClassContainer</returns>
        public static ClassContainer create<T>(T objekt)
        {
            ClassContainer container = new ClassContainer();
            container.objekt = objekt;
            container.T = typeof(T);

            return container;

        }




    }
}
