﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mvc.JQuery.Datatables.Example.Controllers
{
    public class HomeController : Controller
    {
        private static List<User> _users;
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public enum PositionTypes
        {
            Engineer,
            Tester,
            Manager
        }

        public DataTablesResult<UserView> GetUsers(DataTablesParam dataTableParam)
        {
            var users = Users().AsQueryable();

            return DataTablesResult.Create(users, dataTableParam, user => new UserView()
            {
                Position = user.Position == null ? "" : user.Position.ToString(),
                Number = user.Number,
                Email = user.Email,
                Name = new MvcHtmlString("<b>" + user.Name + "</b>"),
                Hired = user.Hired,
                someOtherInfo1 = "",
                someOtherInfo2 = "",
                someOtherInfo3 = string.Format("Id is {0}", user.Id)
            });
        }
        public DataTablesResult GetUsersUntyped(DataTablesParam dataTableParam)
        {
            var users = Users();

            return DataTablesResult.Create(users, dataTableParam);
        }

        private static List<User> Users()
        {
            var r = new Random();
            var domains = "gmail.com,yahoo.com,hotmail.com".Split(',').ToArray();

            var positions = new List<PositionTypes?> { null, PositionTypes.Engineer, PositionTypes.Tester, PositionTypes.Manager };
            return _users ?? (_users = new List<User>
                (
                Enumerable.Range(1, 100).Select(i =>
                                                new User()
                                                {
                                                    Id = i,
                                                    Email = "user" + i + "@" + domains[i%domains.Length],
                                                    Name = r.Next(6) == 3 ? null : "User" + i,
                                                    Position = positions[i%positions.Count],
                                                    Number = (Numbers) r.Next(4),
                                                    Hired = DateTimeOffset.UtcNow.AddDays(-1 * 365 * 3 * r.NextDouble())
                                                })
                ));
            return _users;
        }
    }

    public enum Numbers
    {
        Zero,
        One,
        Two,
        Three,
        Four
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public HomeController.PositionTypes? Position { get; set; }

        public DateTimeOffset Hired { get; set; }

        public Numbers Number { get; set; }
    }

    public class UserView
    {
        public string Position { get; set; }
        public Numbers Number { get; set; }
        public string Email { get; set; }
        public MvcHtmlString Name { get; set; }
        public DateTimeOffset Hired { get; set; }
        public string someOtherInfo1 { get; set; }
        public string someOtherInfo2 { get; set; }
        public string someOtherInfo3 { get; set; }
    }

     
}