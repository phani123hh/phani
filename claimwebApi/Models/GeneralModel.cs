using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace claimwebApi.Models
{
    public class GeneralModel
    {

        public class Member
        {
            public Member(string MemberID, string EnrollmentDate, string FirstName, string LastName)
            {
                this.MemberID = MemberID;
                this.EnrollmentDate = EnrollmentDate;
                this.FirstName = FirstName;
                this.LastName = LastName;
            }

            public string MemberID { get; set; }
            public string EnrollmentDate { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
        public class Claim
        {
            public Claim(string MemberID, string ClaimDate, string ClaimAmount)
            {
                this.MemberID = MemberID;
                this.ClaimDate = ClaimDate;
                this.ClaimAmount = ClaimAmount;
            }

            public string MemberID { get; set; }
            public string ClaimDate { get; set; }
            public string ClaimAmount { get; set; }

        }

        public class ErrorResponse
        {
            public string Code { get; set; }
            public string Response { get; set; }

        }


        public class SuccessResponse
        {
            public string MemberID { get; set; }
            public string EnrollmentDate { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string ClaimDate { get; set; }
            public string ClaimAmount { get; set; }

        }
        public object bussinessLogic(string date)
        { 
            string jsonsresult = string.Empty;
            try
            {          
                using (var reader = new StreamReader(@"E:\claimwebApi\claimwebApi\Resoources\Claim.csv"))
                using (var reader2 = new StreamReader(@"E:\claimwebApi\claimwebApi\Resoources\Member.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    using (var csv2 = new CsvReader(reader2, CultureInfo.InvariantCulture))
                    {
                        var cliam = csv.GetRecords<GeneralModel.Claim>().ToList();
                        var Member = csv2.GetRecords<GeneralModel.Member>().ToList();
                        var q = from p in cliam
                                join c in Member
                                on p.MemberID equals c.MemberID

                                select new
                                {
                                    MemberID = p.MemberID,
                                    EnrollmentDate = c.EnrollmentDate,
                                    FirstName = c.FirstName,
                                    LastName = c.LastName,
                                    ClaimDate = p.ClaimDate,
                                    ClaimAmount = p.ClaimAmount
                                };
                        var x = q.Where(m => Convert.ToDateTime(m.ClaimDate) <= Convert.ToDateTime(date));
                       return    JsonConvert.SerializeObject(x);
                        

                    }
                } 
            }
            catch (Exception ex)
            {
                ErrorResponse obj = new ErrorResponse();
                obj.Code = "XXXXX";
                obj.Response = ex.Message.ToString();
                jsonsresult = JsonConvert.SerializeObject(obj);
                return null;
            }
            
        }


    }
}