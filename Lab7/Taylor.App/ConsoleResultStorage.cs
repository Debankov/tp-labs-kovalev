using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Text;
using Taylor.Core;

namespace Taylor.App
{
    public class ConsoleResultStorage : IResultStorage
    {
        public void Save(string result)
        {
            var table = new ConsoleTable("Результат");

            table.AddRow(result);

            table.Write();
        }
    }
}
