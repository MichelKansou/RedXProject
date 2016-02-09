using RedX.Service.IO.Generics;
using RedX.Service.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.IO{
    public class LogWriter : GenericFileWriter{
        public LogWriter(String path, String file) : base(path ?? Const.LogPath, file ?? Const.LogFile) { }

        public new virtual bool WriteIntoFile(String content){
            return _lastOp = base.WriteIntoFile(content);
        }

    }
}
