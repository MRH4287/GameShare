using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;



namespace Communication
{
    /// <summary>
    /// Die Klasse die zur Kommunikation mit den Clients dient
    /// </summary>
    public class Translator
    {


        /// <summary>
        /// Serialisiert ein Objekt in ein byte Array
        /// </summary>
        /// <param name="input">Objekt das Serialisiert werden soll</param>
        /// <returns>Serialisiertes Objekt</returns>
        public byte[] writeCommand(Command input)
        {

            MemoryStream stream = new MemoryStream();

            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, input);


            byte[] result = stream.GetBuffer();

            stream.Close();

            return result;

        }


        /// <summary>
        /// Deserialisiert ein Objekt
        /// </summary>
        /// <param name="input">Byte Array</param>
        /// <returns>Command Objekt</returns>
        public Command getCommand(byte[] input)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();

            stream.Write(input, 0, input.Length);

            stream.Position = 0;

            return (Command)formatter.Deserialize(stream);

        }


        /// <summary>
        /// Schreibt eine Liste aus Objekten in eine Datei
        /// </summary>
        /// <param name="list">Die Liste der Objekte</param>
        /// <param name="filename">Dateiname</param>
        public void writeData(List<ClassContainer> list, string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.Create);
            IFormatter formatter = new BinaryFormatter();

            formatter.Serialize(stream, list);

            stream.Close();


        }

        /// <summary>
        /// Liest eine Liste Objekten aus einer Datei
        /// </summary>
        /// <param name="filename">Dateiname</param>
        /// <returns>Liste aus Objekten</returns>
        public List<ClassContainer> readData(string filename)
        {
            FileStream stream = new FileStream(filename, FileMode.Open);
            IFormatter formatter = new BinaryFormatter();

            stream.Position = 0;

            List<ClassContainer> list = (List<ClassContainer>)formatter.Deserialize(stream);

            stream.Close();
            return list;


        }


    }
}
