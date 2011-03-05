using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MySql.Data.MySqlClient;
using System.Collections;
using Communication;


namespace Game.Game
{
    /// <summary>
    /// Spiel Management Klasse
    /// </summary>
    [Serializable]
    public class GameData
    {
        string prefix = "";

        List<User> users = new List<User>();

        Hashtable shipTypes = new Hashtable();
        List<Ship> ships = new List<Ship>();

        List<Station> stations = new List<Station>();
        Hashtable stationTypes = new Hashtable();

        /// <summary>
        /// Die Liste aller Planeten
        /// </summary>
        public List<Planet> planets = new List<Planet>();
        Hashtable planetTypes = new Hashtable();

        //  List<Troop> troops = new List<Troop>();
        //  Hashtable troopTypes = new Hashtable();

        Hashtable techs = new Hashtable();

        Hashtable races = new Hashtable();

        Hashtable skills = new Hashtable();

        Hashtable updates = new Hashtable();

        /// <summary>
        /// Die Mysql Verbindung
        /// </summary>
        [NonSerialized()]
        public MysqlConnect.MysqlConnector connection = null;


        /// <summary>
        /// Erzeugt einen neuen Spiel Manager
        /// </summary>
        /// <param name="filename">Pfad zur DatenDatei</param>
        public GameData(string filename)
        {
            this.readDatafromFile(filename);
        }

        /// <summary>
        /// Erzeugt einen neuen Spiel Manager
        /// </summary>
        /// <param name="connection">Verbindung</param>
        /// <param name="prefix">Prefix</param>
        public GameData(MysqlConnect.MysqlConnector connection, string prefix)
        {
            //Stellt eine Verbindung zum Server her
            this.connection = connection;
            this.prefix = prefix;



            //Rassen
            refreshRaceList();

            //Läd die Benutzerdaten in eine Liste
            refreshUserList();

            //Läd Schiffsdaten
            refreshShipClassList();

            //Läd stationsdaten
            refreshStationClassList();


            //Läd Planetendaten
            refreshPlanetClassList();


            //Läd TruppenDaten
            // refreshTroopClassList();
            // refreshTroopList();

            //Tech
            refreshTechList();


            //Skill
            refreshSkillList();

            //Updates
            refreshUpdateList();


            updateData();
        }
        /// <summary>
        /// Aktualisiert alle Refferenzen
        /// Beötigt, wenn z.B eine Schiffsklasse veränert wurde.
        /// </summary>
        public void updateData()
        {
            foreach (DictionaryEntry station in stationTypes)
            {
                ((StationClass)(station.Value)).updateData(this);
            }

            foreach (DictionaryEntry ship in shipTypes)
            {
                ((ShipClass)(ship.Value)).updateData(this);
            }

            foreach (DictionaryEntry planet in planetTypes)
            {
                ((PlanetClass)(planet.Value)).updateData(this);
            }


            /*   foreach (DictionaryEntry troop in troopTypes)
               {
                   ((TroopClass)(troop.Value)).updateData(this);
               }  */

            foreach (DictionaryEntry tech in techs)
            {
                ((Tech)(tech.Value)).updateData(this);
            }

            foreach (DictionaryEntry update in updates)
            {
                ((Update)(update.Value)).updateData(this);
            }

        }

        /// <summary>
        /// Aktualisiert die Benutzerliste
        /// </summary>
        public void refreshUserList()
        {
            MySqlDataReader Reader = Query("SELECT * FROM `PX_user`;");
            users.Clear();

            if (Reader != null)
                while (Reader.Read())
                {
                    User userO = new User((int)Reader["ID"], (string)Reader["user"], (string)Reader["level"], getRace((int)Reader["race"]));

                    userO.setData((string)Reader["name"], (string)Reader["nachname"], (string)Reader["email"], (string)Reader["pass"], (int)Reader["lastlogin"]);


                    users.Add(userO);

                }

            Reader.Close();
        }

        /// <summary>
        /// Aktualisiert die Technologien
        /// </summary>
        public void refreshTechList()
        {
            techs.Clear();

            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_tech`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                techs.Add(id, Tech.create(id, this));
            }
        }

        /// <summary>
        /// Aktualisiert die Updates
        /// </summary>
        public void refreshUpdateList()
        {
            updates.Clear();

            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_update`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                updates.Add(id, Update.create(id, this));
            }
        }



        /// <summary>
        /// Aktualisiert die Skills
        /// </summary>
        public void refreshSkillList()
        {
            skills.Clear();

            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_skills`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                skills.Add(id, Skill.create(id, this));
            }
        }

        /// <summary>
        /// Aktualisiert die Rassen
        /// </summary>
        public void refreshRaceList()
        {
            races.Clear();


            MySqlDataReader Reader = Query("SELECT * FROM `PX_race`;");

            while (Reader.Read())
            {
                int id = (int)Reader["ID"];
                string name = (string)Reader["name"];

                races.Add(id, new Race(id, name));


            }
            Reader.Close();

        }


        /// <summary>
        /// Aktualisiert die Liste der Schiffsklassen
        /// </summary>
        public void refreshShipClassList()
        {
            shipTypes.Clear();
            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_ships`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                shipTypes.Add(id, ShipClass.create(id, this));
            }


        }

        /// <summary>
        /// Aktualisiert die Liste der Stationsklassen
        /// </summary>
        public void refreshStationClassList()
        {
            stationTypes.Clear();
            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_stations`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                stationTypes.Add(id, StationClass.create(id, this));
            }


        }





        /// <summary>
        /// Aktualisiert die Liste der PlanetenKlassen
        /// </summary>
        public void refreshPlanetClassList()
        {
            planetTypes.Clear();
            List<int> ids = new List<int>();
            MySqlDataReader Reader = Query("SELECT * FROM `PX_planeten`;");

            while (Reader.Read())
            {
                ids.Add((int)Reader["ID"]);

            }
            Reader.Close();

            foreach (int id in ids)
            {
                planetTypes.Add(id, PlanetClass.create(id, this));
            }


        }



        /// <summary>
        /// Mysql Query
        /// </summary>
        /// <param name="query">Query String</param>
        /// <returns>DataReader</returns>
        public MySqlDataReader Query(String query)
        {

            query = query.Replace("PX_", prefix);


            return connection.Query(query);


        }


        /// <summary>
        /// Liefert einen bestimmten Benutzer zurück
        /// </summary>
        /// <param name="id">Benutzer ID</param>
        /// <returns>Benutzer</returns>
        public User getUser(int id)
        {

            foreach (User user in users)
            {
                if (user.id == id)
                {
                    return user;
                }


            }

            return null;
        }


        /// <summary>
        /// Liefert die UserListe zurück
        /// </summary>
        /// <returns>BenutzerListe</returns>
        public List<User> getUsers()
        {
            return users;
        }

        /// <summary>
        /// Liefert alle zurzeit vorhandenen Schiffe zurück
        /// </summary>
        /// <returns>Schiffe</returns>
        public List<Ship> getShips()
        {
            return ships;
        }

        /// <summary>
        /// Liefert alle zurzeit vorhandenen Stationen zurück
        /// </summary>
        /// <returns>Schiffe</returns>
        public List<Station> getStations()
        {
            return stations;
        }


        /// <summary>
        /// Liefert alle zurzeit vorhandenen Planeten zurück
        /// </summary>
        /// <returns>Schiffe</returns>
        public List<Planet> getPlanets()
        {
            return planets;
        }


        /*
        /// <summary>
        /// Liefert alle zurzeit vorhandenen Truppen zurück
        /// </summary>
        /// <returns>Schiffe</returns>
        public List<Troop> getTroops()
        {
            return troops;
        }


        /// <summary>
        /// Liefert einene bestimmten Truppentyp zurück
        /// </summary>
        /// <param name="type">ID der Truppe</param>
        /// <returns>TroopClass Objekt</returns>
        public TroopClass getTroopType(int type)
        {
            return (TroopClass)troopTypes[type];
        }
        */

        /// <summary>
        /// Liefert einene bestimmte Rasse zurück
        /// </summary>
        /// <param name="race">ID der Rasse</param>
        /// <returns>Race Objekt</returns>
        public Race getRace(int race)
        {
            return (Race)races[race];
        }


        /// <summary>
        /// Liefert einen bestimmten PlanetTyp zurück
        /// </summary>
        /// <param name="type">ID des Planetens</param>
        /// <returns>PlanetClass Objekt</returns>
        public PlanetClass getPlanetType(int type)
        {
            return (PlanetClass)planetTypes[type];
        }


        /// <summary>
        /// Liefert eine bestimmte Technologie zurück
        /// </summary>
        /// <param name="type">ID der Technologie</param>
        /// <returns>Tech Objekt</returns>
        public Tech getTech(int type)
        {
            return (Tech)techs[type];
        }


        /// <summary>
        /// Liefert einen bestimmten Shiffstyp zurück
        /// </summary>
        /// <param name="type">ID des Schiffes</param>
        /// <returns>ShipClass Objekt</returns>
        public ShipClass getShipType(int type)
        {
            return (ShipClass)shipTypes[type];
        }

        /// <summary>
        /// Liefert einen bestimmten Stationstyp zurück
        /// </summary>
        /// <param name="type">ID der Station</param>
        /// <returns>StationClass Objekt</returns>
        public StationClass getStationType(int type)
        {
            return (StationClass)stationTypes[type];
        }

        /// <summary>
        /// Liefert einen bestimmten Skill zurück
        /// </summary>
        /// <param name="type">ID des Skills</param>
        /// <returns>Skill Objekt</returns>
        public Skill getSkill(int type)
        {
            return (Skill)skills[type];
        }


        /// <summary>
        /// Liefert einbestimmtes Update zurück
        /// </summary>
        /// <param name="type">ID des Updates</param>
        /// <returns>Update Objekt</returns>
        public Update getUpdate(int type)
        {
            return (Update)updates[type];
        }


        /*
        /// <summary>
        /// Liefert alle TruppenKlassen zurück
        /// </summary>
        /// <returns>Alle TruppenKlassen</returns>
        public List<TroopClass> getTroopTypes()
        {
            List<TroopClass> list = new List<TroopClass>();
            foreach (DictionaryEntry entry in troopTypes)
            {
                list.Add((TroopClass)entry.Value);
                list.Sort();
            }

            return list;
        }  */

        /// <summary>
        /// Liefert alle Updates zurück
        /// </summary>
        /// <returns>Alle TruppenKlassen</returns>
        public List<Update> getUpdates()
        {
            List<Update> list = new List<Update>();
            foreach (DictionaryEntry entry in updates)
            {
                list.Add((Update)entry.Value);
                list.Sort();
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Stationsklassen zurück
        /// </summary>
        /// <returns>Alle Schiffsklassen</returns>
        public List<StationClass> getStationTypes()
        {
            List<StationClass> list = new List<StationClass>();
            foreach (DictionaryEntry entry in stationTypes)
            {
                list.Add((StationClass)entry.Value);
                list.Sort();
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Skills zurück
        /// </summary>
        /// <returns>Alle Skills</returns>
        public List<Skill> getSkills()
        {
            List<Skill> list = new List<Skill>();
            foreach (DictionaryEntry entry in skills)
            {
                list.Add((Skill)entry.Value);
                list.Sort();
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Rassen zurück
        /// </summary>
        /// <returns>Alle Rassev</returns>
        public List<Race> getRaces()
        {
            List<Race> list = new List<Race>();
            foreach (DictionaryEntry entry in races)
            {
                list.Add((Race)entry.Value);
                list.Sort();
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Schiffsklassen zurück
        /// </summary>
        /// <returns>Alle Schiffsklassen</returns>
        public List<ShipClass> getShipTypes()
        {
            List<ShipClass> list = new List<ShipClass>();
            foreach (DictionaryEntry entry in shipTypes)
            {
                list.Add((ShipClass)entry.Value);
                list.Sort();
            }

            return list;
        }

        /// <summary>
        /// Liefert alle Technologien zurück
        /// </summary>
        /// <returns>Alle Technologien</returns>
        public List<Tech> getTechs()
        {
            List<Tech> list = new List<Tech>();
            foreach (DictionaryEntry entry in techs)
            {
                list.Add((Tech)entry.Value);
                list.Sort();

            }

            return list;
        }


        /// <summary>
        /// Liefert alle PlanetenKlassen zurück
        /// </summary>
        /// <returns>Alle PlanetenKlassen</returns>
        public List<PlanetClass> getPlanetTypes()
        {
            List<PlanetClass> list = new List<PlanetClass>();
            foreach (DictionaryEntry entry in planetTypes)
            {
                list.Add((PlanetClass)entry.Value);
                list.Sort();
            }

            return list;
        }



        /// <summary>
        /// Liefert den UINX Timestamp
        /// </summary>
        /// <returns>Zeit in Sekunden seit dem 1.1.1970</returns>
        public static int getTimestamp()
        {
            TimeSpan t = (DateTime.UtcNow - new DateTime(1970, 1, 1));
            return (int)t.TotalSeconds;

        }

        /// <summary>
        /// Wandelt einen Timestamp in ein DateTime Objekt um
        /// </summary>
        /// <param name="Timestamp">UNIX Timestamp</param>
        /// <returns></returns>
        public static System.DateTime Timestamp2Date(int Timestamp)
        {
            //  gerechnet wird ab der UNIX Epoche
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            // den Timestamp addieren           
            dateTime = dateTime.AddSeconds(Timestamp);

            return dateTime.ToLocalTime();
        }

        /// <summary>
        /// Strukt für die Timestamp2Text Methode
        /// </summary>
        public struct TimeData
        {
            /// <summary>
            /// Die Anzahl der Sekunden
            /// </summary>
            public int seconds;
            /// <summary>
            /// Die Anzahl an Minuten
            /// </summary>
            public int minutes;
            /// <summary>
            /// Die Anzahl an Stunden
            /// </summary>
            public int hours;
            /// <summary>
            /// Die Amzahl an Tagen
            /// </summary>
            public int days;
            /// <summary>
            /// Der Text
            /// </summary>
            public string text;
        }

        /// <summary>
        /// Wandelt den Timestamp in die einzelnen Komponenten um
        /// </summary>
        /// <param name="timestamp">Timestamp</param>
        /// <returns></returns>
        public static TimeData Timestamp2Text(int timestamp)
        {
            TimeData data = new TimeData();

            int seconds = timestamp;
            int min;
            int hours;
            int days;

            //Minutenberechnung Rest Sekunden
            float min1 = ((float)seconds / 60);
            if (min1 >= 1)
            {
                min = (int)Math.Floor(min1);
                seconds = (int)Math.Round((min1 - min) * 60);
            }
            else
            {
                min = 0;
            }


            //Stundenberechnung Rest Minuten
            float hour1 = ((float)min / 60);
            if (hour1 >= 1)
            {
                hours = (int)Math.Floor(hour1);
                min = (int)Math.Round((hour1 - hours) * 60);
            }
            else
            {
                hours = 0;
            }


            //Tagesberechnung Rest Stunden
            float day1 = ((float)hours / 24);
            if (day1 >= 1)
            {
                days = (int)Math.Floor(day1);
                hours = (int)Math.Round((day1 - days) * 24);
            }
            else
            {
                days = 0;
            }

            data.seconds = seconds;
            data.minutes = min;
            data.hours = hours;
            data.days = days;

            string text = "";

            if (seconds != 0)
            {
                text = seconds + " Sekunden";
            }
            if (min != 0)
            {
                text = min + " Minuten " + text;
            }
            if (hours != 0)
            {
                text = hours + " Stunden " + text;
            }
            if (days != 0)
            {
                text = days + " Tage " + text;
            }

            data.text = text;



            return data;

        }


        /// <summary>
        /// Liefert eine Liste in der Form 1, 2, 3 entsprechend der ID`s der Einträge zurück
        /// </summary>
        /// <param name="list">Einträge</param>
        /// <returns>Liste</returns>
        public static string getIdList(List<Interface.DatabaseEntry> list)
        {
            string text = "";

            foreach (Interface.DatabaseEntry entry in list)
            {
                if (text != "") { text += ", "; }

                text += entry.getID().ToString();

            }

            return text;

        }


        /// <summary>
        /// Liefert eine Liste in der Form 1, 2, 3 entsprechend der ID`s der Einträge zurück
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string getIdList<T>(List<T> list)
            where T : Interface.DatabaseEntry
        {
            try
            {
                List<Interface.DatabaseEntry> entryList = new List<Interface.DatabaseEntry>();
                foreach (T item in list)
                {
                    entryList.Add(item);
                }

                return getIdList(entryList);


            }
            catch
            {
                return "";
            }
        }



        /// <summary>
        /// Aktualisiert eine Tabelle
        /// </summary>
        /// <param name="table">Zu aktualisierende Tabelle</param>
        /// <param name="hash">Werte die verändert werden</param>
        /// <param name="BedName">WHERE `# Dieser Wert #` = '...'</param>
        /// <param name="BedValue">WHERE `...` = '# Dieser Wert #'</param>
        public void updateTable(string table, System.Collections.Hashtable hash, string BedName, string BedValue)
        {
            table = table.Replace("PX_", prefix);

            connection.updateTable(table, hash, BedName, BedValue);


        }

        /// <summary>
        /// Aktualisiert eine Tabelle
        /// </summary>
        /// <param name="table">Zu aktualisierende Tabelle</param>
        /// <param name="hash">Werte die verändert werden</param>
        public void replaceIntoTable(string table, System.Collections.Hashtable hash)
        {
            table = table.Replace("PX_", prefix);

            connection.ReplaceInto(table, hash);


        }

        /// <summary>
        /// Fügt einen Eintrag in eine Tabelle ein
        /// </summary>
        /// <param name="table">Tabelle</param>
        /// <param name="hash">Werte</param>
        public void addEntry(string table, System.Collections.Hashtable hash)
        {
            table = table.Replace("PX_", prefix);

            connection.addEntry(table, hash);
        }


        /// <summary>
        /// Läd die Spiel Dateien aus einer Datei
        /// </summary>
        /// <param name="filename"></param>
        private void readDatafromFile(string filename)
        {
            Translator tr = new Communication.Translator();


            List<ClassContainer> list = tr.readData(filename);

            planetTypes = new Hashtable();
            races = new Hashtable();
            shipTypes = new Hashtable();
            skills = new Hashtable();
            stationTypes = new Hashtable();
            techs = new Hashtable();
            // troopTypes = new Hashtable();
            updates = new Hashtable();

            foreach (ClassContainer Container in list)
            {
                ClassType type = Container.type;



                switch (type)
                {
                    case ClassType.PlanetClass:

                        PlanetClass objekt = (PlanetClass)Container.objekt;
                        this.planetTypes.Add(objekt.Id, objekt);

                        break;

                    case ClassType.Race:
                        Race objekt1 = (Race)Container.objekt;
                        this.races.Add(objekt1.id, objekt1);
                        break;

                    case ClassType.ShipClass:
                        ShipClass shipclass = (ShipClass)Container.objekt;
                        this.shipTypes.Add(shipclass.Id, shipclass);
                        break;

                    case ClassType.Skill:
                        Skill skill = (Skill)Container.objekt;
                        this.skills.Add(skill.Id, skill);
                        break;

                    case ClassType.StationClass:
                        StationClass stationclass = (StationClass)Container.objekt;
                        this.stationTypes.Add(stationclass.Id, stationclass);
                        break;

                    case ClassType.Tech:
                        Tech tech = (Tech)Container.objekt;
                        this.techs.Add(tech.Id, tech);
                        break;

                    //  case ClassType.TroopClass:
                    //      TroopClass troopclass = (TroopClass)Container.objekt;
                    //      this.troopTypes.Add(troopclass.Id, troopclass);
                    //      break;

                    case ClassType.Update:
                        Update update = (Update)Container.objekt;
                        this.updates.Add(update.Id, update);
                        break;

                    case ClassType.None:
                        break;
                }

            }




        }

        /// <summary>
        /// Speicher die Daten der GameData Klasse in eine Datei
        /// </summary>
        /// <param name="filename"></param>
        public void writeIntoFile(string filename)
        {
            Translator tr = new Communication.Translator();
            List<ClassContainer> list = new List<ClassContainer>();


            foreach (DictionaryEntry station in stationTypes)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.StationClass;

                container.objekt = station.Value;

                list.Add(container);
            }

            foreach (DictionaryEntry ship in shipTypes)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.ShipClass;

                container.objekt = ship.Value;

                list.Add(container);
            }

            foreach (DictionaryEntry planet in planetTypes)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.PlanetClass;

                container.objekt = planet.Value;

                list.Add(container);
            }


            /*   foreach (DictionaryEntry troop in troopTypes)
               {
                   ClassContainer container = new ClassContainer();
                   container.type = ClassType.TroopClass;
         
                   container.objekt = troop.Value;
          
                  list.Add(container);
               }   */

            foreach (DictionaryEntry tech in techs)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.Tech;

                container.objekt = tech.Value;

                list.Add(container);
            }

            foreach (DictionaryEntry update in updates)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.Update;

                container.objekt = update.Value;

                list.Add(container);
            }



            foreach (DictionaryEntry skill in skills)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.Skill;

                container.objekt = skill.Value;

                list.Add(container);
            }


            foreach (DictionaryEntry race in races)
            {
                ClassContainer container = new ClassContainer();
                container.type = ClassType.Race;

                container.objekt = race.Value;

                list.Add(container);
            }


            tr.writeData(list, filename);


        }

        /// <summary>
        /// Fügt einen Spieler dem Spiel hinzu
        /// </summary>
        /// <param name="user">Spieler</param>
        public void addUser(User user)
        {
            if (user != null)
            {
                int id = 0;
                foreach (User u in users)
                {
                    if (u.id > id)
                    {
                        id = u.id;
                    }
                }

                user.id = id + 1;

                users.Add(user);
            }
        }


        /// <summary>
        /// Fügt einen Planeten dem Spiel hinzu
        /// </summary>
        /// <param name="planet">Planet</param>
        public void addPlanet(Planet planet)
        {
            if (planet != null)
            {
                planets.Add(planet);
            }
        }

        /// <summary>
        /// Fügt ein Schiff dem Spiel hinzu
        /// </summary>
        /// <param name="ship">Schiff</param>
        public void addShip(Ship ship)
        {
            if (ship != null)
            {
                ships.Add(ship);
            }
        }

        /// <summary>
        /// Fügt eine Station dem Spiel hinzu
        /// </summary>
        /// <param name="station">Station</param>
        public void addStation(Station station)
        {
            if (station != null)
            {
                stations.Add(station);
            }
        }

        /// <summary>
        /// Entfernt ein Schiff aus dem Spiel
        /// </summary>
        /// <param name="ship">Schff</param>
        public void removeShip(Ship ship)
        {
            if (ship != null)
            {
                ships.Remove(ship);
            }
        }

        /// <summary>
        /// Entfernt eine Station aus dem Spiel
        /// </summary>
        /// <param name="station">Station</param>
        public void removeStation(Station station)
        {
            if (station != null)
            {
                stations.Remove(station);
            }
        }

    }
}
