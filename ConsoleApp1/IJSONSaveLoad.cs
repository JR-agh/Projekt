using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal interface IJSONSaveLoad<T> {
        void SaveToJSON();
        static abstract T LoadFromJSON(string fileName);
    }
}
