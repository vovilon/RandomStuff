using System;
using System.Diagnostics;
using RandomStuff.Lib.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RandomStuff.WebSite.Models;
using System.Linq;
using RandomStuff.Lib.Model;
using RandomStuff.WebSite.ViewModel;

namespace RandomStuff.WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IDataProvider dataProvider, ILogger<HomeController> logger)
        {
            _dataProvider = dataProvider;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Healers();
        }

        [HttpGet]
        public IActionResult Healers()
        {
            ViewBag.AllSpecialities = _dataProvider.Specialities.ToList();
            return View("Healers", new HealerListViewModel(_dataProvider.Healers));
        }

        [HttpPost]
        public IActionResult Checkin(Execution execution)
        {
            if (!ModelState.IsValid)
                return Checkin(execution.HealerId, execution.VictimId, execution.Id);

            _dataProvider.Save(execution);
            return RedirectToAction("Healers");
        }

        [HttpGet]
        public IActionResult Checkin(int healerId, int victimId, int executionId)
        {
            PrepareHealersScope(addAllinItem: false);
            PrepareVictimsScope(addAllinItem: false);

            if (executionId != 0)
            {
                ViewBag.ReturnUrl = "ExecutionList";
                return View(_dataProvider.Executions.First(e => e.Id == executionId));
            }
            
            if (victimId != 0)
            {
                ViewBag.ReturnUrl = "Victims";
                ViewBag.Id = victimId;
            } 
            else if (healerId != 0)
            {
                ViewBag.ReturnUrl = "Healers";
                ViewBag.Id = healerId;
            }

            var execution = new Execution
            {
                HealerId = healerId,
                VictimId = victimId,
                ExecutionTime = DateTime.Today
            };

            return View(execution);
        }

        [HttpGet]
        public IActionResult EditHealer(int id)
        {
            var list = _dataProvider.Specialities.ToList();
            var killer = _dataProvider.Healers.FirstOrDefault(h => h.Id == id)
                        ?? new Healer { FullName = "", Speciality = list.First() };
            ViewBag.AllSpecialities = list.Select(x => new { Id = x.Id, name=x.name });
            return View(killer);
        }

        [HttpPost]
        public IActionResult EditHealer(Healer healer)
        {
            if (!ModelState.IsValid)
                return EditHealer(healer.Id);

            _dataProvider.Save(healer);
            return RedirectToAction("Healers");
        }

        public IActionResult DeleteHealer(int id)
        {
            _dataProvider.DeleteHealer(id);

            return RedirectToAction("Healers");
        }

        public IActionResult AbortExecution(int id)
        {
            _dataProvider.DeleteExecution(id);
            return RedirectToAction("ExecutionList");
        }

        [HttpGet]
        public IActionResult Victims()
        {
            return View(_dataProvider.Victims);
        }

        [HttpGet]
        public IActionResult EditVictim(int id)
        {
            var deadbeaf = _dataProvider.Victims.FirstOrDefault(h => h.Id == id);

            return View(deadbeaf ?? new Victim { BirthDay = DateTime.Today });
        }

        [HttpPost]
        public IActionResult EditVictim(Victim victim)
        {
            if (!ModelState.IsValid)
                return EditVictim(victim);

            _dataProvider.Save(victim);
            return RedirectToAction("Victims");
        }

        public IActionResult DeleteVictim(int id)
        {
            _dataProvider.ExecuteVictim(id);

            return RedirectToAction("Victims");
        }


        [HttpPost]
        public IActionResult ExecutionList(ExecutionListViewModel vm)
        {
            return ExecutionList(vm.VictimId, vm.HealerId, vm.ChoosenDate);
        }

        [HttpGet]
        public IActionResult ExecutionList(int victimId, int healerId, DateTime date)
        {
            var healers = _dataProvider.Healers.ToList();
            var victims = _dataProvider.Victims.ToList();

            var executions = _dataProvider
                            .Executions
                            .Where(e => victimId == 0 || e.VictimId == victimId)
                            .Where(e => healerId == 0 || e.HealerId == healerId)
                            .Where(e => date == DateTime.MinValue || e.ExecutionTime.Date == date);

            var vm = new ExecutionListViewModel(executions, healers, victims)
            {
                VictimId = victimId,
                HealerId = healerId,
                ChoosenDate = date == DateTime.MinValue ? DateTime.Today : date
            };

            PrepareHealersScope(addAllinItem: true);
            PrepareVictimsScope(addAllinItem: true);
            return View(vm);
        }

        private void PrepareVictimsScope(bool addAllinItem)
        {
            var victims = _dataProvider.Victims.Select(v => new { Id = v.Id, FullName = v.FullName }).ToList();
            if (addAllinItem)
                victims.Insert(0, new { Id = 0, FullName = "Все" });
            ViewBag.AllVictims = victims;
        }

        private void PrepareHealersScope(bool addAllinItem)
        {
            var healers = _dataProvider.Healers.Select(v => new { Id = v.Id, FullName = v.FullName }).ToList();
            if (addAllinItem)
                healers.Insert(0, new { Id = 0, FullName = "Все" });
            ViewBag.AllHealers = healers;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
