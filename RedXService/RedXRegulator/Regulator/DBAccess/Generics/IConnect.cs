using RedX.Regulator.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Regulator.DBAccess.Generic{
    public interface IConnect<T>{
        void OpenConnection();
        bool Add(T info);
        bool Add(params T[] info);
        //bool Delete(T info);
        //bool Delete(params T[] infos);
    }
}
