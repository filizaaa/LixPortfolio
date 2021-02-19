using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LixPortfolio.Models
{
    public class ViewModel { 


    public IEnumerable<Personal> Personal { get; set; }
    public IEnumerable<Contact> Contacts { get; set; }
    public IEnumerable<Experience> Experiences { get; set; }
    public IEnumerable<Skill> Skills { get; set; }
    public IEnumerable<AcademicLife> AcademicLife { get; set; }

    public IEnumerable<SkillType> SkillTypes { get; set; }
    public IEnumerable<WorkType> ExperienceTypes { get; set; }

    public IEnumerable<Hobby> Hobbies { get; set; }

}
}