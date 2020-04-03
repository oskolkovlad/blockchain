using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlockChain.Tests
{
    [TestClass()]
    public class ChainTests
    {
        [TestMethod()]
        public void ChainTest()
        {
            var chain = new Chain();
            chain.Add("bla bla bla", "Vlad");

            Assert.AreEqual(chain.Blocks.Count, 2);
            Assert.AreEqual(chain.Last.Data, "bla bla bla");
            Assert.AreEqual(chain.Last.User, "Vlad");
        }
    }
}