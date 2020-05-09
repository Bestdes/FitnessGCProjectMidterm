using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    class SingleClubMember : Member
    {
        public Club ClubAssign { get; set; }
        public SingleClubMember(int id, string firstName, string lastName, Club clubAssign) : base(id, firstName, lastName)
        {
            this.ClubAssign = clubAssign;
        }
        public SingleClubMember() { }
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
