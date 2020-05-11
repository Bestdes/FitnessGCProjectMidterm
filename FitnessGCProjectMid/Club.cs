using System;
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

        // This method displays member info
        public void DisplayMemberInfo(Member member)
        {
            Console.WriteLine($"Member name: {member.FirstName} {member.LastName}");
            Console.WriteLine($"Member ID: {member.ID}");
            if (member is MultiClubMember multimem)
            {
                Console.WriteLine($"Membership Points: {multimem.MemPts}");
            }
            else if (member is SingleClubMember singlemem)
            {
                Console.WriteLine($"Club Assignment: {singlemem.ClubAssign}");
            }
        }

        // This method below is the primary Add Member logic that will be used by the User Interface Logic (switch statement)
        public void AddMemberToClub()
        {
            int i = 0;
            Console.WriteLine($"Which club would you like to be a member of? Please enter 1 through {ListOfClubs.Instance.ClubList.Count}");
            foreach (Club c in ListOfClubs.Instance.ClubList) //listing clubs for user to choose
            {
                i++;
                Console.WriteLine($"{i}. {c.Name}");
            }
            Console.WriteLine($"6. Multi-club membership"); //or choose a multiclub membership
            i = 0;
            string input = Console.ReadLine().Trim();
            if (!int.TryParse(input, out int userChoice))
            {
                Console.WriteLine($"That is not a number. Please enter 1 through {ListOfClubs.Instance.ClubList.Count}");
                AddMemberToClub();
            }
            else if (userChoice <= 0 || userChoice > ListOfClubs.Instance.ClubList.Count && userChoice != 6)
            {
                Console.WriteLine($"That is not an option. Please enter 1 through {ListOfClubs.Instance.ClubList.Count}");
                AddMemberToClub();
            }
            else
            {
                userChoice = int.Parse(input);
                Console.WriteLine("What is the member's first name?");
                string newName = Console.ReadLine().Trim();
                Console.WriteLine("What is the member's last name?");
                string newLastName = Console.ReadLine().Trim();
                Console.WriteLine("What is the member ID?");
                int newId = int.Parse(Console.ReadLine().Trim());
                foreach (Club c in ListOfClubs.Instance.ClubList) //adding a single club member 
                {
                    i++;
                    if (userChoice == i) //choosing by int index (i)
                    {
                        Console.WriteLine($"New Member: {newName} {newLastName} added to {c.Name} with ID: {newId}");
                        Member newSingleMember = new SingleClubMember(newId, newName, newLastName, c);
                        ListOfMembers.Add(newSingleMember);
                    }
                }
                if (userChoice == 6) //or adding a multiclub member
                {
                    Console.WriteLine($"New Member: {newName} {newLastName} added as a multi-club member with ID: {newId}");
                    Member newMultiMember = new MultiClubMember(newId, newName, newLastName, 0);
                    ListOfMembers.Add(newMultiMember);
                }
            }
        }



        // This method below is the primary Remove Member logic that will be used by the User Interface Logic (switch statement)
        public void RemoveMemberFromClub(Member member)
        {
            try
            {
                Console.WriteLine("\nPlease input the member's ID number: ");
                string input = ReadAndReturnInput();
                int num = 0;
                bool isANum = int.TryParse(input, out num);

                foreach (Member searchedForMember in ListOfMembers.ToArray())
                {
                    if (searchedForMember.ID == num)
                    {
                        ListOfMembers.Remove(searchedForMember);
                        Console.WriteLine("The membership for this person has been cancelled.");
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
            int i = 0;
            Console.WriteLine("Which member would you like to add fees to?");
            foreach (Member member in ListOfMembers)
            {
                i++;
                Console.WriteLine($"{i}. {member.FirstName} {member.LastName}");
                string input = Console.ReadLine();
            }
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
        
        public bool ValidateIfMemberisInClub(Member member, Club club)
        {
            bool isAMember = false;

            if(club.ListOfMembers.Contains(member))
            {
                isAMember = true;
                return isAMember;
            }
            else
            {
                return isAMember;
            }
        }

        // This method just adds the behaviour of a member checking in from the active Club class
        public void MemberCheckIn(Club club)
        {
            Member member = FindMemberByIDLoop();

            if (ValidateIfMemberisInClub(member, club))
            {
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
            else
            {
                Console.WriteLine($"Error, not a member of: {club.Name}");
            }
        }

        // This method is a simple AddMemberToClub primarily used for testing purposes.
        public void AddMemberToClub(Member newMember)
        {
            ListOfMembers.Add(newMember);
        }

    }
}
