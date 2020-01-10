using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroBotFramework
{
    public class UserProfile
    {
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public bool PromptedUserForName { get; set; } = false;

    }
}
