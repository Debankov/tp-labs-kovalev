using System;
using System.Collections.Generic;
using System.Text;

namespace Taylor.Core
{
    public interface IResultStorage
    {
        void Save(string result);
    }
}
