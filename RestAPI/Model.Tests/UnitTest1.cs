using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Linq;
namespace Model.Tests
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

        

        /*[TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void DetecteLesEditionsConcurrentes()
        {
            using (JoinUsContext contexteDeJohn = GetContext())
            {
                using (JoinUsContext contexteDeSarah = GetContext())
                {
                    var clientDeJohn = contexteDeJohn.Customers.First();
                    var clientDeSarah = contexteDeSarah.Customers.First();

                    clientDeJohn.Remark = "Hey";
                    contexteDeJohn.SaveChanges();

                    clientDeSarah.Remark = "Ho";

                    contexteDeSarah.SaveChanges();


                }
            }
        }*/
        [TestMethod]
        public void CanGetUsers()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(2, context.Users.ToList().Count);
            }
        }
        [TestMethod]
        public void CanGetEvents()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Events.ToList().Count);
            }
        }
        [TestMethod]
        public void CanGetTags()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Tags.ToList().Count);
            }
        }
        [TestMethod]
        public void CanGetCategories()
        {
            using (var context = GetContext())
            {
                Assert.AreEqual(1, context.Categories.ToList().Count);
            }
        }


    }
}
