using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Game;

namespace Game.Game
{
    /// <summary>
    /// Die vom System verwendeten Resourcen.
    /// Nur diese werden verwendet!
    /// </summary>
    [Serializable]
    public enum ResType
    {
        /// <summary>
        /// Erz
        /// </summary>
        Erz, 
        /// <summary>
        /// Kurbidium
        /// </summary>
        Kurbidium, 
        /// <summary>
        /// Unobtanium
        /// </summary>
        /// <remarks>Muss noch ersetzt werden</remarks>
        Unobtanium

    }

    /// <summary>
    /// Eine Klasse für die "Lagerung" von ResourcenDaten
    /// </summary>
    [Serializable]
    public class ResList : IEnumerable<KeyValuePair<ResType, double>>
    {
       private Dictionary<ResType, double> list = new Dictionary<ResType, double>();

        /// <summary>
        /// Liefert den Wert für einen Bestimmten Resourcen Typ
        /// </summary>
        /// <param name="type">Resourcen Typ</param>
        /// <returns>Betrag des Types in der Liste</returns>
        public double this[ResType type]
        {
            get
            {
                if (list.ContainsKey(type))
                {
                    return list[type];
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                if (list.ContainsKey(type))
                {
                    this.list[type] = value;
                }
                else
                {
                    list.Add(type, value);
                    
                }
            }
  
        }


        /// <summary>
        /// Fügt die Daten der Resourcen in eine HashTabel ein, um Sie in die Datenbank zu speichern
        /// </summary>
        /// <param name="table">HashTable in die die Liste gespeichert werden soll</param>
        public void addIntoHashTable(Dictionary<string,string> table)
        {
            string tmp = "";

            foreach (ResType type in Enum.GetValues(typeof(ResType)))
            {
                if (tmp != "") { tmp += "&"; }
                tmp += type.ToString()+"="+list[type].ToString();
            }

            table.Add("costs", tmp);

        }

        #region IEnumerable<KeyValuePair<ResType,double>> Member

        /// <summary>
        /// Gibt den Enumerator zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<ResType, double>> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return list.GetEnumerator();
        }

        #endregion
    }


}

namespace Game.Game.Static
{

    /// <summary>
    /// Eine Klasse die Methoden zur Ressourcen Verarbeitung bereitstellt
    /// </summary>
    public static class ResHelper
    {
        /// <summary>
        /// Helferfunktion, die Listen, bestehend aus buildres und res, in eine Liste umwandelt,
        /// die Angibt, wieviel Resourcen der Spieler in einer Runde bekommt.
        /// </summary>
        /// <param name="reslist">buildres Liste, die Angibt, welche Resourcen produziert werden.</param>
        /// <param name="res">Liste die angibt wie viel Resourcen pro Runde produziert werden</param>
        /// <returns>Eine Liste die angibt, wieviel Resourcen der Spieler bekommt</returns>
        public static ResList getResAdd(string reslist, string res)
        {
            ResList result = new ResList();



            string[] lista = reslist.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            string[] listb = res.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < lista.Length; i++)
            {
                try
                {
                    string resourceName = lista[i];
                    string resourceCount = listb[i];

                    int resCount = int.Parse(resourceCount);
                    ResType type = (ResType)Enum.Parse(typeof(ResType), resourceName);

                    result[type] = resCount;


                }
                catch
                {
                    //Fehler ...
                    //Möglicherweiße, steht ein unsinner Eintrag in der DB...
                }

            }

            return result;
        }

        /// <summary>
        /// Helferfunktion die eine ResList in 2 Komponenten aufteilt
        /// </summary>
        /// <param name="list">Liste</param>
        /// <returns>[0] = buildres, [1] = resount</returns>
        public static string[] getResText(ResList list)
        {
            string res = "";
            string rescount = "";

            foreach (KeyValuePair<ResType, double> ent in list)
            {
                if (res != "") { res += ", "; }
                if (rescount != "") { rescount += ", "; }

                res += ent.Key.ToString();
                rescount += ent.Value.ToString();

            }

            return new string[] { res, rescount };
        }


    }



}
