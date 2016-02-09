using RedX.Service.IO.IWriter;
using RedX.Service.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.IO.Generics{
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


        public bool CheckDirectory(){
            return Directory.Exists(_path);
        }

        public bool CreateDirectory(){
            if (!CheckDirectory())
                return (_lastOp |= (Directory.CreateDirectory(_path) != null));
            return false;
        }

        public virtual bool CreateFile(){
            _lastOp = CreateDirectory();
            FileStream fs;
            _lastOp |= (fs = File.Create((_final = Path.Combine(_path + @"..\", _file)))) != null;
            fs.Dispose();
            return _lastOp;
        }
    }
}
