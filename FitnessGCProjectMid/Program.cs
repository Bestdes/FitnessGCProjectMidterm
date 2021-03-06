﻿using System;
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


            //------------------------------------------------------------------------------------------------
            //Beginning of Program Logic

            bool runProgram = true;
            bool activeEmployeeSession = false;

            while (runProgram)
            {
                ClubController activeEmployee = new ClubController();
                activeEmployee.ClearCheckInMembers();
                GreetingsPrompt(activeEmployee);

                activeEmployeeSession = true;

                while (activeEmployeeSession)
                {
                    bool gettingMenuOption = true;
                    while (gettingMenuOption)
                    {
                        Console.WriteLine("This is the Main Menu of the Grand Circus Fitness Club Manager\nPlease input a menu option for services\n");

                        DirectionsPrompt(activeEmployee);

                        string input = Console.ReadLine().Trim();
                        int num = 0;
                        bool isANum = int.TryParse(input, out num);

                        if (isANum)
                        {
                            if (num > 0 && num <= 5)
                            {
                                switch (num)
                                {
                                    case 1:
                                        //Option to check a member into a club
                                        activeEmployee.ActiveClub.MemberCheckIn(activeEmployee.ActiveClub);
                                        ListOfClubs.Instance.PrintAllClubsAndMembers();
                                        Console.Clear();
                                        break;

                                    case 2:
                                        //Option to add a member to a club
                                        ModifyMemberStatus(activeEmployee);
                                        ListOfClubs.Instance.PrintAllClubsAndMembers();
                                        break;

                                    case 3:
                                        //option to remove a member from a club
                                        SearchDataBase(activeEmployee);
                                        break;
                                    case 4:
                                        activeEmployeeSession = LoginAnotherUser();
                                        gettingMenuOption = false;
                                        Console.Clear();
                                        break;

                                    case 5:
                                        //option to exit the program
                                        Console.WriteLine("Logging you out and closing the program...");
                                        Environment.Exit(0);
                                        break;
                                }
                            }
                            else if (num > 5)
                            {
                                Console.WriteLine("I'm sorry, that number is too high and I didn't recognize it. Please try again.");
                                gettingMenuOption = true;
                            }
                            else if (num <= 0)
                            {
                                Console.WriteLine("I'm sorry, that number is too low and I didn't recognize it. Please try again.");
                                Console.WriteLine("");
                                gettingMenuOption = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, I didn't recognize a number. Please try again.");
                            Console.WriteLine("");
                            gettingMenuOption = true;
                        }
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
            bool cont = true;
            while (cont)
            {
                Console.WriteLine($"{employee.EmployeeName}, What Club Database do you want to enter?\nSelect by number please:\n");
                /*Console.WriteLine();*/
                ListOfClubs.Instance.RetrieveCreateDisplayClubsFromFile();
                Console.WriteLine();
                string clubInput = ReadAndReturnInput();
                int clubNumSelection = 0;
                bool IsClubNumInput = int.TryParse(clubInput, out clubNumSelection);
                if (IsClubNumInput)
                {
                    if (clubNumSelection <= ListOfClubs.Instance.ClubList.Count() || clubNumSelection >= 1)
                    {
                        foreach (Club testingClub in ListOfClubs.Instance.ClubList)
                        {
                            i++;
                            if (clubNumSelection == i)
                            {
                                employee.ActiveClub = testingClub;
                                Console.WriteLine(employee.ActiveClub.Name);
                                cont = false;
                            }
                        }
                        Console.Clear();
                    }
                }
                else
                {
                    Console.WriteLine("That is not a valid input. Please select 1 of the following 5 options.");
                    Console.Clear();
                }
            }
            ListOfClubs.Instance.CreateMembersFromFile();
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
                Console.Clear();

                Console.WriteLine($"What would action would you like to initiate? Please enter the number associated with your choice.\n\n" +
                    $"\tPress 1: To Add New Member To: {clubController.ActiveClub.Name}\n" +
                    $"\tPress 2: To Remove Member From: {clubController.ActiveClub.Name}\n" +
                    $"\tPress 3: To Add New Member To: Other Club\n" +
                    $"\tPress 4: To Remove Member From: Other Club\n" +
                    $"\tPress 5: To Return To Main Menu");

                string input = ReadAndReturnInput();
                int confirmedNum;
                bool isANum = int.TryParse(input, out confirmedNum);


                if (isANum && confirmedNum <= 0 || isANum && confirmedNum > 5)
                {
                    Console.WriteLine("The input you entered is not a valid number!\nPlease enter 1 through 5.");
                }
                if (isANum)
                {

                    switch (confirmedNum)
                    {
                        case 1:
                            // The logic to decide what type of Member probably has to go inside the AddMemberToClub Method
                            Console.Clear();
                            clubController.ActiveClub.AddMemberToClub();
                            break;
                        case 2:
                            Console.Clear();
                            CancelMembership();
                            break;
                        case 3:
                            Console.Clear();
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
                            Console.Clear();
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
                if (isANum == false)
                {

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
                            ListOfClubs.Instance.GlobalFindMemberFromClub();
                            Console.ReadLine();
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

                            Console.WriteLine("Not a valid number, please try again.");
                    }
                }
                else if (isANum && confirmedNum <= 0 || isANum && confirmedNum > 6)
                {
                    Console.WriteLine("The input you entered is not a valid number!\nPlease enter 1 through 6.");
                }
                else if (isANum)
                {
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
                else
                {
                    Console.WriteLine("I'm sorry, I didn't recognize that response. Please try again.");
                    runSearchDatabase = true;
                }
            }
        }
        public static void CancelMembership()
        {
            var club = new Club();
            bool cancellingMems = true;

            while (cancellingMems)

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

                    if (isANum)
                    {

                        if (num == member1.ID || num == member2.ID)
                        {
                            if (member1.ID == num)
                            {
                                Console.WriteLine("");
                                club.RemoveMemberFromClub(member1, num);
                                Console.WriteLine($"This membership has been located and cancelled successfully, and you'll be returned to the Modify Member Status Menu.");
                                Console.WriteLine("");
                                cancellingMems = false;
                            }
                            else if (member2.ID == num)
                            {
                                Console.WriteLine("");
                                club.RemoveMemberFromClub(member2, num);
                                Console.WriteLine($"This membership has been located and cancelled successfully, and you'll be returned to the Modify Member Status Menu.");
                                Console.WriteLine("");
                                cancellingMems = false;
                            }
                        }
                        else
                        {
                            Console.WriteLine("I'm sorry, I could not locate a member with that ID number. Would you like to try cancelling this membership again? (Y/N)");
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
                        Console.WriteLine("I'm sorry, I did not recognize a number there. Please try again.");
                        cancellingMems = true;
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
                    else if (member2.Name == input)
                    {
                        Console.WriteLine("");
                        club.RemoveMemberFromClubByName(member2, input);
                        Console.WriteLine("This membership has been located and succesfully cancelled, and you'll be returned to the Modify Member Status Menu.");
                        Console.WriteLine("");
                        cancellingMems = false;
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry, I could not locate a member by that name. Perhaps we experienced a typo. Would you like to try cancelling this membership again? (Y/N)");
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
    }
}