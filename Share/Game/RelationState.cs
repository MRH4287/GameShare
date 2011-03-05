using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Die Beziehungen die ein Spieler zu einen andern haben kann
    /// </summary>
    [Serializable]
    public enum RelationState
    {
        /// <summary>
        /// Krig
        /// </summary>
        War,
        /// <summary>
        /// Verbündet
        /// </summary>
        Ally,
        /// <summary>
        /// Waffenstillstand
        /// </summary>
        CeaseFire, 
        /// <summary>
        /// HandelsAbkommen
        /// </summary>
        Trade,
        /// <summary>
        /// Neutral
        /// </summary>
        None
    }
}
