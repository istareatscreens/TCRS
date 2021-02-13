using TCRS_client.Pages;
using Bunit;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace TCRS_client_test
{
    public class Tests
    {
        private Bunit.TestContext testContext;
        //private Mock<ILoginProcessor> loginProcessor;
        [SetUp]
        public void Setup()
        {
            testContext = new Bunit.TestContext();
            //   loginProcessor = new Mock<ILoginProcessor>();
        }
        [TearDown]
        public void Teardown()
        {
            testContext.Dispose();
        }

        [Test]
        public void Test1()
        {
            // testContext.Services.AddScoped(x => loginProcessor.Object);

            var component = testContext.RenderComponent<Index>();

            Assert.IsTrue(component.Markup.Contains("<h1>Hello, world!</h1>"));
        }
    }
}