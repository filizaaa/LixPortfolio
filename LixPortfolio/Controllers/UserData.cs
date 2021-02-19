using LixPortfolio.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace LixPortfolio.Controllers
{
    public class UserData
    {
        public StarLabs_PortfolioEntities db = new StarLabs_PortfolioEntities();
        public List<AcademicLife> al = new List<AcademicLife>();
        public List<Skill> s = new List<Skill>();
        public List<Experience> e = new List<Experience>();
        public List<Contact> c = new List<Contact>();
        public List<Personal> p = new List<Personal>();
        public List<SkillType> st = new List<SkillType>();
        public List<WorkType> et = new List<WorkType>();
        public List<Hobby> h = new List<Hobby>();
        public ViewModel vmodel = new ViewModel();
        public static int PortfolioID = 0;
        public static string LoggedEmail="";
        public static string SearchEmail = "";

        public void RemoveData() // nese bohet log out ndrohet vlera e logged user edhe bohen clear krejt t dhanat
        {
            al.Clear();
            s.Clear();
            e.Clear();
            c.Clear();
            p.Clear();
            h.Clear();
            vmodel.AcademicLife = al;
            vmodel.Contacts = c;
            vmodel.Experiences = e;
            vmodel.Skills = s;
            vmodel.Personal = p;
            vmodel.Hobbies = h;
        }
    
        ViewModel FilterDataPerUser(string email)
        {

            ViewModel PersonalViewModels = new ViewModel();
            Personal personal = null;

            foreach (var item in db.Personals)
            {
               if (item.Email == email) // filtrohen te dhenat ne baze te personit qe osht logged , kam mendu mi bo qe ni person me mujt me bo disa resume por po e lo vetem 1 ese mrri koha..
               {
                    p.Add(item);
                    personal = item;
                   
                }


            }
                
            if (personal != null)
            {
                PortfolioID = personal.ID;

                foreach (var item in db.AcademicLives)
                {
                    if (item.Personal == personal)
                    {
                        al.Add(item);
                    }
                }

                foreach (var item in db.Skills)
                {
                    if (item.Personal == personal)
                    {
                        s.Add(item);
                    }
                }

                foreach (var item in db.Experiences)
                {
                    if (item.Personal == personal)
                    {
                        e.Add(item);
                    }

                }

                foreach (var item in db.Contacts)
                {
                    if (item.Personal == personal)
                    {
                        c.Add(item);
                    }

                }
                foreach (var item in db.SkillTypes)
                {
                    st.Add(item);

                }
                foreach (var item in db.WorkTypes)
                {
                    et.Add(item);

                }

                foreach (var item in db.Hobbies)
                {
                    if (item.Personal == personal)
                    {
                        h.Add(item);
                    }

                }

                /// e bojim qe me username te loguar te gjindet personal info pastaj prej personal info te gjinden te dhenat tjera


                vmodel.Personal = p; // te ndrohet kur te krijohet login
                vmodel.Experiences = e;
                vmodel.Skills = s;
                vmodel.AcademicLife = al;
                vmodel.Contacts = c;
                vmodel.SkillTypes = st;
                vmodel.ExperienceTypes = et;
                vmodel.Hobbies = h;


            }
            return vmodel;
        }


        public UserData(string email)
        {
            
            FilterDataPerUser(email);

        }



    }
}