using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using JoinUsClassLibrary.Models;
using System.Linq;

namespace JoinUs.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new DbInitializer());
            using (JoinUsContext context = GetContext())
            {
                context.Database.Initialize(true);
            }
        }

        private static JoinUsContext GetContext()
        {
            return new JoinUsContext();
        }
        [TestMethod]
        public void CanGetUsers()
        {
            using (JoinUsContext context = GetContext())
            {
                Assert.AreEqual(2, context.Users.ToList().Count);
            }
        }
    }
}
