using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Repository
{
    public class MissionRepository : IMissionInterface
    {
        private readonly CIPLATFORMDbContext _cIPLATFORMDbContext;


        public MissionRepository(CIPLATFORMDbContext cIPLATFORMDbContext)
        {
            _cIPLATFORMDbContext = cIPLATFORMDbContext;
        }

        public MissionViewmodel GetAll(int pageIndex, int pageSize, string keyword, int sortId) { 
           
            var mission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Where(model => keyword == null || model.Title.Contains(keyword) || model.Theme.Title.Contains(keyword) || model.City.Name.Contains(keyword)).AsQueryable();
             // METHOD 2 
            /* var missions = from m in _cIPLATFORMDbContext.Missions
                           join c in _cIPLATFORMDbContext.Cities on m.CityId equals c.CityId
                           join t in _cIPLATFORMDbContext.MissionThemes on m.ThemeId equals t.MissionThemeId
                           where keyword == null
                              || m.Title.Contains(keyword)
                              || t.Title.Contains(keyword)
                              || c.Name.Contains(keyword)
                           select m;*/


            MissionViewmodel listOfMission = new MissionViewmodel()
            {   // PAGINATION
               /* Missions = mission.ToList((pageIndex - 1) * pageSize).Take(pageSize).ToList(),*/
                // COUNT
                totalrecords = mission.Count()
            };
            //SORT BY
            if (sortId == 1)
            {
                listOfMission.Missions = listOfMission.Missions.OrderByDescending(p => p.CreatedAt).ToList();
            }
            else if (sortId == 2)
            {
                listOfMission.Missions = listOfMission.Missions.OrderBy(p => p.CreatedAt).ToList();
            }
            else if (sortId == 3)
            {
                listOfMission.Missions = listOfMission.Missions.OrderBy(p => p.Availability).ToList();
            }
            else if (sortId == 4)
            {
                listOfMission.Missions = listOfMission.Missions.OrderBy(p => p.Availability).ToList();
            }
            else if (sortId == 6)
            {
                listOfMission.Missions = listOfMission.Missions.OrderBy(p => p.EndDate).ToList();
            }
            listOfMission.Missions = mission.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return listOfMission;
        }

        public VolunteerMissionViewmodel GetMissionId(int Id)
        {
            List<string> skills = _cIPLATFORMDbContext.MissionSkills
                        .Where(m => m.MissionId ==  Id)
                        .Include(m => m.Mission)
                        .Include(m => m.Skill)
                        .Select(m => m.Skill.SkillName)
                        .ToList();

            Mission mission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(x => x.Theme).SingleOrDefault(m => m.MissionId == Id);
            VolunteerMissionViewmodel volunteerMission = new VolunteerMissionViewmodel()
            {
                MissionId = mission.MissionId,
                Theme = mission.Theme.Title,
                City = mission.City.Name,
                CountryId = mission.CountryId,
                Title = mission.Title,
                ShortDescription = mission.ShortDescription,
                Description = mission.Description,
                StartDate = mission.StartDate,
                EndDate = mission.EndDate,
                MissionType = mission.MissionType,
                Status = mission.Status,
                OrganizationName = mission.OrganizationName,
                OrganizationDetail = mission.OrganizationDetail,
                Availability = mission.Availability,
                SkillList = skills,
                goalvalue = 0,
                goalObjective = ""
            };
            if (mission.MissionType == "GOAL")
            {
                var goal = _cIPLATFORMDbContext.GoalMissions.SingleOrDefault(m => m.MissionId == Id);
                volunteerMission.goalObjective = goal.GoalObjectiveText;
                volunteerMission.goalvalue = goal.GoalValue;
            }
            return volunteerMission;
        }

        public List<Mission> GetMissionsList()
        {
            
            return _cIPLATFORMDbContext.Missions.ToList();
        }

        public relatedmissionviewmodel GetRelatedMission(int Id)
        {
            var missions = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.Country).Where(ms => ms.MissionId == Id).FirstOrDefault();
            var RelatedMission = _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme).Include(m => m.Country).Where(ms =>ms.MissionId != missions.MissionId && (ms.City.Name == missions.City.Name || ms.Theme.Title == missions.Theme.Title || ms.Country.Name == missions.Country.Name)).ToList();
            var goalmission = _cIPLATFORMDbContext.GoalMissions.ToList();
            if (RelatedMission.Any(rml => rml.City.Name == missions.City.Name))
            {
                RelatedMission = RelatedMission.Where(rml => rml.City.Name == missions.City.Name).ToList();
            }
            if (RelatedMission.Any(rml => rml.Country.Name == missions.Country.Name))
            {
                RelatedMission = RelatedMission.Where(rml => rml.Country.Name == missions.Country.Name).ToList();
            }
            if (RelatedMission.Any(rml => rml.Theme.Title == missions.Theme.Title))
            {
                RelatedMission = RelatedMission.Where(rml => rml.Theme.Title == missions.Theme.Title).ToList();
            }

            var resultRelatedMission = new relatedmissionviewmodel
            {
                missionList = RelatedMission,
                goalMissionList = goalmission



            };
            return resultRelatedMission;

        }
        /*public List<cardsViewModel> GetMissions()
{
List<cardsViewModel> listOfMission = new List<cardsViewModel>();
foreach (var mission in _cIPLATFORMDbContext.Missions.Include(m => m.City).Include(m => m.Theme))
{
cardsViewModel missionViewModel = new cardsViewModel()
{
Theme = mission.Theme.Title,
City = mission.City.Name,
Country = mission.Country.Name,
Title = mission.Title,
ShortDescription = mission.ShortDescription,
Description = mission.Description,
StartDate = mission.StartDate,
EndDate = mission.EndDate,
MissionType = mission.MissionType,
Status = mission.Status,
OrganizationName = mission.OrganizationName,
OrganizationDetail = mission.OrganizationDetail,
Availability = mission.Availability,
};
listOfMission.Add(missionViewModel);
}
return listOfMission;
}*/
    }
 }



