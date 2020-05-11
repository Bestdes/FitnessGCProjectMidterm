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
            ListOfClubs listOfClubs = ListOfClubs.Instance;

            Club testingClub = new Club("Testing Club", "First Address");
            Member testingMember = new SingleClubMember(123, "Jon Doe", testingClub);

            testingClub.AddMemberToClub(testingMember);

            listOfClubs.AddClubToList(testingClub);
            

            listOfClubs.PrintAllClubsAndMembers();

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
                            listOfClubs.PrintAllClubsAndMembers();
                            Console.Clear();
                            break;

                        case 2:
                            //Option to add a member to a club
                            ModifyMemberStatus(activeEmployee);
                            break;

                        /*case 3:
                            //option to remove a member from a club
                            CancelMembership();
                            break;*/
                        case 4:
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
            Console.WriteLine($"Hello, employee please input your name: ");
            string input = ReadAndReturnInput();
            employee.EmployeeName = input;
            Console.Clear();           
            Console.WriteLine($"{employee.EmployeeName}, What Club Database do you want to enter?");
            Console.WriteLine();
            ListOfClubs.Instance.DisplayAllCLubs(); 
            Console.WriteLine();
            string clubInput = ReadAndReturnInput();
            employee.ActiveClub = ListOfClubs.Instance.GlobalFindClubByName(clubInput);
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

        public static void ModifyMemberStatus(ClubController clubController)
        {
            Console.Clear();

            bool runModifyStatus = true;
            while(runModifyStatus)
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

                if(isANum)
                {

                    switch (confirmedNum)
                    {
                        case 1:
                            Console.WriteLine("Will the member use more than one Fitness Club?  (y/n)");
                            string multiSingleInput = ReadAndReturnInput().Trim();
                            if(multiSingleInput == "n")
                            {
                                Member singleNewMem = new SingleClubMember();
                                clubController.ActiveClub.AddMemberToClub();
                            }
                            else if(multiSingleInput == "n")
                            {
                                Member singleNewMem = new MultiClubMember(); // The logic to decide what type of Member probably has to go inside the AddMemberToClub Method
                                clubController.ActiveClub.AddMemberToClub();
                            }
                            break;
                        case 2:
                            clubController.ActiveClub.RemoveMemberFromClub();
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
                            ListOfClubs.Instance.GlobalFindClubByName("").RemoveMemberFromClub();
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
        public static void CancelMembership()
        {
            var club = new Club("Planet Fitness","1234 Leisure Drive");
            var scMember = new SingleClubMember();
            var mcMember = new MultiClubMember();
            var clubController = new ClubController();

            Console.WriteLine("Begin the membership cancellation process by entering a member ID or NAME.\nWhich would you like to enter? (ID/NAME)");
            string decision = Console.ReadLine().Trim().ToLower();
            if (decision == "id")
            {
                Console.WriteLine("Enter the ID number of the person you wish to cancel the membership for:");
                string memberID = ReadAndReturnInput();
                bool isAnID = int.TryParse(memberID, out int result);
                if(isAnID)
                {
                    Console.WriteLine($"{ListOfClubs.Instance.GlobalFindClubOfMember(result).Name}");
                    Club otherClub = ListOfClubs.Instance.GlobalFindClubOfMember(result);
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

                string[] fileLines = System.IO.File.ReadAllLines(membersFile,System.Text.Encoding.Default);
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
