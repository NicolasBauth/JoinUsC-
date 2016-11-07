using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using JoinUsClassLibrary;
using System.Linq;

namespace JoinUsTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void canGetEvents()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Events.ToList().Count);
            }
        }
        [TestMethod]
        public void canGetTags()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Tags.ToList().Count);
            }
        }
        [TestMethod]
        public void canGetCategories()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Categories.ToList().Count);
            }
        }
        [TestMethod]
        public void canGetUsers()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Users.ToList().Count);
            }
        }
        public JoinUsContext GetContext()
        {
            return new JoinUsContext();
        }

        [TestInitialize]
        public void Setup()
        {
            Database.SetInitializer(new DbInitializer());
            using (JoinUsContext context = GetContext())
            {
                context.Database.Initialize(true);
            }

        }
    }
}
