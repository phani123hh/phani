using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using claimwebApi.Models;
using System.Web.Mvc;


namespace claimwebApi.Controllers
{
    public class ClaimController : ApiController
    {
        public JsonResult Get(string Date)
        {
            object s = null;
            string SwitchGetResponse = string.Empty;
            string contenttype = System.Web.HttpContext.Current.Request.ContentType;
            try {
                //List<GeneralModel.Claim> Claimls = new List<GeneralModel.Claim> { new GeneralModel.Claim("1123", "10-06-2020", "1112.56"), new GeneralModel.Claim("1245", "12-05-2020", "67.54") };
                //List<GeneralModel.Member> Memberls = new List<GeneralModel.Member> { new GeneralModel.Member("1123", "09-01-2020", "John", "Doe"), new GeneralModel.Member("1245", "10-03-2020", "Jane", "Doe") };
                GeneralModel obj = new GeneralModel();
                 s = obj.bussinessLogic( Date);

            }
            catch (Exception ex) {

                GeneralModel.ErrorResponse obj = new GeneralModel.ErrorResponse();
                obj.Code = "XXXXX";
                obj.Response = ex.Message.ToString();
             
            }
             
            return new JsonResult()
            {
                Data = s,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

      
    }
}
