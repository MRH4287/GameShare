using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Game
{
    /// <summary>
    /// Eine Spiel Excption
    /// </summary>
    [Serializable]
    public class GameException : Exception
    {
        /// <summary>
        /// Erzeugt eine neue GameExcpetion
        /// </summary>
        /// <param name="message">Meldung</param>
        public GameException(string message)
            : base(message)
        {

        }

        /// <summary>
        /// Erzeugt eine neue GameExcpetion
        /// </summary>
        public GameException()
            : base()
        {

        }

    }
}
