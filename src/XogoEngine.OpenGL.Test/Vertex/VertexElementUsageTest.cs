using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    public  class VertexElementUsageTest
    {
        [Test, TestCaseSource(nameof(StaticFieldsData))]
        public void StaticFields_Return_ExpectedStrings(string expected, string actual)
        {
            expected.ShouldBe(actual);
        }

        private IEnumerable<TestCaseData> StaticFieldsData
        {
            get
            {
                yield return new TestCaseData("position", VertexElementUsage.Position);
                yield return new TestCaseData("colour", VertexElementUsage.Colour);
                yield return new TestCaseData("texCoord", VertexElementUsage.TexCoord);
            }
        }
    }
}
