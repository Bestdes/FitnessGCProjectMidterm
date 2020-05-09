using System;
using System.Collections.Generic;

namespace FitnessGCProjectMid
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Fitness Center Manager");
            Console.WriteLine("Please select a management option below:");
            Console.WriteLine($"1. Check a Member into a club");
            Console.WriteLine($"2. Add member");
            Console.WriteLine($"3. Remove member");
            Console.WriteLine($"4. Display member information");

            Club club = new Club();
            List<Club> clubList = new List<Club>();
            SingleClubMember scMember = new SingleClubMember();
            MultiClubMember mcMember = new MultiClubMember();

            string input = Console.ReadLine().Trim();
            int num = 0;
            bool isANum = int.TryParse(input, out num);

            switch (num)
            {
                case 1:
                    //Option to check a member into a club
                    club.DisplayClubs(clubList);
                    Console.WriteLine("Enter the club you wish to check the member into:");
                    string ciClub = Console.ReadLine().Trim().ToLower();
                    
                    Console.WriteLine("Enter the ID of the person you wish to check in");
                    string numID = Console.ReadLine().Trim();
                    Member.FindMember();
                    break;

                case 2:
                    //Option to add a member to a club
                    club.DisplayClubs(clubList);
                    Console.WriteLine("Enter the club you wish to add a member to:");
                    string clubChocie = Console.ReadLine().Trim().ToLower();
                    break;

                case 3:
                    //option to remove a member from a club
                    Console.WriteLine("Enter the name of the member you wish to remove:");
                    Member removeMem = Member.FindMember();

                    Console.WriteLine("Enter the club you wish to remove this member from:");
                    string removeClub = Club.ReadAndReturnInput();
                    club.RemoveMemberFromClub(removeMem, removeClub);
                    break;

                case 4:
                    //option to display member information
                    Console.WriteLine("Enter the name of the member you wish to display information for:");
                    string memberDisplay = Console.ReadLine().Trim();
                    Member.FindMember();
                    break;

                case 5:
                    //option to exit the program
                    Console.WriteLine("Logging you out and closing the program...");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
