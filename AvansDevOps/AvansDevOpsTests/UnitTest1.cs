using System;
using Xunit;

namespace AvansDevOpsTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("Hello World", AvansDevOps.Program.HelloWorld());
        }
    }
}
