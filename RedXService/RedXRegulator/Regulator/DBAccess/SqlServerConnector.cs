using RedX.Regulator.DBAccess.Collection;
using RedX.Regulator.DBAccess.Generics;
using RedX.Regulator.System;
using RedX.Regulator.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess{
    /// <summary>
    /// Data Access Layer, will be connected to the base (won't be used directly by the user through a raw connection).
    /// </summary>
    public class SqlServerConnector : Generics.GenericDbConnector<RedX.Regulator.System.SysInfo>{
        private bool isSuccessful;

        public SqlServerConnector(){
            isSuccessful = false;

            // Mutex to avoid Concurrency
            m_lock = new Mutex();

            client = new SqlConnection();
            client.ConnectionString = Const.SOURCE + ";" + Const.DBNAME + ";" + Const.INSECU;
            SqlCommand lastCommand;

            OpenConnection();

            foreach (DataRow row in client.GetSchema(Const.TABLE).Rows){
                (lastCommand = new SqlCommand("SELECT COUNT(*) FROM information_schema.columns WHERE table_name = @tabName;", client)).Parameters.Add("@tabName", SqlDbType.VarChar).Value = (string)row[2];
                var value = Convert.ToInt32(lastCommand.ExecuteScalar());
            }
        }

        /// <summary>
        /// Add several SysInfo structures
        /// </summary>
        /// <param name="infos"></param>
        /// <returns>Successful </returns>
        public override Boolean Add(params SysInfo[] infos){
            isSuccessful = false;
            foreach (var info in infos)
                isSuccessful |= Add(info);
            return isSuccessful;
        }

        /// <summary>
        /// Get an OS id by its name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Obsolete("Used in the Prior version of the Database.")]
        private String GetOsByName(String name){
            m_lock.WaitOne();
            OpenConnection();

            using (var lastCommand = new SqlCommand("SELECT id FROM OS WHERE OSname = @name", client)){
                lastCommand.Parameters.AddWithValue("@name", name);
                
                using (SqlDataReader reader = lastCommand.ExecuteReader()){
                    if (reader.HasRows){
                        reader.Read();
                        m_lock.ReleaseMutex();

                        return reader.GetString(0); 
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Set up parameters for SQL Insert 
        /// </summary>
        /// <param name="info">Structure to use</param>
        /// <returns></returns>
        private String QueryParameters(SysInfo info){
            return "( '" + info.Date.ToString("yyyy/MM/dd HH:mm:ss") + "','" + info.Environment + "'," + info.PercentageRAM + "," + info.PercentageCPU +")";
        }

        /// <summary>
        /// Add data (from SysInfo structure)
        /// </summary>
        /// <param name="info"></param>
        /// <returns> If the operation failed</returns>
        public override Boolean Add(SysInfo info){
            lastQuery = Const.INSERT.Replace("@table", Const.PERFORMANCE_TABLE) + QueryParameters(info);

            m_lock.WaitOne();
            CheckHistory();
            infoCollection.Add(info);
            OpenConnection();

            using (var lastCommand = new SqlCommand(lastQuery, client)){
                hResult = lastCommand.ExecuteNonQuery();
            }

            m_lock.ReleaseMutex();
            return (hResult <= 0); 
        }

        /// <summary>
        /// Remove values from the History
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        public Boolean Delete(params SysInfo[] infos){
            isSuccessful = false;
            foreach (var info in infos)
                isSuccessful |= Delete(info);
            return isSuccessful;
        }

        public Boolean Delete(SysInfo info){
            return infoCollection.Remove(info);
        }

        public override void OpenConnection(){
            if (client != null && client.State == ConnectionState.Closed) client.Open();
        }

        /// <summary>
        /// Clear the temporary history if its size is higher than the Max Length
        /// </summary>
        protected override void CheckHistory(){
            if (infoCollection.Count > HistoryLength)
                infoCollection.RemoveAt(0);
        }

        public SysInfoCollection History(){
            return infoCollection;
        }
    }
}
