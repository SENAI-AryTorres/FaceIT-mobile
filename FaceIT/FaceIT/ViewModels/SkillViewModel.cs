using faceitapi.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FaceIT.ViewModels
{
    public class SkillViewModel
    {
        //private Skill _oldProduct;
        public ObservableCollection<Skill> SkillsLP { get; set; }
        public SkillViewModel()
        {
            SkillsLP = new ObservableCollection<Skill>
            {
                new Skill {IDSkill = 1, IDTipoSkill = 1, Descricao = "C#"},
                new Skill {IDSkill = 2,IDTipoSkill = 2,Descricao = "ASP.NET"},
                new Skill {IDSkill = 3,IDTipoSkill = 6,Descricao = "SQL Server"},
                new Skill {IDSkill = 4,IDTipoSkill = 1,Descricao = "Java"},
                new Skill {IDSkill = 5,IDTipoSkill = 1,Descricao = "JavaScript"},
                new Skill {IDSkill = 6,IDTipoSkill = 1,Descricao = "Python"},
                new Skill {IDSkill = 7,IDTipoSkill = 1,Descricao = "PHP"},
                new Skill {IDSkill = 8,IDTipoSkill = 1,Descricao = "Ruby"},
                new Skill {IDSkill = 9, IDTipoSkill = 1, Descricao = "C++"},
                new Skill {IDSkill = 10,IDTipoSkill = 1,Descricao = "SQL" },
                new Skill {IDSkill = 12,IDTipoSkill = 2,Descricao = "Hibernate"},
                new Skill {IDSkill = 13,IDTipoSkill = 2,Descricao = "Spark"},
                new Skill {IDSkill = 14,IDTipoSkill = 2,Descricao = "Node"},
                new Skill {IDSkill = 15,IDTipoSkill = 2,Descricao = "Django"},
                new Skill {IDSkill = 16,IDTipoSkill = 2,Descricao = "JQuery"},
                new Skill {IDSkill = 17,IDTipoSkill = 2,Descricao = "Vue"},
                new Skill {IDSkill = 18,IDTipoSkill = 2,Descricao = "React"},
                new Skill {IDSkill = 19,IDTipoSkill = 2,Descricao = "Angular"},
                new Skill {IDSkill = 20,IDTipoSkill = 2,Descricao = "Laravel"},
                new Skill {IDSkill = 21,IDTipoSkill = 2,Descricao = "Spring"},
                new Skill {IDSkill = 22,IDTipoSkill = 2,Descricao = "Grails"},
                new Skill {IDSkill = 23,IDTipoSkill = 2,Descricao = "CodeIgniter"},
                new Skill {IDSkill = 24,IDTipoSkill = 2,Descricao = "Rails"},
                new Skill {IDSkill = 25,IDTipoSkill = 2,Descricao = "Flask"},
                new Skill {IDSkill = 26, IDTipoSkill = 2, Descricao = "CherryPy"},
                new Skill {IDSkill = 27,IDTipoSkill = 2,Descricao = "Cassandra"},
                new Skill {IDSkill = 28,IDTipoSkill = 5,Descricao = "Visual Studio"},
                new Skill {IDSkill = 29, IDTipoSkill = 5, Descricao = "NetBeans"},
                new Skill {IDSkill = 30,IDTipoSkill = 5,Descricao = "Eclipse"},
                new Skill {IDSkill = 31,IDTipoSkill = 5,Descricao = "Jetbrains"},
                new Skill {IDSkill = 32,IDTipoSkill = 5,Descricao = "SSMS"},
                new Skill {IDSkill = 33,IDTipoSkill = 5,Descricao = "MySQL Workbench"},
                new Skill {IDSkill = 34, IDTipoSkill = 6, Descricao = "SQL Server"},
                new Skill {IDSkill = 35,IDTipoSkill = 6,Descricao = "Oracle"},
                new Skill {IDSkill = 36,IDTipoSkill = 6,Descricao = "MySQL"},
                new Skill {IDSkill = 37,IDTipoSkill = 6,Descricao = "PostgreSQL"},
                new Skill {IDSkill = 38,IDTipoSkill = 6,Descricao = "MariaDB"},
                new Skill {IDSkill = 39,IDTipoSkill = 6,Descricao = "MongoDB"},
                new Skill {IDSkill = 40, IDTipoSkill = 6, Descricao = "SQLite"},
                new Skill {IDSkill = 41,IDTipoSkill = 3,Descricao = "WEB"},
                new Skill {IDSkill = 42,IDTipoSkill = 3,Descricao = "Mobile"},
                new Skill {IDSkill = 43,IDTipoSkill = 3,Descricao = "Desktop"},
                new Skill {IDSkill = 44,IDTipoSkill = 7,Descricao = "Inglês"},
                new Skill {IDSkill = 45,IDTipoSkill = 7,Descricao = "Português"},
                new Skill {IDSkill = 46,IDTipoSkill = 6,Descricao = "Espanhol"},
                new Skill {IDSkill = 47, IDTipoSkill = 7, Descricao = "Francês"},
                new Skill {IDSkill = 48,IDTipoSkill = 4,Descricao = "Git"},
                new Skill {IDSkill = 49,IDTipoSkill = 4,Descricao = "CVS"},
                new Skill {IDSkill = 50,IDTipoSkill = 4,Descricao = "Subversion"},
                new Skill {IDSkill = 51,IDTipoSkill = 4,Descricao = "TFS"},
                new Skill {IDSkill = 52,IDTipoSkill = 4,Descricao = "Mercurial"},
            };
        }
    }
}
