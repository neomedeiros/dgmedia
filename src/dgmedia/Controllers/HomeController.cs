using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using dgmedia.Entities;
using MongoDB.Bson;
using dgmedia.Models;
using dgmedia.DataAccess.Action;
using dgmedia.Util.Helpers;
using dgmedia.Entities.Domain;

namespace dgmedia.Controllers
{
    public class HomeController : Controller
    {

        private IActionRepository _actionRepository { get; set; }

        public HomeController(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetActionTypes()
        {
            var actionTypes = _actionRepository.GetActionTypes().Select(m => new SelectItemModel() { Value = (int)m, Text = EnumHelper.GetDescription(m) });
            return new JsonResult(actionTypes);
        }

        [HttpGet]
        public JsonResult GetEarnTypes()
        {
            var actionTypes = _actionRepository.GetEarnTypes();
            return new JsonResult(actionTypes);
        }

        [HttpGet]
        public JsonResult GetIPS()
        {
            var actionTypes = _actionRepository.GetIPS();
            return new JsonResult(actionTypes);
        }

        [HttpGet]
        public JsonResult GetUserIDs()
        {
            var actionTypes = _actionRepository.GetUserIDs();
            return new JsonResult(actionTypes);
        }

        [HttpPost]
        public JsonResult GenerateChart([FromBody]ActionsChartConfiguration actionsChartConfiguration)
        {
            var result = _actionRepository.GetActionChart(actionsChartConfiguration);
            return new JsonResult(result.ToJson());            
        }
    }
}
