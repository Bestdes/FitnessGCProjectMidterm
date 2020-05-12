using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FitnessGCProjectMid
{
    class SingleClubMember : Member
    {
        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: SingleClubMember
        public Club ClubAssign { get; set; }

        //------------------------------------------------------------------------------------------------
        // The code below represents the Constructor(s) for the class: SingleClubMember
        public SingleClubMember(int id, string firstName, string lastName, Club clubAssign) : base(id, firstName, lastName)
        {
            this.ClubAssign = clubAssign;
        }

        public SingleClubMember(int id, string name, Club clubAssign) : base(id, name)
        {
            this.ClubAssign = clubAssign;
        }

        public SingleClubMember(int id, string name) : base(id, name)
        {

        }

        public SingleClubMember()
        {

        }

        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: SingleClubMember
        public override void CheckIn(Club club) //checks to see if SingleClubMember is assigned to club
        {
            //This is were we code goes that would print a text file that prints all members who are checked in to this club At the moment
            if (club == ClubAssign)
            {
                try
                {
                    /*club = ClubAssign;*/
                    StreamWriter writer = new StreamWriter("../../../CheckedInMembers.txt");
                    writer.WriteLine($"{club.Name}: {Name} + {ID}");
                    writer.Close();
                    Console.WriteLine("Success");

                }

                catch (FormatException)
                {
                    Console.WriteLine("That is not a valid club. ");
                }
            }
        }

        public override bool CheckIfIsAMemberOfActiveLocation()
        {
            throw new NotImplementedException();
        }
    }
}