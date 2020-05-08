using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    class SingleClubMember : Member
    {
        public Club ClubAssign { get; set; }
        public SingleClubMember(int id, string name, Club clubAssign) : base(id, name)
        {
            this.ClubAssign = clubAssign;
        }

        public override void CheckIn(Club club) //checks to see if SingleClubMember is assigned to club
        {
            if (club == ClubAssign)
            {
                try
                    {
                    club = ClubAssign;
                    }

                    catch (FormatException)
                    {
                    Console.WriteLine("That is not a valid club. ");
                    }
            }
        }
    }
}
