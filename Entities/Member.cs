using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public class Member
    {
        public int MemberId
        { get; set; }
        public string FirstName
        { get; set; }
        public string LastName
        { get; set; }
        public string Status
        { get; set; }
        public double MemberPoint 
        { get; set; }
        public string[] MemberInfo
        { get; set; }
        public Member(int memberId, string firstName, string lastName, string status, double memberPoint)
        {
            MemberInfo = new string[]
            {
             Convert.ToString(memberId),
             firstName,
             lastName,
             status,
              Convert.ToString(memberPoint)
            };
            this.MemberId = memberId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Status = status;
            this.MemberPoint = memberPoint;
        }
        public Member()
        {
        }
        public Member(int memberId)
        {
            this.MemberId = memberId;
        }
    }
}
