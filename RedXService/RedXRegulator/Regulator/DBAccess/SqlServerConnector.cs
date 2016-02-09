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
    public class SqlServerConnector : Generics.GenericDbConnector<RedX.Regulator.System.SysInfo>{
        private bool isSuccessfull;

        public SqlServerConnector(){
            isSuccessfull = false;
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

        public override Boolean Add(params SysInfo[] infos){ 
            isSuccessfull = false;
            foreach (var info in infos)
                isSuccessfull |= Add(info);
            return isSuccessfull;
        }

        private String GetOsByName(String name){
            m_lock.WaitOne();
            OpenConnection();
            using (var lastCommand = new SqlCommand("SELECT id FROM OS WHERE OSname = @name", client)){
                lastCommand.Parameters.AddWithValue("@name", name);
                m_lock.ReleaseMutex();

                using (SqlDataReader reader = lastCommand.ExecuteReader()){
                    if (reader.HasRows){
                        reader.Read();
                        return reader.GetString(0); 
                    }
                }
            }
            return null;
        }

        private String QueryParameters(SysInfo info){
            return "( '" + info.Date.ToString("yyyy/MM/dd HH:mm:ss") + "','" + info.Environment + "'," + info.PercentageRAM + "," + info.PercentageCPU +")";
        }

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

        public Boolean Delete(params SysInfo[] infos){
            isSuccessfull = false;
            foreach (var info in infos)
                isSuccessfull |= Delete(info);
            return isSuccessfull;
        }

        public Boolean Delete(SysInfo info){
            return infoCollection.Remove(info);
        }

        public override void OpenConnection(){
            if (client != null && client.State == ConnectionState.Closed) client.Open();
        }

        protected override void CheckHistory(){
            if (infoCollection.Count > HistoryLength)
                infoCollection.RemoveAt(0);
        }

        public SysInfoCollection History(){
            return infoCollection;
        }
    }
}
