using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GenerateReferences.Services;

namespace GenerateReferences.Tests
{
    [TestClass]
    public class ReferenceTest
    {
        [TestMethod]
        public void TestGeReference()
        {
            Service service = new Service();
            var result = service.Commands("new reference");

            bool equality = (result == "BlobClient должен быть авторизован с помощью учетных данных общего ключа для создания службы SAS") ? false : true;

            Assert.AreEqual(equality, true);
        }
    }
}
