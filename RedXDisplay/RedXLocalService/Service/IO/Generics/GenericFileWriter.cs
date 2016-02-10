using RedX.Service.IO.IWriter;
using RedX.Service.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.IO.Generics{
    /// <summary>
    /// Generic writer.
    /// </summary>
    public class GenericFileWriter : IFWriter {
        protected String _path;
        protected String _file;
        protected String _final;

        protected bool _lastOp;

        public GenericFileWriter(String path, String file)
        {
            if (path.Equals("")) path = null;
            if (file.Equals("")) file = null;

            _path = path ?? Const.OutPath;
            _file = file ?? Const.OutFile;
            _lastOp = CreateFile();
        }


        /// <summary>
        /// Write into a file.
        /// </summary>
        /// <param name="content"></param>
        /// <returns>Success or not</returns>
        public virtual bool WriteIntoFile(String content){
            try{
                using (var file = new System.IO.StreamWriter(_final, true)){
                    file.WriteLine(content);
                }
            }
            catch{
                return _lastOp = false;
            }
            return _lastOp = true;
        }

        /// <summary>
        /// Check if a directory exists or not
        /// </summary>
        /// <returns>Exists.</returns>
        public bool CheckDirectory(){
            return Directory.Exists(_path);
        }

        /// <summary>
        /// Create the target directory if this one does not exist.
        /// </summary>
        /// <returns></returns>
        public bool CreateDirectory(){
            if (!CheckDirectory())
                return (_lastOp |= (Directory.CreateDirectory(_path) != null));
            return false;
        }

        /// <summary>
        /// Create the targeted file
        /// </summary>
        /// <returns></returns>
        public virtual bool CreateFile(){
            _lastOp = CreateDirectory();
            FileStream fs;
            _lastOp |= (fs = File.Create((_final = Path.Combine(_path + @"..\", _file)))) != null;
            fs.Dispose();
            return _lastOp;
        }
    }
}
