﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace FitnessGCProjectMid
{
    //Jake
    class MultiClubMember : Member
    {
        //------------------------------------------------------------------------------------------------
        // The code below represents the Propertie(s) for the class: MultiClubMember
        public int MemPts { get; set; }

        public List<Club> ClubsAssignedTo { get; set; }

        //------------------------------------------------------------------------------------------------
        // The code below represents the Constructor(s) for the class: MultiClubMember

        public MultiClubMember(int id, string name, int memPts) : base(id, name) { }
        public MultiClubMember(int id, string firstName, string lastName, int memPts, List<Club> clubsAssignedTo) : base(id, firstName, lastName)
        {
            this.MemPts = memPts;
            this.ClubsAssignedTo = clubsAssignedTo;
        }

        public MultiClubMember(int id, string firstName, string lastName, int memPts) : base(id, firstName, lastName)
        {
            this.MemPts = memPts;
        }
        public MultiClubMember()
        {

        }


        //-------------------------------------------------------------------------------------------------
        // The code below represents the Method(s) for the class: MultiClubMember


        // This method Checks in member to their respective club (We can posibly use this method in conjuction to with another
        // method to print a list of all of the member of a club that are checked in.
        public override void CheckIn(Club club) //checking in adds membership points for MultiClubMember
        {
            //This is were we code goes that would print a text file that prints all members who are checked in to this club At the moment
            MemPts++;

            StreamWriter writer = new StreamWriter("../../../CheckedInMembers.txt");
            writer.WriteLine($"{club.Name}: {Name} + 123");
            writer.Close();
            Console.WriteLine("Success");
        }

        public override void DisplayDetails()
        {
            Console.WriteLine($"[ID: {ID}] Name: {Name} - (First: {FirstName}, Last: {LastName} - Membership Points: {MemPts})");
            Console.WriteLine($"{Name} has access to the following list of Clubs");
            foreach (Club club in ClubsAssignedTo)
            {
                Console.WriteLine(club.Name);
            }
        }
    }
}