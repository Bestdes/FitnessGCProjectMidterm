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
                            Console.WriteLine("Will the member use more than one Fitness Club?  (y/n)");
                            string multiSingleInput = ReadAndReturnInput().Trim();
                            if (multiSingleInput == "n")
                            {
                                Member singleNewMem = new SingleClubMember();
                                clubController.ActiveClub.AddMemberToClub();
                            }
                            else if (multiSingleInput == "n")
                            {
                                Member singleNewMem = new MultiClubMember(); // The logic to decide what type of Member probably has to go inside the AddMemberToClub Method
                                clubController.ActiveClub.AddMemberToClub();
                            }
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
        public static void CancelMembership()
        {
            var club = new Club();
            bool cancellingMems = true;
            while(cancellingMems)
            {
                Console.WriteLine("");
                Console.WriteLine("Begin the membership cancellation process by entering a member ID or NAME.\nWhich would you like to enter? (ID/NAME)");
                string decision = Console.ReadLine().Trim().ToLower();
                if (decision == "id")
                {
                    Member member1 = new SingleClubMember();
                    Member member2 = new MultiClubMember();

                    Console.WriteLine("\nPlease input the member's ID number: ");
                    string input = ReadAndReturnInput();
                    int num = 0;
                    bool isANum = int.TryParse(input, out num);

                    if (member1.ID == num)
                    {
                        Console.WriteLine("");
                        club.RemoveMemberFromClub(member1, num);
                        Console.WriteLine($"This membership has been located and cancelled successfully, and you'll be returned to the Modify Member Status Menu.");
                        Console.WriteLine("");
                        cancellingMems = false;
                    }
                    else if(member2.ID == num)
                    {
                        Console.WriteLine("");
                        club.RemoveMemberFromClub(member2, num);
                        Console.WriteLine($"This membership has been located and cancelled successfully, and you'll be returned to the Modify Member Status Menu.");
                        Console.WriteLine("");
                        cancellingMems = false;
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry, I could not locate a member with that ID number. Would you like to try entering the member ID again? (Y/N)");
                        string goAgainChoice = Console.ReadLine().Trim().ToLower();
                        if (goAgainChoice == "y" || goAgainChoice == "yes")
                        {
                            cancellingMems = true;
                        }
                        else if (goAgainChoice == "n" || goAgainChoice == "no")
                        {
                            cancellingMems = false;
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, I can only accept yes or no responses. Please try again.");
                            cancellingMems = true;
                        }
                    }
                }
                else if (decision == "name")
                {
                    Member member1 = new SingleClubMember();
                    Member member2 = new MultiClubMember();
                    Console.WriteLine("\nPlease input the member's full name: ");
                    string input = ReadAndReturnInput();

                    if (member1.Name == input)
                    {
                        Console.WriteLine("");
                        club.RemoveMemberFromClubByName(member1, input);
                        Console.WriteLine("This membership has been located and succesfully cancelled, and you'll be returned to the Modify Member Status Menu.");
                        Console.WriteLine("");
                        cancellingMems = false;
                    }
                    else if(member2.Name == input)
                    {
                        Console.WriteLine("");
                        club.RemoveMemberFromClubByName(member2, input);
                        Console.WriteLine("This membership has been located and succesfully cancelled, and you'll be returned to the Modify Member Status Menu.");
                        Console.WriteLine("");
                        cancellingMems = false;
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry, I could not locate a member by that name. Perhaps we experienced a typo. Would you like to try entering the member's name again? (Y/N)");
                        string goAgainChoice = Console.ReadLine().Trim().ToLower();
                        if (goAgainChoice == "y" || goAgainChoice == "yes")
                        {
                            cancellingMems = true;
                        }
                        else if (goAgainChoice == "n" || goAgainChoice == "no")
                        {
                            cancellingMems = false;
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, I can only accept yes or no responses. Please try again.");
                            cancellingMems = true;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("I'm sorry, I didn't recognize that decision. Please try again.");
                    cancellingMems = true;
                }
            }
        }

        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }
    }
}