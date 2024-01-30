using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClientConvertisseurV2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConvertisseurV2.ViewModels;
using ClientConvertisseurV2.Models;

namespace ClientConvertisseurV2.Services.Tests
{
    [TestClass()]
    public class WSServiceTests
    {
        [TestMethod()]
        public void ConstructeurTest()
        {
            //Arrange
            WSService service = new WSService("https://localhost:7228/api/");
            
            Assert.IsNotNull(service);
        }
        [TestMethod()]
        public void GetDevisesAsyncTest()
        {
            //Arrange
            WSService service = new WSService("https://localhost:7228/api/");
            var result = service.GetDevisesAsync("devises").Result;
            Thread.Sleep(1000);

            var expectedDevises = new List<Devise>
            {
                new Devise(1, "Dollar", 1.08),
                new Devise(2, "Franc Suisse", 1.07),
                new Devise(3, "Yen", 120)
            };


            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Devise>));
            CollectionAssert.AreEqual(expectedDevises, result);
        }
    }
}