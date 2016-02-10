using RedX.Service.IO.Generics;
using RedX.Service.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.IO{
    /// <summary>
    /// Custom logger for the Service
    /// </summary>
    public class LogWriter : GenericFileWriter{
        public LogWriter(String path, String file) : base(path ?? Const.LogPath, file ?? Const.LogFile) { }
        /// <summary>
        /// Store a content in a file.
        /// </summary>
        /// <param name="content"> Text to add, often events.</param>
        /// <returns>The text is successfully stored or not </returns>
        public new virtual bool WriteIntoFile(String content){
            return _lastOp = base.WriteIntoFile(content);
        }

    }
}
