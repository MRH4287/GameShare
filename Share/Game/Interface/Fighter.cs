using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game.Interface
{
    /// <summary>
    /// Interface um alle Schiffe / Stationen die Kämpfen können zusammenzufassen
    /// </summary>
    [Serializable]
    public abstract class Fighter
    {

        /// <summary>
        /// Das Leben eines Kämpfers
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
        /// Typ des Kämpfers
        /// </summary>
        public FighterType fighterTyp = FighterType.NONE;

    }

    /// <summary>
    /// Die Typen der Truppen, die Kämpfen können
    /// </summary>
    [Serializable]
    public enum FighterType
    {
        /// <summary>
        /// Schiffe
        /// </summary>
        SHIP,
        /// <summary>
        /// Stationen
        /// </summary>
        STATION,
        /// <summary>
        /// Bodentruppen
        /// </summary>
        TROOP,
        /// <summary>
        /// Kein
        /// </summary>
        NONE
    }

}
