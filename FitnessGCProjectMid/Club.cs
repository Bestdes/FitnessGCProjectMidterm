﻿using System;
using System.Collections.Generic;


namespace FitnessGCProjectMid
{
    class Club
    {
        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: Club
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Member> ListOfMembers { get; set; } = new List<Member>();

        //------------------------------------------------------------------------------------------------
        // The code below represents the Constructor(s) for the class: Club

        public Club(string name, string address, List<Member> memberList)
        {
            Name = name;
            Address = address;
            ListOfMembers = memberList;
        }

        public Club(string name, string address)
        {
            Name = name;
            Address = address;
        }
        public Club()
        {

        }

        //------------------------------------------------------------------------------------------------
        // The code below represents the Method(s) for the class: Club


        // This method below is the primary Add Member logic that will be used by the User Interface Logic (switch statement)
        public void AddMemberToClub()
        {
            Console.WriteLine("What is the member name?");
            string newName = Console.ReadLine().Trim();
            Console.WriteLine("What is the member ID?");
            int newId = int.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Which club would you like to be a member of?");
            /*foreach (Club c in ListOfClubs.Instance.ClubList)
            {
                Console.WriteLine(c);
            }*/
            Member newMember = new MultiClubMember(newId, newName, 0);
            // This is where we ask the user for the input and write it to the member properties
            //For Both the name and the ID
            ListOfMembers.Add(newMember);
        }

        // This method below is the primary Remove Member logic that will be used by the User Interface Logic (switch statement)
        public void RemoveMemberFromClub(Member member)
        {
            try
            {
                Console.WriteLine("\nPlease input your ID number: ");
                string input = ReadAndReturnInput();
                int num = 0;
                bool isANum = int.TryParse(input, out num);

                foreach (Member searchedForMember in ListOfMembers.ToArray())
                {
                    if (searchedForMember.ID == num)
                    {
                        ListOfMembers.Remove(searchedForMember);
                    }
                    else
                    {
                        Console.WriteLine("I'm sorry, that is not a valid ID number. Please try again.");
                    }
                }
            }
            catch(OverflowException numTooBig)
            {
                Console.WriteLine("The number you entered is too large. Please try again! (Example ID: 1234)");
            }
        }

        // Overloaded Remove member method
        public void RemoveMemberFromClub()
        {
            Member searchedMem = FindMemberByIDLoop();

            ListOfMembers.Remove(searchedMem);
        }


        public void GenerateBillsAndFees()
        {

        }

        // This is simply a helper method that makes more simple Console.ReadLine() Method
        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }


        // This method is a very important method that how we identify member in a Club, it takes in a Club but,
        // we should be able to overload it if logically it makes more sense to use the entire list of clubs as the parameter
        public Member FindMemberByIDLoop()
        {

            int num = 0;
            bool isANum;
            bool runSearch = true;
            bool isMulti = false;

            Member single = new SingleClubMember();
            Member multi = new MultiClubMember();


            while (runSearch)
            {
                Console.WriteLine("Please enter member's ID:");
                string input = ReadAndReturnInput();
                isANum = int.TryParse(input, out num);

                if (isANum == true)
                {
                    foreach (Member searchingForMember in ListOfMembers.ToArray()) // This represents the active Club the emplyee is accessing from
                    {
                        if (searchingForMember.ID == num)
                        {
                            if (searchingForMember.GetType().Equals(single.GetType()))
                            {
                                single = searchingForMember;
                                isMulti = false;
                                runSearch = false;
                                return single;
                            }
                            else if (searchingForMember.GetType().Equals(multi.GetType()))
                            {
                                multi = searchingForMember;
                                isMulti = true;
                                return multi;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Error ID Number not found at this location!");
                            runSearch = false;
                        }
                    }
                }
                else if (!isANum)
                {
                    Console.WriteLine("I'm sorry, that is not a valid ID number. Please try again.");
                }
            }
            return single;
        }

        // This method just adds the behaviour of a member checking in from the active Club class
        public void MemberCheckIn(Member member, Club club)
        {

            member = FindMemberByIDLoop();
            try
            {
                member.CheckIn(ListOfClubs.Instance.GlobalFindClubOfMember(member.ID));
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

        // This method is a simple AddMemberToClub primarily used for testing purposes.
        public void AddMemberToClub(Member newMember)
        {
            ListOfMembers.Add(newMember);
        }
    }
}
