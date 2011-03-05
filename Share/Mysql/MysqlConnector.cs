using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

// need (Groß und Kleinschreibung beachten!)
using MySql.Data.MySqlClient;

namespace MysqlConnect
{
    /// <summary>
    /// MysqlConnector Klasse
    /// Wird für MYSQL Verbindungen benutzt
    /// </summary>
    public class MysqlConnector
    {
        private MySqlConnection connection;
        private IMysql handler;
        private volatile MySqlDataReader reader;

        /// <summary>
        /// Konstruktor der Klasse MysqlConnect
        /// </summary>
        /// <param name="handler">MYsql Handler für Callback</param>
        public MysqlConnector(IMysql handler)
        {
            this.handler = handler;
        }


        /// <summary>
        /// Verbindet zu einem Mysql Server
        /// </summary>
        /// <param name="Host">Host</param>
        /// <param name="Database">Datenbank</param>
        /// <param name="Username">Benutzername</param>
        /// <param name="Password">Passwort</param>
        public void connect(String Host, String Database, String Username, String Password)
        {


            try
            {
                // Create Connection String from Input data
                string myConnectionString = "SERVER=" + Host + ";" +
                                            "DATABASE=" + Database + ";" +
                                            "UID=" + Username + ";" +
                                            "PASSWORD=" + Password + ";";


                connection = new MySqlConnection(myConnectionString);
                connection.Open();



            }

            catch (Exception ex)
            {
                handler.MYSQL_Error(ex);
            }

        }

        /// <summary>
        /// Führt ein Kommando auf dem SQL Server aus
        /// </summary>
        /// <param name="query">SQL Query</param>
        /// <returns>MysqlDataReader für das Auslesen von Querys</returns>
        public MySqlDataReader Query(String query)
        {
            if (connection.State == ConnectionState.Open)
            {

                try
                {
                    lock (connection)
                    {

                        if ((this.reader != null) && (!reader.IsClosed))
                        {
                            reader.Close();
                        }



                        MySqlCommand command = connection.CreateCommand();
                        command.CommandText = query;


                        MySqlDataReader Reader;
                        Reader = command.ExecuteReader();

                        this.reader = Reader;


                        return Reader;
                    }


                }

                catch (Exception ex)
                {
                    handler.MYSQL_Error(ex);
                }



            }


            return null;
        }


        /// <summary>
        /// Schließt die Verbindung
        /// </summary>
        public void Close()
        {
            if (connection.State == ConnectionState.Open)
                connection.Close();
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
            string sql = "";

            foreach (System.Collections.DictionaryEntry entry in hash)
            {
                if (sql != "")
                {
                    sql += ", ";
                }

                sql += "`" + entry.Key + "` = '" + entry.Value + "'";

            }
            string where = "";
            if ((BedName != "") && (BedValue != ""))
            {
                where = " WHERE `" + BedName + "` = '" + BedValue + "'";
            }

            sql = "UPDATE `" + table + "` SET " + sql + where;

            Query(sql).Close();


        }

        /// <summary>
        /// Fügt einen Eintrag in eine Tabelle ein
        /// </summary>
        /// <param name="table">Tabelle</param>
        /// <param name="hash">Werte</param>
        public void addEntry(string table, System.Collections.Hashtable hash)
        {
            string sql1 = "";
            string sql2 = "";

            foreach (System.Collections.DictionaryEntry entry in hash)
            {
                if (sql1 != "") sql1 += ", ";
                if (sql2 != "") sql2 += ", ";

                sql1 += "`" + entry.Key + "`";
                sql2 += "'" + entry.Value + "'";


            }

            string sql = "INSERT INTO `" + table + "` (" + sql1 + ") VALUES (" + sql2 + ");";
            Query(sql).Close();


        }

        /// <summary>
        /// Fügt einen Eintrag in eine Tabelle hibnzu oder aktualisiert ihn
        /// </summary>
        /// <param name="table">Tabelle</param>
        /// <param name="hash">Werte</param>
        public void ReplaceInto(string table, System.Collections.Hashtable hash)
        {
            string sql1 = "";
            string sql2 = "";

            foreach (System.Collections.DictionaryEntry entry in hash)
            {
                if (sql1 != "") sql1 += ", ";
                if (sql2 != "") sql2 += ", ";

                sql1 += "`" + entry.Key + "`";
                sql2 += "'" + entry.Value + "'";


            }



            string sql = "REPLACE INTO `" + table + "` (" + sql1 + ") VALUES (" + sql2 + ");";
            Query(sql).Close();


        }

    }

}
