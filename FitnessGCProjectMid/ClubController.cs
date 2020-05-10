using System;
using System.Collections.Generic;
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
    }
}
