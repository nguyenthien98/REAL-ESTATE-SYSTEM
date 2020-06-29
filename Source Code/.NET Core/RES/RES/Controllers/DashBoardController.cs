using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RES.Data.DBModels;

namespace RES.Controllers
{
    [Route("dashboard")]
    public class DashBoardController : Controller
    {
        public string Index(string id)
        {
            using(RealEstateSystemContext db = new RealEstateSystemContext())
            {
                switch (id)
                {
                    case "fb":
                        db.Dashboard.First().TotalFbclick++;
                        break;
                    case "lin":
                        db.Dashboard.First().TotalLinClick++;
                        break;
                    case "ins":
                        db.Dashboard.First().TotalInsClick++;
                        break;
                    case "twi":
                        db.Dashboard.First().TotalTwiClick++;
                        break;
                    case "pin":
                        db.Dashboard.First().TotalPinClick++;
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
                return "1";
            }
        }

        [HttpGet("convert-money")]
        public string ConvertMoney(string input = "0")
        {
            double inputValue = double.Parse(input);

            return RES.Models.Commons.ConvertMoneyFunction.So_chu(inputValue);
        }
    }
}