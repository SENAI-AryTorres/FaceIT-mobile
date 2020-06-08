using FaceIT.Service;
using faceitapi.Models;
using Syncfusion.ListView.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FaceIT.ViewModels
{
    public class CadastroSkillViewModel
    {
        private ObservableCollection<Skill> skills;
        public ObservableCollection<Skill> Skill
        {
            get { return Skill; }
            set
            {
                skills = value;
            }
        }

        public CadastroSkillViewModel()
        {
            _ = SkillService.GetSkillAsync(lista =>
              {
                  foreach (Skill item in lista) Skill.Add(item);
              });
        }
    }
}
