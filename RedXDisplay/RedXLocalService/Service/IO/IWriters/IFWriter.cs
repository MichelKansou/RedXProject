using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedX.Service.IO.IWriter{
    public interface IFWriter{
        bool WriteIntoFile(String content);
    }
}
