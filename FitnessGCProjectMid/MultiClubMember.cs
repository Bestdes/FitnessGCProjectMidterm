using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    //Jake
    class MultiClubMember : Member
    {
        public int MemPts {  get; set;  }

        public MultiClubMember(int id, string name, int memPts) : base(id, name) { }
        public MultiClubMember(int id, string firstName, string lastName, int memPts) : base(id, firstName, lastName)
        {
            this.MemPts = memPts;
        }
        public MultiClubMember() { }

        public override void CheckIn(Club club) //checking in adds membership points for MultiClubMember
        {
            MemPts ++;
        }
    }
}
