using RandomStuff.Lib.Abstract;
using RandomStuff.Lib.Model;
using System.Collections.Generic;
using System.Linq;

namespace RandomStuff.Lib.Services
{

    public class DataProvider : IDataProvider
    {
        private readonly MedicDbContext _dbCtx;

        public DataProvider(MedicDbContext dbCtx)
        {
            _dbCtx = dbCtx;
        }

        public IEnumerable<Healer> Healers => _dbCtx.Healers;

        public IEnumerable<Victim> Victims => _dbCtx.Victims;

        public IEnumerable<Execution> Executions => _dbCtx.Executions;

        public IEnumerable<Speciality> Specialities => _dbCtx.Specialities;

        public void Save(Healer healer)
        {
            var killer = _dbCtx.Healers.Find(healer.Id);
            if (killer == null)
            {
                _dbCtx.Healers.Add(healer);
            }
            else
            {
                killer.FullName = healer.FullName;
                killer.SpecialityId = healer.SpecialityId;
            }

            _dbCtx.SaveChanges();
        }

        public void Save(Victim victim)
        {
            var deadbeaf = _dbCtx.Victims.Find(victim.Id);
            if (deadbeaf == null)
            {
                _dbCtx.Victims.Add(victim);
            }
            else
            {
                deadbeaf.FullName = victim.FullName;
                deadbeaf.BirthDay = victim.BirthDay;
            }

            _dbCtx.SaveChanges();
        }

        public void Save(Execution execution)
        {
            var finalize = _dbCtx.Executions.Find(execution.Id);
            if (finalize == null)
            {
                _dbCtx.Executions.Add(execution);
            }
            else
            {
                finalize.ExecutionTime = finalize.ExecutionTime;
            }

            _dbCtx.SaveChanges();
        }

        public void DeleteHealer(int id)
        {
            var killer = _dbCtx.Healers.Find(id);
            if (killer != null)            
            {

                //var drops = _dbCtx.Executions.Where(x => x.HealerId == id);
                //_dbCtx.Executions.RemoveRange(drops);
                _dbCtx.Healers.Remove(killer);
                _dbCtx.SaveChanges();
            }
        }

        public void DeleteVictim(int id)
        {
            var luckyDude = _dbCtx.Victims.Find(id);
            if (luckyDude != null)
            {
                //var drops = _dbCtx.Executions.Where(x => x.VictimId == id);
                //_dbCtx.Executions.RemoveRange(drops);
                _dbCtx.Victims.Remove(luckyDude);
                _dbCtx.SaveChanges();
            }
        }

        public void DeleteExecution(int id)
        {
            var action = _dbCtx.Executions.Find(id);
            if (action != null)
            {
                _dbCtx.Executions.Remove(action);             
                _dbCtx.SaveChanges();
            }
        }

        public void ExecuteVictim(int id)
        {
            var victim = _dbCtx.Victims.Find(id);
            if (victim != null)
            {
                //var drops = _dbCtx.Executions.Where(x => x.VictimId == id);
                //_dbCtx.Executions.RemoveRange(drops);
                _dbCtx.Victims.Remove(victim);
                _dbCtx.SaveChanges();
            }
        }
    }
}
