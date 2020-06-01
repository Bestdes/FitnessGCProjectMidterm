using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FitnessGCProjectMid
{
    class ClubController
    {
        public Club ActiveClub { get; set; }

        public string EmployeeName { get; set; }

        public ClubController(Club activeClub)
        {
            ActiveClub = activeClub;
        }

        public ClubController()
        {

        }

        public void ChangeActiveCub()
        {

        }

        public void ClearCheckInMembers()
        {
            StreamWriter writer = new StreamWriter("../../../CheckedInMembers.txt");
            writer.WriteLine($"");
            writer.Close();
        }
    }
}