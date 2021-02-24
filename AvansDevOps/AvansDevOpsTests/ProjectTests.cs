using System;
using Xunit;

namespace AvansDevOpsTests
{
    public class ProjectTests
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal("Hello World", AvansDevOps.Program.HelloWorld());
        }
    }
}
