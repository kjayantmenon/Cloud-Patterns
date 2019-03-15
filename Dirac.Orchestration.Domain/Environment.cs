using System;
using System.Collections.Generic;
using System.Text;

namespace Dirac.Orchestration.Domain
{
    public class Environment:ValueObject
    {

        public OS OS{ get; set; }


        public RAM RAM { get; set; }
        //"ram": "",
        //"gpu": "",
        //"nodes": "",
        //"softwareDependancy": ["matlab runtime","python lib"]
    }
}

