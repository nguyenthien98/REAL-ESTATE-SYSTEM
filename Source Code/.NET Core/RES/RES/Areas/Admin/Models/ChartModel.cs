using RES.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RES.Areas.Admin.Models
{
    public class ChartModel
    {
        public int totalDayOfMonth { get; set; }
        public List<int> lstNewPosts { get; set; }
        public List<int> lstNewRealPosts { get; set; }
        public List<int> lstNewUsers { get; set; }

        private RealEstateSystemContext _context = null;
        private DateTime dt = DateTime.Now;

        public ChartModel()
        {
            DateTime dt = DateTime.Now;
            this.totalDayOfMonth = DateTime.DaysInMonth(dt.Year, dt.Month);
            this.lstNewPosts = new List<int>();
            this.lstNewRealPosts = new List<int>();
            this.lstNewUsers = new List<int>();
            this._context = new RealEstateSystemContext();

            for (int i = 1; i <= totalDayOfMonth; i++)
            {
                int newPostInDay = GetNewPostInDay(i);
                int newRealPostInDay = GetRealNewPostInDay(i);
                int newUsersInDay = GetNewUserInDay(i);

                lstNewPosts.Add(newPostInDay);
                lstNewRealPosts.Add(newRealPostInDay);
                lstNewUsers.Add(newUsersInDay);
            }
        }

        private int GetNewUserInDay(int day)
        {
            int num = _context.Customer.Where(n => n.CreatedDate.Value.Day == day && n.CreatedDate.Value.Month == dt.Month && n.CreatedDate.Value.Year == dt.Year).Count();
            return num;
        }

        private int GetRealNewPostInDay(int day)
        {
            int num = _context.Post.Where(n => n.PostTime.Value.Day == day && n.PostTime.Value.Month == dt.Month && n.PostTime.Value.Year == dt.Year && n.Status != 4).Count();
            return num;
        }

        private int GetNewPostInDay(int day)
        {
            int num = _context.Post.Where(n => n.PostTime.Value.Day == day && n.PostTime.Value.Month == dt.Month && n.PostTime.Value.Year == dt.Year).Count();
            return num;
        }
    }
}
