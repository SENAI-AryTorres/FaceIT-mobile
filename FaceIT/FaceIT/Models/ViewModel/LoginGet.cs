using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace faceitapi.Models.ViewModel
{
    public class LoginGet
    {
        public string Email { get; set; }
        public string Senha { get; set; }
        public int? GoogleId { get; set; }
    }
}
