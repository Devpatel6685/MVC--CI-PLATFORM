using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            
            
            MissionViewmodel listOfMission = new MissionViewmodel()
            {
                Missions = mission.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList(),
            
                totalrecords = mission.Count(),
            };
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
            return listOfMission;
        }

        
        public List<Mission> GetMissionsList()
        {
            
            return _cIPLATFORMDbContext.Missions.ToList();
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



