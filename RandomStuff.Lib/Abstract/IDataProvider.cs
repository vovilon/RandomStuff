using RandomStuff.Lib.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomStuff.Lib.Abstract
{
    public interface IDataProvider
    {
        void Save(Healer healer);
        void Save(Victim victim);
        void Save(Execution execution);
        void DeleteHealer(int id);
        void DeleteVictim(int id);
        void DeleteExecution(int id);
        void ExecuteVictim(int id);

        IEnumerable<Healer> Healers { get; }
        IEnumerable<Victim> Victims { get; }
        IEnumerable<Execution> Executions { get; }
        IEnumerable<Speciality> Specialities { get; }

    }
}
