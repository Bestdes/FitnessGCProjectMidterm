using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FitnessGCProjectMid
{
    class ListOfClubs
    {
        private static ListOfClubs instance = null;
        private static readonly object padlock = new object();

        public List<Club> ClubList { get; set; } = new List<Club>();

        public ListOfClubs()
        {
           
        }

        public static ListOfClubs Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new ListOfClubs();
                    }
                    return instance;
                }
            }
        }

        public void AddClubToList(Club club)
        {
            ClubList.Add(club);
        }

        public void MemberCheckIn(Member member)
        {
            Console.WriteLine("What is your member ID?");
            int memberID = int.Parse(Console.ReadLine().Trim());
            try
            {
                member.CheckIn(FindClubOfMember(memberID));
            }
            catch (FormatException notANum)
            {
                Console.WriteLine("Some type of Exception is thrown");
            }
            catch (OverflowException numTooBig)
            {
                Console.WriteLine("Some type of Exception is thrown");
            }
        }

        public Club FindClubOfMember(int memberID)
        {
            Club workingClub = new Club();
            foreach (Club club in ClubList)
            {
                foreach(Member member in club.ListOfMembers)
                {
                    if(memberID == member.Id)
                    {
                        workingClub = club;
                        return club;
                    }
                }
            }
            return workingClub;
        }

        public void PrintAllClubsAndMembers()
        {
            StreamWriter writer = new StreamWriter("../../../Members.txt");
            foreach (Club club in ClubList)
            {
                writer.WriteLine($"Club Name: {club.Name}\n\nMembers Include:");

                foreach (Member member in club.ListOfMembers)
                {
                    writer.WriteLine($"\n\t{member.Name}");
                }
            }
            writer.Close();
        }

    }
}
