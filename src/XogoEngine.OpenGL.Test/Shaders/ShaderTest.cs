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
            adapter.Setup(a => a.GetShaderStatus(It.IsAny<int>(), ShaderParameter.CompileStatus))
                   .Returns(true);

            shader = new Shader(adapter.Object, ShaderType.VertexShader);
        }

        [TearDown]
        public void TearDown()
        {
            shader.Dispose();
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

        [Test]
        public void Load_Throws_ObjectDisposedException_OnDisposedShader()
        {
            shader.Dispose();
            Assert.Throws<ObjectDisposedException>(() => shader.Load("source"));
        }

        [Test, TestCaseSource(nameof(InvalidSourceStrings))]
        public void Load_Throws_ArgumentException_OnInvalidSourceStrings(string invalidSource)
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
        public void Adapter_ShaderSource_ShouldBeInvoked_OnLoad()
        {
            shader.Load("source");
            adapter.Verify(a => a.ShaderSource(shader.Handle, "source"), Times.Once);
        }

        [Test]
        public void Adapter_CompileShader_ShouldBeInvoked_OnLoad()
        {
            shader.Load("source");
            adapter.Verify(a => a.CompileShader(shader.Handle), Times.Once);
        }

        [Test]
        public void Load_ShouldThrowShaderCompilationException_OnFailedCompilation()
        {
            Action load = () => shader.Load("source");
            adapter.Setup(a => a.GetShaderStatus(shader.Handle, ShaderParameter.CompileStatus))
                   .Returns(false);
            adapter.Setup(a => a.GetShaderInfoLog(shader.Handle))
                   .Returns("error:syntax error");

            string expectedMessage = string.Format(
                "Failed to compile shader Id : {0}, Reason : {1}",
                shader.Handle,
                "error:syntax error"
            );

            load.ShouldThrow<ShaderCompilationException>().Message.ShouldContain(expectedMessage);
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
            using (IDisposable other = new Shader(adapter.Object, ShaderType.FragmentShader))
            {
                shader.Equals(other).ShouldBeFalse();
            }
        }

        [Test]
        public void ObjectEquals_ReturnsTrue_ForEqualShader()
        {
            using (IDisposable other = new Shader(adapter.Object, ShaderType.VertexShader))
            {
                shader.Equals(other).ShouldBeTrue();
            }
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
            using (var other = new Shader(adapter.Object, ShaderType.ComputeShader))
            {
                shader.Equals(other).ShouldBeFalse();
            }
        }

        [Test]
        public void TypeEquals_ReturnsTrue_ForEqualInstance()
        {
            using (var other = new Shader(adapter.Object, ShaderType.VertexShader))
            {
                shader.Equals(other).ShouldBeTrue();
            }
        }

        [Test]
        public void HashCodes_AreEqual_ForIdenticalShaders()
        {
            using (var other = new Shader(adapter.Object, ShaderType.VertexShader))
            {
                shader.ShouldSatisfyAllConditions(
                    () => shader.Equals(other).ShouldBeTrue(),
                    () => shader.GetHashCode().ShouldBe(other.GetHashCode())
                );
            }
        }

        [Test]
        public void HashCodes_AreNotEqual_ForShaderWithUnidenticalHandles()
        {
            var otherAdapter = new Mock<IShaderAdapter>();
            otherAdapter.Setup(a => a.CreateShader(ShaderType.VertexShader)).Returns(2);

            using (var other = new Shader(otherAdapter.Object, ShaderType.VertexShader))
            {
                shader.ShouldSatisfyAllConditions(
                    () => shader.Equals(other).ShouldBeFalse(),
                    () => shader.GetHashCode().ShouldNotBe(other.GetHashCode())
                );
            }
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
            shader.ToString().ShouldBe(expected);
        }
    }
}
