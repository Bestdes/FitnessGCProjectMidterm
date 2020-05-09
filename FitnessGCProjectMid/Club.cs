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
            // This is where we ask the user for the input and write it to the member properties
            //For Both the name and the ID
            ListOfMembers.Add(member);
        }
        public void RemoveMemberFromClub(Member member, Club club)
        {
            Console.WriteLine("\nPlease input your ID number: ");
            string input = ReadAndReturnInput();
            try
            {
                bool result = int.TryParse(input, out int num);
                foreach(Member member in ListOfMembers.ToArray())
                {
                    if  (member.ID == num)
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

        public void MemberCheckIn(Member member)
        {
            try
            {
                member.CheckIn();
            }
            catch (Exception anyException)
            {
                Console.WriteLine("Some type of Exception is thrown");
            }
        }

        public void 


        public static string ReadAndReturnInput()
        {
            return Console.ReadLine();
        }

    }
}
