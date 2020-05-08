using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    //Jake
    class MultiClubMember : Member
    {
        public int MemPts {  get; set;  }
        public MultiClubMember(int id, string name, int memPts) : base(id, name)
        {
            this.MemPts = memPts;
        }

        public override void CheckIn(Club club) //checking in adds membership points for MultiClubMember
        {
            MemPts ++;
        }
    }
}
