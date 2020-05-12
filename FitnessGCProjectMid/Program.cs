using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FitnessGCProjectMid
{
    class Program
    {
        static void Main(string[] args)
        {
            /*ListOfClubs listOfClubs = ListOfClubs.Instance;

            Club testingClub = new Club("Testing Club", "First Address");
            Member testingMember = new SingleClubMember(123, "Jon Doe", testingClub);

            testingClub.AddMemberToClub(testingMember);
            listOfClubs.PrintAllClubsAndMembers();*/

            //------------------------------------------------------------------------------------------------
            //Beginning of Program Logic

            bool runProgram = true;
            bool activeEmployeeSession = false;


            while (runProgram)
            {
                

                ClubController activeEmployee = new ClubController();
                GreetingsPrompt(activeEmployee);

                activeEmployeeSession = true;

                while (activeEmployeeSession)
                {

                    Console.WriteLine("This is the Main Menu of the Grand Circus Fitness Club Manager\nPlease input a menu option for services\n");

                    DirectionsPrompt(activeEmployee);

                    string input = Console.ReadLine().Trim();
                    int num = 0;
                    bool isANum = int.TryParse(input, out num);

                    switch (num)
                    {
                        case 1:
                            //Option to check a member into a club
                            activeEmployee.ActiveClub.MemberCheckIn(activeEmployee.ActiveClub);
                            ListOfClubs.Instance.PrintAllClubsAndMembers();
                            Console.Clear();
                            break;

                        case 2:
                            //Option to a member to a club
                            ModifyMemberStatus(activeEmployee);
                            ListOfClubs.Instance.PrintAllClubsAndMembers();
                            break;

                        case 3:
                            //option to remove a member from a club
                            SearchDataBase(activeEmployee);
                            break;
                        case 4:
                            activeEmployeeSession = LoginAnotherUser();
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

        public static void GreetingsPrompt(ClubController employee)
        {
            int i = 0;

            Console.WriteLine($"Hello, employee please input your name: ");
            string input = ReadAndReturnInput();
            employee.EmployeeName = input;
            Console.Clear();     
            
            Console.WriteLine($"{employee.EmployeeName}, What Club Database do you want to enter?\nSelect by number please:\n");
            /*Console.WriteLine();*/

            ListOfClubs.Instance.RetrieveCreateDisplayClubsFromFile();
            Console.WriteLine();

            string clubInput = ReadAndReturnInput();
            int numInput = int.Parse(clubInput);
            foreach (Club testingClub in ListOfClubs.Instance.ClubList)
            {
                i++;
                if (numInput == i)
                {
                    employee.ActiveClub = testingClub;
                    Console.WriteLine(employee.ActiveClub.Name);
                }
            }
            Console.Clear();
        }

        public static void DirectionsPrompt(ClubController clubController)
        {
            Console.WriteLine($"\tPress 1: CheckIn Members to: {clubController.ActiveClub.Name}\n" +
                $"\tPress 2: To Modify a Member Status\n" +
                $"\tPress 3: Search Fitness Club Database\n" +
                $"\tPress 4: Login To Another User\n" +
                $"\tPress 5: Close Session");
        }

        public static bool LoginAnotherUser()
        {
            bool keepAsking = true;
            while (keepAsking)
            {
                Console.WriteLine("Would you like to login to another user? y/n");
                string response = Console.ReadLine().ToLower().Trim();
                if (response != "y" && response != "n")
                {
                    Console.WriteLine("I did not understand that. Please enter y/n");
                    keepAsking = true;
                }
                else if (response == "y")
                {
                    keepAsking = false;
                    return false;
                }
                else if (response == "n")
                {
                    keepAsking = false;
                    return true;
                }
            }
            return true;
        }
        public static void ModifyMemberStatus(ClubController clubController)
        {
            Console.Clear();

            bool runModifyStatus = true;
            bool runMStatus1 = true;

            while (runModifyStatus)
            {
                Console.WriteLine($"What would action would you like to initiate?\n\n" +
                    $"\tPress 1: To Add New Member To: {clubController.ActiveClub.Name}\n" +
                    $"\tPress 2: To Remove Member From: {clubController.ActiveClub.Name}\n" +
                    $"\tPress 3: To Add New Member To: Other Club\n" +
                    $"\tPress 4: To Remove Member From: Other Club\n" +
                    $"\tPress 5: To Return To Main Menu");

                string input = ReadAndReturnInput();
                int confirmedNum;
                bool isANum = int.TryParse(input, out confirmedNum);
                

                if (isANum)
                {
                    switch (confirmedNum)
                    {
                        case 1:
                            while (runMStatus1)
                            {
                                Console.WriteLine("Will the member use more than one Fitness Club?  (y/n)");
                                string multiSingleInput = ReadAndReturnInput().Trim();
                                if (multiSingleInput == "n")
                                {
                                    Member singleNewMem = new SingleClubMember();
                                    clubController.ActiveClub.AddMemberToClub();
                                    runModifyStatus = false;
                                }
                                else if (multiSingleInput == "y")
                                {
                                    Member multiNewMem = new MultiClubMember(); // The logic to decide what type of Member probably has to go inside the AddMemberToClub Method
                                    clubController.ActiveClub.AddMemberToClub();
                                    runModifyStatus = false;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid response entered. Please input y for yes and n for no.");
                                }
                            }
                            runModifyStatus = true;
                            break;
                        case 2:
                            CancelMembership();
                            break;
                        case 3:
                            Console.WriteLine("What is the name of the club");
                            string clubInput = ReadAndReturnInput();
                            if (ListOfClubs.Instance.GlobalFindIfClubExists(clubInput))
                            {
                                ListOfClubs.Instance.GlobalFindClubByName(clubInput).AddMemberToClub();
                            }
                            else
                            {
                                Console.WriteLine("The club name you entered did not return a club in the database");
                            }
                            break;
                        case 4:
                            //ListOfClubs.Instance.GlobalFindClubByName("").RemoveMemberFromClub();
                            CancelMembership();
                            break;
                        case 5:
                            runModifyStatus = false;
                            Console.Clear();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("The input you entered is not a valid number!\nPlease try again.");
                }

            }
        }

        public static void SearchDataBase(ClubController clubController)
        {
            Console.Clear();

            bool runSearchDatabase = true;

            while (runSearchDatabase)
            {
                Console.Clear();

                Console.WriteLine($"What action would you like to initiate?\n\n" +
                        $"\tPress 1: To Search for A Specific Member In The National Database\n" +
                        $"\tPress 2: To Display All Members in {clubController.ActiveClub.Name}\n" +
                        $"\tPress 3: To Display All Clubs\n" +
                        $"\tPress 4: To Display All Members of A Specific Club\n" +
                        $"\tPress 5: To Display All Members of All Clubs\n" +
                        $"\tPress 6: To Return to the Main Menu");

                string input = ReadAndReturnInput();
                int confirmedNum;
                bool isANum = int.TryParse(input, out confirmedNum);

                switch (confirmedNum)
                {
                    case 1:
                        Console.Clear();
                        ListOfClubs.Instance.GlobalFindMember();
                        Console.ReadLine();
                        break;
                    case 2:
                        //Display all members in Active Club
                        Console.Clear();
                        clubController.ActiveClub.DisplayAllMembers();
                        Console.ReadLine();
                        break;
                    case 3:
                        Console.Clear();
                        ListOfClubs.Instance.DisplayAllClubs();
                        Console.ReadLine();
                        break;
                    case 4:
                        Console.Clear();
                        break;
                    case 5:
                        Console.Clear();
                        ListOfClubs.Instance.DisplayAllClubsAllMembers();
                        Console.ReadLine();
                        break;
                    case 6:
                        runSearchDatabase = false;
                        Console.Clear();
                        break;
                }
            }
        }
        public static void CancelMembership()
        {
            var club = new Club();
            var scMember = new SingleClubMember();
            var mcMember = new MultiClubMember();
            var clubController = new ClubController();

            Console.WriteLine("Begin the membership cancellation process by entering a member ID or NAME.\nWhich would you like to enter? (ID/NAME)");
            string decision = Console.ReadLine().Trim().ToLower();
            if (decision == "id")
            {
                bool byID = true;
                if(byID)
                {
                    Member member1 = new SingleClubMember();
                    club.RemoveMemberFromClub(member1);
                    Console.WriteLine($"This membership has been cancelled successfully.");
                }
                else
                {
                    Console.WriteLine("I'm sorry, I could not locate a member with that ID number.");
                }
            }
            else if (decision == "name")
            {
                    string membersFile = "../../../members.txt";
                    Console.WriteLine("Enter the member's first name:");
                    string firstName = ReadAndReturnInput().ToLower();
                    Console.WriteLine("Enter the member's last name:");
                    string lastName = ReadAndReturnInput().ToLower();
                    string name = firstName + " " + lastName;

                    Member member2 = new SingleClubMember(5, name);

                    string[] fileLines = System.IO.File.ReadAllLines(membersFile, System.Text.Encoding.Default);
                    for (int i = 0; i < fileLines.Length; i++)
                    {
                        if (fileLines[i].Contains(member2.Name))
                        {
                            Console.WriteLine("The associated membership has been cancelled");
                        }
                        else
                        {
                            Console.WriteLine("I could not locate a member by that name.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't recognize that decision.");
                }
                Console.WriteLine("");
                Console.WriteLine();
            }


        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }
    }
}
