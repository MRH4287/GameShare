using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MysqlConnect
{
    /// <summary>
    /// Interface, das von einer Klasse, die die Mysql Funktion nutzt, implementiert werden muss.
    /// </summary>
    public interface IMysql
    {
        /// <summary>
        /// Fehler Handling für Mysql
        /// </summary>
        /// <param name="error">Der aufgetretene Fehler</param>
       void MYSQL_Error(Exception error);

    }
}
