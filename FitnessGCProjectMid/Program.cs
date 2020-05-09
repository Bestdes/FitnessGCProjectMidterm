using System;

namespace FitnessGCProjectMid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome. Please select an option:");
            Console.WriteLine($"1. Check in to a club");
            Console.WriteLine($"2. Add member");
            Console.WriteLine($"3. Remove member");
            Console.WriteLine($"4. Display member information");

            Club club = new Club();
            SingleClubMember scMember = new SingleClubMember();
            MultiClubMember mcMember = new MultiClubMember();

            string input = Console.ReadLine().Trim();
            int num = 0;
            bool isANum = int.TryParse(input, out num);

            switch (num)
            {
                case 1:
                    Console.WriteLine("Enter the ID of the person you wish to check in");
                    string numID = Console.ReadLine().Trim();
                    int idNum = int.Parse(numID);
                    if (idN == scMember.Id)
                    {
                        scMember.CheckIn(club);
                    }
                    else if (num == mcMember.Id)
                    {
                        mcMember.CheckIn(club);
                    }
                    break;
                case 2:
                    //scMember.CheckIn(member);
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }

            ListOfClubs listOfClubs = new ListOfClubs();

            Club testingClub = new Club("Jakes Club", "First Address");
            Member testingMember = new SingleClubMember(123, "Jon Doe", testingClub);

            testingClub.AddMemberToClub(testingMember);

            listOfClubs.AddClubToList(testingClub);

            listOfClubs.PrintAllClubsAndMembers();

            //--------------------------------------------------

        }
    }
}
