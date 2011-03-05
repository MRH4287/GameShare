using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Communication
{

    /// <summary>
    /// Das Kommando, die zwischen server und Client ausgetauscht werden
    /// </summary>
    [Serializable]
    public class Command
    {
        /// <summary>
        /// Befehl an den Server oder Client
        /// </summary>
        public string command;
        /// <summary>
        /// Die Argumente für diesen Befehl
        /// </summary>
        public List<Object> Arguments = new List<object>();


    }
}
