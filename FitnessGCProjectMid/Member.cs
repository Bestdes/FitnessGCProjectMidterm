using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    abstract class Member
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public Member(int id, string firstName, string lastName)
        {
            this.ID = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public static Member FindMember()
        {
            Console.WriteLine("Please enter member's first name");
            string fName = Console.ReadLine();
            Console.WriteLine("Please enter member's last name");
            string lName = Console.ReadLine();
            try
            {
                return NameSearch(fName, lName);
            }
            catch
            {
                Console.WriteLine($"{fName} {lName} could not be found in the system.");
            }
            return null;
        }
        public Member() { }

        public static Member NameSearch(string fnameSearch, string lnameSearch, List<Member> scMemberList, List<Member> mcMemberList)
        {
            // searches for any kind of member given the name of the member.  Returns the member if found.  Returns a null if not.
            int foundIt = -1;

            for (int i = 0; i < scMemberList.Count; i++)  // Search systen for name entered
            {
                if (lnameSearch.Trim().ToLower() == scMemberList[i].LastName.Trim().ToLower())
                {
                    if (fnameSearch.Trim().ToLower() == scMemberList[i].FirstName.Trim().ToLower())
                    {
                        foundIt = i;    // Store index for later
                        return scMemberList[i];
                    }
                }
            }
            if (foundIt == -1)  // name not found
            {
                for (int i = 0; i < scMemberList.Count; i++)  // Search systen for name entered
                {
                    if (lnameSearch.Trim().ToLower() == mcMemberList[i].LastName.Trim().ToLower())
                    {
                        if (fnameSearch.Trim().ToLower() == mcMemberList[i].FirstName.Trim().ToLower())
                        {
                            foundIt = i;    // Store index for later
                            return mcMemberList[i];
                        }
                    }
                }
            }
            return null;
        }
        public abstract void CheckIn(Club club);
        
    }
}
