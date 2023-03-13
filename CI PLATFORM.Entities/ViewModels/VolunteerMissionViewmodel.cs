using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_PLATFORM.Entities.ViewModels
{
    public class VolunteerMissionViewmodel
    {
        public long MissionId { get; set; }

        public string Theme { get; set; }

        public string City { get; set; }

        public List<string> SkillList { get; set; }
        public long CountryId { get; set; }

        public string Title { get; set; }

        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? MissionType { get; set; }

        public int? Status { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetail { get; set; }

        public int? Availability { get; set; }

        public string? goalObjective { get; set; }
        public int? goalvalue { get; set; }
    }
}
