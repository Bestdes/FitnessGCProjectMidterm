using System;
using System.Collections.Generic;


namespace FitnessGCProjectMid
{
    class Club
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public List<Member> ListOfMembers { get; set; } = new List<Member>();

        public Club(string name, string address, List<Member> memberList)
        {
            Name = name;
            Address = address;
            ListOfMembers = memberList;
        }

        public Club(string name, string address) { }
        public Club()
        {

        }

        public void DisplayClubs(List<Club> clubList)
        {
            for (int i = 0; i < clubList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {clubList[i].Name}, {clubList[i].Address}");
            }
        }

        public void AddMemberToClub(Member member)
        {
            Name = name;
            Address = address;
        }

        public void AddMemberToClub()
        {
            Console.WriteLine("What is the member name?");
            string newName = Console.ReadLine().Trim();
            Console.WriteLine("What is the member ID?");
            int newId = int.Parse(Console.ReadLine().Trim());
            Console.WriteLine("Which club would you like to be a member of?");
            foreach (Club c in ListOfClubs.Instance)
            {
                Console.WriteLine(c);
            }
            Member newMember = new MultiClubMember(newId, newName, 0);
            // This is where we ask the user for the input and write it to the member properties
            //For Both the name and the ID
            ListOfMembers.Add(newMember);
        }

        public void RemoveMemberFromClub(Member member, Club club)
        {
            Console.WriteLine("\nPlease input your ID number: ");
            string input = ReadAndReturnInput();
            int num = 0;
            bool isANum = int.TryParse(input, out num);

            foreach (Member searchingForMember in ListOfMembers.ToArray())
            {
                if (searchingForMember.Id == num)
                    try
                    {
                        bool result = int.TryParse(input, out int num);
                        foreach (Member member in ListOfMembers.ToArray())
                        {
                            if (member.ID == num)
                            {
                                ListOfMembers.Remove(member);
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("I'm sorry, that is not a valid ID number. Please try again.");
                    }

            }
        }


        public void GenerateBillsAndFees()
        {

        }

        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }

        /*public void AddMemberToClub(Member newMember)
        {
            ListOfMembers.Add(newMember);
        }*/

    }
}
