using System;
using System.Collections.Generic;
using System.Text;

namespace FitnessGCProjectMid
{
    abstract class Member
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Member(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }
        public Member() { }

        public abstract void CheckIn(Club club);
        
    }
}
