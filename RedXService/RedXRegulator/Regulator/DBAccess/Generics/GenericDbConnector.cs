using RedX.Regulator.DBAccess.Collection;
using RedX.Regulator.DBAccess.Generic;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess.Generics{
    public abstract class GenericDbConnector<T> : IConnect<T>{

        public static readonly int HistoryLength = 25;
        protected readonly static SysInfoCollection infoCollection = new SysInfoCollection();

        // Mutex, on va empêcher le multi-accès
        protected Mutex                     m_lock;
        protected String                    lastQuery;   // Dernière requête,
        protected SqlConnection             client;      // Connection
        
        protected int                       hResult;     // Handle Result
        protected int?                      nhResult;    // Nullable Handle Result

        public abstract         Boolean Add(T info);
        public abstract         Boolean Add(params T[] infos);
        //public abstract         Boolean Delete(T info);
        //public abstract         Boolean Delete(params T[] infos);
        public abstract         void OpenConnection();
        protected abstract      void CheckHistory();

    }
}
