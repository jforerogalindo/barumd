using System;
using System.Collections.Generic;

namespace ApiRest.Model.Bender
{
    public partial class User
    {
        public int Iduser { get; set; }
        public string? Names { get; set; }
        public int BranchIdbranch { get; set; }
        public string? Password { get; set; }
        public int RolIdrol { get; set; }
    }
}
