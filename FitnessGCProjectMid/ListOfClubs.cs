using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

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
                if (clubName == club.Name)
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
        public void GlobalFindMemberFromClub()
        {
            Club foundClub;
            bool runMethodPart1 = true;
            DisplayAllClubs();

            while (runMethodPart1)
            {
                Console.WriteLine("\nEnter the name of the club you want to search");
                string input = ReadAndReturnInput();

                foreach (Club club in ListOfClubs.Instance.ClubList)
                {
                    if (input == club.Name || input.ToLower() == club.Name.ToLower())
                    {
                        foundClub = club;

                        DisplayAllMembersInSpecificClub(foundClub);

                        Console.WriteLine("\nPlease enter the name of the member you want details about");
                        string input2 = ReadAndReturnInput();
                        int turnedToInt;
                        bool testingNum = int.TryParse(input2, out turnedToInt);

                        foreach (Member member in foundClub.ListOfMembers)
                        {
                            if (member.Name == input2)
                            {
                                member.DisplayDetails();
                                runMethodPart1 = false;
                            }
                            else if (testingNum)
                            {
                                if (member.ID == turnedToInt)
                                {
                                    member.DisplayDetails();
                                    runMethodPart1 = false;
                                }
                            }
                            else
                            {
                                Console.WriteLine($"No match in the {club.Name}");
                                runMethodPart1 = false;
                            }
                        }
                    }
                    else if (input == club.Name || input.ToLower() != club.Name.ToLower())
                    {
                        Console.WriteLine($"No match in the list of clubs.");
                    }
                    runMethodPart1 = false;
                }
            }
        }

        public bool GlobalFindIfClubExists(string clubName)
        {
            clubName = clubName.Trim();
            bool clubDoesExists = false;

            foreach (Club club in ClubList)
            {
                if (club.Name == clubName)
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
            List<Member> foundResults = new List<Member>();
            string searchedForMember = "";
            int resultsfoundCount = 0;
            bool isSelectingResult = true;
            bool run1stBehaviour = true;
            string input1;

            while (run1stBehaviour)
            {
                Console.WriteLine("Would you like to search for a member by name or ID?\n" +
                    "\n\tPress 1: To Search For A Member By Name" +
                    "\n\tPress 2: To Searh For A Member By ID" +
                    "\n\tPress 3: To Return To The Previous Menu");


                input1 = ReadAndReturnInput();
                int typeOfSearchSelection = 0;
                bool isAnInt = int.TryParse(input1, out typeOfSearchSelection);

                if (typeOfSearchSelection == 1)
                {
                    Console.WriteLine("Enter the Name of the member to initiate a search.");
                    searchedForMember = ReadAndReturnInput();

                    foreach (Club club in ClubList)
                    {
                        if (club.ListOfMembers.Count > 0)
                        {
                            foreach (Member member in club.ListOfMembers)
                            {
                                if (member.Name.ToLower().Trim() == searchedForMember)
                                {
                                    foundResults.Add(member);
                                    run1stBehaviour = false;
                                }
                                else if (member.Name == searchedForMember)
                                {
                                    foundResults.Add(member);
                                    run1stBehaviour = false;
                                }
                                else
                                {
                                    Console.WriteLine("\nThe name you entered did not return a Member.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Club: {club.Name} returned no results");
                        }
                    }
                }
                else if (typeOfSearchSelection == 2)
                {
                    Console.WriteLine("Enter the ID of the member to initiate a search.");
                    searchedForMember = ReadAndReturnInput();
                    int parsedNum = 0;
                    bool isANum = int.TryParse(searchedForMember, out parsedNum);

                    foreach (Club club in ClubList)
                    {
                        if (club.ListOfMembers.Count > 0)
                        {
                            foreach (Member member in club.ListOfMembers)
                            {
                                if (member.ID == parsedNum)
                                {
                                    foundResults.Add(member);
                                    run1stBehaviour = false;
                                }
                                else
                                {
                                    Console.WriteLine("\nThe number you entered did not return a Member.");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"\nClub: {club.Name} returned no results\n");
                        }
                    }
                }
                else if (typeOfSearchSelection == 3)
                {
                    run1stBehaviour = false;
                    isSelectingResult = false;
                }
                else
                {
                    Console.WriteLine("\nThe input you entered is not a valid menu selection please try again!");
                }
            }

            if (foundResults.Count > 0)
            {
                foreach (Member member in foundResults)
                {
                    Console.Clear();
                    resultsfoundCount++;
                    Console.WriteLine($"{resultsfoundCount}:{member.Name} : {member.ID} : {GlobalFindClubOfMember(member.ID).Name}");
                }

                while (isSelectingResult)
                {

                    Console.WriteLine("\n\tPlease select a result: ");

                    string input2 = ReadAndReturnInput();
                    int selectionnNum = 0;
                    bool isANum = int.TryParse(input2, out selectionnNum);
                    int index = 0;

                    if (selectionnNum <= foundResults.Count)
                    {
                        foreach (Member member in foundResults)
                        {
                            index++;
                            if (selectionnNum == index)
                            {
                                isSelectingResult = false;
                                member.DisplayDetails();
                                Console.ReadLine();
                            }
                        }
                    }

                }
            }
            else if (run1stBehaviour && foundResults.Count == 0)
            {
                Console.WriteLine("\nNo results found");
            }
        }

        public Club GlobalFindClubOfMember(int memberID)
        {
            Club workingClub = new Club();
            foreach (Club club in ClubList)
            {
                foreach (Member member in club.ListOfMembers)
                {
                    if (memberID == member.ID)
                    {
                        workingClub = club;
                        return workingClub;
                    }
                }
            }
            return workingClub;
        }


        // This method takes in a list composed of Club objects and displays their name, address, and numbers them
        public void DisplayAllClubs()
        {
            int i = 0;
            foreach (Club club in ClubList)
            {
                i++;
                Console.WriteLine($"{i}. {club.Name} {club.Address}");

            }
        }

        public void DisplayAllMembersInSpecificClub(Club club)
        {
            int count = 0;
            foreach (Member member in club.ListOfMembers)
            {
                count++;
                Console.WriteLine($"{count}: Member Name: {member.Name}");
            }
        }

        public void DisplayAllClubsAllMembers()
        {
            foreach (Club club in ClubList)
            {
                Console.WriteLine($"\tClub Name: {club.Name}\n");
                Console.WriteLine($"\t\tMembers include:\n\t\t----------------------");

                foreach (Member member in club.ListOfMembers)
                {
                    Console.WriteLine($"\t\t\t{member.Name}");
                }
            }
        }


        // This method prints all of the member and clubs that the organization current has at the time of call
        public void PrintAllClubsAndMembers()
        {
            StreamWriter writer = new StreamWriter("../../../Members.txt");
            foreach (Club club in ClubList)
            {
                writer.WriteLine($"Club Name: {club.Name}");
                writer.WriteLine($"Members include:");

                foreach (Member member in club.ListOfMembers)
                {
                    writer.WriteLine($"{member.Name}");
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
                writer.WriteLine($"{club.Name}");
            }
            writer.Close();
        }

        public void RetrieveCreateDisplayClubsFromFile()
        {
            int count = 0;
            List<string> clubNameList = new List<string>();
            using (StreamReader reader = new StreamReader("../../../Clubs.txt"))
            {
                string textDocLine;
                while ((textDocLine = reader.ReadLine()) != null)
                {
                    count++;
                    clubNameList.Add(textDocLine); // Add to list.
                    Console.WriteLine($"{count}. {textDocLine}"); // Write to console.
                }
                textDocLine = reader.ReadLine();
                foreach (String clubName in clubNameList)
                {
                    ListOfClubs.Instance.AddClubToList(new Club(clubName));
                }
            }
        }
        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }
    }
}