using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    class ListOfClubs
    {
        public List<Club> ClubList { get; set; } = new List<Club>();

        public ListOfClubs()
        {
            
        }


        public void AddClubToList(Club club)
        {
            ListOfClubs.Add(club);
        }


    }
}
