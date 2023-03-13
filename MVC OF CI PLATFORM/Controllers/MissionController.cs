using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using CI_PLATFORM_.repository.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Cryptography;

namespace MVC_OF_CI_PLATFORM.Controllers
{
    
    public class MissionController : Controller
    {
       
        private readonly ISubheaderInterface _subheaderInterface;
        private readonly IMissionInterface _missionRepository;

        public IActionResult volunteerpage(int Id)
        {
            /*var mission = _missionRepository.GetMissionId(Id);*/
            var mission = new Tuple<VolunteerMissionViewmodel, relatedmissionviewmodel>(_missionRepository.GetMissionId(Id),_missionRepository.GetRelatedMission(Id));    
            return View(mission);
            
        }
        public MissionController(ISubheaderInterface subheaderInterface,IMissionInterface missionRepository )
        {
            _subheaderInterface = subheaderInterface;
            _missionRepository = missionRepository;
        }

       public JsonResult Country()
        {
            var country = _subheaderInterface.GetCountries();
            return Json(country);
        }
        public JsonResult Theme()
        {
            var theme = _subheaderInterface.GetMissionThemeList();
            return Json(theme);
        }
        public JsonResult Skill()
        {
            var skills = _subheaderInterface.GetSkillsList();
            return Json(skills);
        }
        public JsonResult City(List<int> id)

        {
            var city = _subheaderInterface.GetCities(id);
            return Json(city);    
        }
       
        /*public IActionResult platformLanding(int pageIndex = 1, int pageSize = 9, string? SearchInputdata = "")
        {
            var mission = _missionRepository.GetAll(pageIndex, pageSize, SearchInputdata);
            mission.currentPage = pageIndex;
            return View(mission);
        }
*/
        [AllowAnonymous]
        public IActionResult platformLanding(int pageIndex = 1, int pageSize = 9, string? SearchInputdata = "", int sortId= 0)
        {
            ViewData["country"]= _subheaderInterface.GetCountryList();

            ViewData["city"]=_subheaderInterface.GetCityList();
            ViewData["theme"]=_subheaderInterface.GetMissionThemeList();
            ViewData["skill"]=_subheaderInterface.GetSkillsList();
            ViewData["Mission"] = _missionRepository.GetMissionsList();
            ViewData["missionSkill"]=_subheaderInterface.GetMissionSkillsList();
            ViewData["GoalMission"]=_subheaderInterface.GetGoalMissionList();
            var firstname_session = HttpContext.Session.GetString("username");
            /*if (firstname_session == null)
            {
                return RedirectToAction("LOGIN", "Home");
            }*/

            var mission = _missionRepository.GetAll(pageIndex, pageSize, SearchInputdata, sortId);
            mission.currentPage = pageIndex;
            return View(mission);
            /*return View();*/
        }
    }
}
