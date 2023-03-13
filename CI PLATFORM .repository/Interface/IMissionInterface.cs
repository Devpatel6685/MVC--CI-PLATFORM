using CI_PLATFORM.Entities.DataModels;
using CI_PLATFORM.Entities.ViewModels;
using CI_PLATFORM_.repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM_.repository.Interface
{
    
    public interface IMissionInterface
    {
        public List<Mission> GetMissionsList();
        public MissionViewmodel GetAll(int pageIndex, int pageSize, string keyword, int sortId);

        public VolunteerMissionViewmodel GetMissionId(int Id);

        public relatedmissionviewmodel GetRelatedMission(int Id);

    }
}
