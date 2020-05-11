using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace FitnessGCProjectMid
{
    class Program
    {
        static void Main(string[] args)
        {

            /*Club club = new Club();
            List<Club> clubList = new List<Club>();
            SingleClubMember scMember = new SingleClubMember();
            MultiClubMember mcMember = new MultiClubMember();*/

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
                            ModifyMemberStatus(activeEmployee);
                            listOfClubs.PrintAllClubsAndMembers();
                            break;

                        case 2:
                            //Option to add a member to a club
                            ModifyMemberStatus(activeEmployee);
                            break;

                        /*case 3:
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
                            break;*/

                        case 4:
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
            Console.WriteLine($"{employee.EmployeeName} what Club Database do you want to enter?");
            string clubInput = ReadAndReturnInput();
            employee.ActiveClub = ListOfClubs.Instance.GlobalFindClubByName(clubInput);
            Console.Clear();
        }

        public static void DirectionsPrompt(ClubController clubController)
        {
            Console.WriteLine($"CheckIn Members to: {clubController.ActiveClub.Name}\n" +
                $"\tPress 1: To Modify a member status\n" +
                $"\tPress 2:Search Fitness Club Database\n" +
                $"\tPress 3:Change Active Employee\n" +
                $"\tPress 4:Close Session");
        }

        public static void ModifyMemberStatus(ClubController clubController)
        {
            Console.Clear();

            bool runModifyStatus = true;
            while(runModifyStatus)
            {
                Console.WriteLine($"What would action would you like to initiate?\n\n" +
                    $"\tPress 1: To Add New Member To {clubController.ActiveClub.Name}\n" +
                    $"\tPress 2: To Add New Member To {clubController.ActiveClub.Name}\n" +
                    $"\tPress 3: To Add New Member To Other Club\n" +
                    $"\tPress 4: To Add New Member To Other Club\n" +
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

        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }
    }
}
