using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    //Jake
    class MultiClubMember : Member
    {
        public int MemPts {  get; set;  }
<<<<<<< HEAD

        public MultiClubMember(int id, string name, int memPts) : base(id, name)
=======
        public MultiClubMember(int id, string firstName, string lastName, int memPts) : base(id, firstName, lastName)
>>>>>>> 767cf975712532753e852120625404f2ed84b540
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
