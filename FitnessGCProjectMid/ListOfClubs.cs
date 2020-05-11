using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FitnessGCProjectMid
{
    class ListOfClubs
    {

        //------------------------------------------------------------------------------------------------
        // The code below represents the field(s) for the class: ListOfClubs
        private static ListOfClubs instance = null;
        private static readonly object padlock = new object();

        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: ListOfClubs
        public List<Club> ClubList { get; set; } = new List<Club>();

        //------------------------------------------------------------------------------------------------
        // The code below represents the Constructors(s) for the class: ListOfClubs


        //Using the singleton pattern(Thread Safe) in order to ensure this is only one list of clubs as this
        //fitness organization will only have one list of clubs that it manages

        //The singleton pattern method checks to see if there is a instance of ListOfClub if there isn't it
        // creates one and it there is it uses the one that is already created.

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

        //The Constructors has to be private in order to disallow anyone from create multiple ListOfClubs Objects
        private ListOfClubs()
        {

        }
        //---------------------------------------------------------------------------------------------------------
        // The code below represents the Method(s) for the class: ListOfClubs


        // This is the primary method that simply adds a Club to the ClubList of Clubs that the ListOfClubs contains
        public void AddClubToList(Club club)
        {
            ClubList.Add(club);
        }

        // This method checks in a member to ideally the current active club
        public void MemberCheckIn(Member member)
        {
            Console.WriteLine("What is your member ID?");
            int memberID = int.Parse(Console.ReadLine().Trim());
            try
            {
                member.CheckIn(GlobalFindClubOfMember(member.ID));
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

        public Club GlobalFindClubByName(string clubName) //Will return Club
        {
            Club foundClub = new Club();
            foreach (Club club in ClubList)
            {
                if(clubName == club.Name)
                {
                    foundClub = club;
                    return foundClub;
                }
                else
                {
                    Console.WriteLine("The club you searched for does not exist");
                }
            }
            return foundClub;
        }

        public bool GlobalFindIfClubExists(string clubName)
        {
            clubName = clubName.Trim();
            bool clubDoesExists = false;

            foreach (Club club in ClubList)
            {
                if(club.Name == clubName)
                {
                    clubDoesExists = true;
                    return clubDoesExists;
                }
                else
                {
                    clubDoesExists = false; 
                    return clubDoesExists;
                }
            }
            return clubDoesExists;
        }

        public void GlobalFindMember() //WIll return Member
        {
            // Can implement by name and by ID
        }

        public Club GlobalFindClubOfMember(int memberID)
        {
            Club workingClub = new Club();
            foreach (Club club in ClubList)
            {
                foreach(Member member in club.ListOfMembers)
                {
                    if(memberID == member.ID)
                    {
                        workingClub = club;
                        return workingClub;
                    }
                }
            }
            return workingClub;
        }


        // This method takes in a list composed of Club objects and displays their name, address, and numbers them
        public void DisplayClubs(List<Club> clubList)
        {
            for (int i = 0; i < clubList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clubList[i].Name}, {clubList[i].Address}");
            }
        }


        // This method prints all of the member and clubs that the organization current has at the time of call
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

        public void PrintAllClubs()
        {
            StreamWriter writer = new StreamWriter("../../../Clubs.txt");

            writer.WriteLine($"Clubs Include:");
            foreach (Club club in ClubList)
            {
                writer.WriteLine($"\n\n\t{club.Name}");
            }
            writer.Close();
        }

    }
}
