using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Game.Game
{
    /// <summary>
    /// Repräsentiert einen Spieler
    /// </summary>
    [Serializable]
    public class User : Interface.DatabaseEntry
    {


        private int ID;
        private string Username;
        private string Name, Nachname;
        private string Email;
        private string Level;
        private string Password;
        private Race Race;
        private int lastlogin;

        #region public vars

        /// <summary>
        /// Die BenutzerID
        /// </summary>
        public int id
        {
            get
            {
                return ID;
            }
            set
            {
                ID = value;
            }
        }

        /// <summary>
        /// Der Benutzername
        /// </summary>
        public string username
        {
            get
            {
                return Username;
            }
        }

        /// <summary>
        /// Der Vorname
        /// </summary>
        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                if (value != "")
                    Name = value;
            }

        }

        /// <summary>
        /// Der Nachname
        /// </summary>
        public string nachname
        {
            get
            {
                return Nachname;
            }
            set
            {
                if (value != "")
                    Nachname = value;
            }
        }

        /// <summary>
        /// Die Email Adresse
        /// </summary>
        public string email
        {
            get
            {
                return Email;
            }
            set
            {
                if (value != "")
                    Email = value;
            }
        }

        /// <summary>
        /// Das Level
        /// </summary>
        public string level
        {
            get
            {
                return Level;
            }
            set
            {
                if (value != "")
                    Level = value;
            }
        }

        /// <summary>
        /// Setzt das Passwort
        /// </summary>
        public string password
        {
            get
            {
                return Password;
            }
            set
            {
                if (value != "")
                    Password = value;
            }
        }



        /// <summary>
        /// Die Rasse
        /// </summary>
        public Race race
        {
            get
            {
                return Race;
            }
            set
            {
                Race = value;
            }
        }

        /// <summary>
        /// Der Timstamp vom letztem Login
        /// </summary>
        public int Lastlogin
        {
            get
            {
                return lastlogin;
            }
            set
            {
                this.lastlogin = value;
            }
        }

        #endregion


        /// <summary>
        /// Erstellt ein neues Benutzerobjekt
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="username"></param>
        /// <param name="level"></param>
        /// <param name="race"></param>
        public User(int ID, string username, string level, Race race)
        {
            this.ID = ID;
            this.Username = username;
            this.Level = level;
            this.Race = race;
        }


        /// <summary>
        /// Übergibt zusützliche Informationen
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="nachname">Nachname</param>
        /// <param name="email">E-Mail</param>
        /// <param name="password">Passwort</param>
        /// <param name="lastlogin">Letzter Login</param>
        public void setData(string name, string nachname, string email, string password, int lastlogin)
        {
            this.Name = name;
            this.Nachname = nachname;
            this.Email = email;
            this.Password = password;
            this.lastlogin = lastlogin;


        }

        /// <summary>
        /// Gibt die BenutzerDaten als String
        /// </summary>
        /// <returns>Benutzerdaten</returns>
        public override string ToString()
        {
            return id + " - " + username;

        }


        /// <summary>
        /// Verhasht das angegebene Passwort um es sicher zu speichern 
        /// </summary>
        public void encryptPassword()
        {
            this.password = GetMD5Hash(this.password);
        }


        /// <summary>
        /// Gibt einen MD5 Hash als String zurück
        /// </summary>
        /// <param name="TextToHash">string der Gehasht werden soll.</param>
        /// <returns>Hash als string.</returns>
        public static string GetMD5Hash(string TextToHash)
        {
            //Prüfen ob Daten übergeben wurden.
            if ((TextToHash == null) || (TextToHash.Length == 0))
            {
                return string.Empty;
            }

            //MD5 Hash aus dem String berechnen. Dazu muss der string in ein Byte[]
            //zerlegt werden. Danach muss das Resultat wieder zurück in ein string.
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] textToHash = Encoding.Default.GetBytes(TextToHash);
            byte[] result = md5.ComputeHash(textToHash);

            string tmp = System.BitConverter.ToString(result);
            tmp = tmp.Replace("-", "");
            tmp = tmp.ToLower();

            return tmp;
        }



        #region DatabaseEntry Member

        /// <summary>
        /// Liefert die Datenbank ID zurück
        /// </summary>
        /// <returns>Liefert die Datenbank ID</returns>
        public int getID()
        {
            return id;
        }

        #endregion
    }
}
