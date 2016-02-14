using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderTest
    {
        private Shader shader;
        private Mock<IShaderAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IShaderAdapter>();
            adapter.Setup(a => a.CreateShader(It.IsAny<ShaderType>()))
                   .Returns(1);

            shader = new Shader(adapter.Object, ShaderType.VertexShader);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            shader.ShouldSatisfyAllConditions(
                () => shader.Handle.ShouldBe(1),
                () => shader.ShaderType.ShouldBe(ShaderType.VertexShader),
                () => shader.IsDisposed.ShouldBeFalse()
            );
        }

        [Test]
        public void Adapter_CreateShader_IsInvokedOnce_OnConstruction()
        {
            adapter.Verify(a => a.CreateShader(shader.ShaderType), Times.Once);
        }

        [Test, TestCaseSource(nameof(InvalidSourceStrings))]
        public void Load_Throws_CustomException_OnInvalidSourceStrings(string invalidSource)
        {
            Assert.Throws<ArgumentException>(() => shader.Load(invalidSource));
        }

        private IEnumerable<TestCaseData> InvalidSourceStrings
        {
            get
            {
                yield return new TestCaseData(string.Empty);
                yield return new TestCaseData(null);
                yield return new TestCaseData("  ");
            }
        }

        [Test]
        public void Shader_ShouldBeDisposed_AfterDisposal()
        {
            shader.Dispose();
            shader.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void Adapter_ShouldDeleteHandleOnce_OnDisposal()
        {
            shader.Dispose();
            shader.Dispose();
            adapter.Verify(a => a.DeleteShader(shader.Handle), Times.Once);
        }

        [Test]
        public void ObjectEquals_ReturnsFalse_ForNullComparison()
        {
            shader.Equals(null).ShouldBeFalse();
        }

        [Test]
        public void ObjectEquals_ReturnsFalse_ForUnequalType()
        {
            shader.Equals(new object()).ShouldBeFalse();
        }

        [Test]
        public void ObjectEquals_ReturnsFalse_ForUnequalShaderType()
        {
            var other = new Shader(adapter.Object, ShaderType.FragmentShader);
            shader.Equals(other).ShouldBeFalse();
        }

        [Test]
        public void ObjectEquals_ReturnsTrue_ForEqualShader()
        {
            object other = new Shader(adapter.Object, ShaderType.VertexShader);
            shader.Equals(other).ShouldBeTrue();
        }

        [Test]
        public void TypeEquals_ReturnsFalse_ForNullComparison()
        {
            Shader other = null;
            shader.Equals(other).ShouldBeFalse();
        }

        [Test]
        public void TypeEquals_ReturnsFalse_ForUnequalShaders()
        {
            Shader other = new Shader(adapter.Object, ShaderType.ComputeShader);
            shader.Equals(other).ShouldBeFalse();
        }

        [Test]
        public void TypeEquals_ReturnsTrue_ForEqualInstance()
        {
            Shader other = new Shader(adapter.Object, ShaderType.VertexShader);
            shader.Equals(other).ShouldBeTrue();
        }

        [Test]
        public void HashCodes_AreEqual_ForIdenticalShaders()
        {
            Shader other = new Shader(adapter.Object, ShaderType.VertexShader);

            shader.ShouldSatisfyAllConditions(
                () => shader.Equals(other).ShouldBeTrue(),
                () => shader.GetHashCode().ShouldBe(other.GetHashCode())
            );
        }

        [Test]
        public void HashCodes_AreNotEqual_ForShaderWithUnidenticalHandles()
        {
            var otherAdapter = new Mock<IShaderAdapter>();
            otherAdapter.Setup(a => a.CreateShader(ShaderType.VertexShader)).Returns(2);
            Shader other = new Shader(otherAdapter.Object, ShaderType.VertexShader);

            shader.ShouldSatisfyAllConditions(
                () => shader.Equals(other).ShouldBeFalse(),
                () => shader.GetHashCode().ShouldNotBe(other.GetHashCode())
            );
        }

        [Test]
        public void Shader_ToString_ReturnsExpectedString()
        {
            string expected = string.Format(
                "[Shader: Handle={0}, ShaderType={1}, IsDisposed={2}]",
                shader.Handle,
                shader.ShaderType,
                shader.IsDisposed
            );
            expected.ShouldBe(shader.ToString(), Case.Sensitive);
        }
    }
}
