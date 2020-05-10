using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FitnessGCProjectMid
{
    abstract class Member
    {
        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: Member
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //------------------------------------------------------------------------------------------------
        // The code below represents the Constructor(s) for the class: Member
        //2 Overloaded Constructors + Default
        public Member(int id, string firstName, string lastName)
        {
            this.ID = id;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }

        public Member(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public Member() 
        {

        }

        //-----------------------------------------------------------------------------------------------
        // The code below represents the Method(s) for the class: Member
        public abstract void CheckIn(Club club);

        // This method generally checks if the member is a member at current location the employee is accessing from
        public abstract bool CheckIfIsAMemberOfActiveLocation();


        // This methods searches for any kind of member given the first and last name of the Member. 
        // Returns the Member if found.  Returns a null if no member is found
        // ATTENTION this method should not return null and instead loop through until a member is found !!!!!!!!!!!!!!!!!1!!!!!
        // and after a certain amount of attempts the employee should be prompted to add the member to the active club
        //ATTENTION this method need work specifically the Namesearch Aspect!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static Member NameSearch(string fnameSearch, string lnameSearch, List<Member> scMemberList, List<Member> mcMemberList)
        {
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


        // This method finds a member by their first and last name and returns the Member
        //ATTENTION this method need work specifically the Namesearch Aspect!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        public static Member PromptFindMemberByFirstAndLast()
        {
            Console.WriteLine("Please enter member's first name");
            string fName = Console.ReadLine();
            Console.WriteLine("Please enter member's last name");
            string lName = Console.ReadLine();
            try
            {
                return NameSearch(fName, lName, ListOfClubs.Instance.ClubList[1].ListOfMembers, ListOfClubs.Instance.ClubList[1].ListOfMembers);
            }
            catch
            {
                Console.WriteLine($"{fName} {lName} could not be found in the system.");
            }
            return null;
        }

        // This convert the first and last name property to the the name property for more code flexibity
        public void ConvertFirstAndLastToName()
        {
            Name = FirstName + " " + LastName;
        }
    }
}
