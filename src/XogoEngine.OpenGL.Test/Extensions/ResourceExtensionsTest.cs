using NUnit.Framework;
using System;
using System.Collections.Generic;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Test.Extensions
{
    [TestFixture]
    internal sealed class ResourceExtensionsTest
    {
        public void ThrowIfDisposed_Throws_OnDisposedResource()
        {
            var resource = new TestResource() { IsDisposed = true };
            Assert.Throws<ObjectDisposedException>(() => resource.ThrowIfDisposed());
        }

        [TestCaseSource(nameof(ValidResources))]
        public void ThrowIfDisposed_DoesNotThrow_ForValidResources(IResource<int> resource)
        {
            Assert.DoesNotThrow(() => resource.ThrowIfDisposed());
        }

        private IEnumerable<TestCaseData> ValidResources
        {
            get
            {
                yield return new TestCaseData(null);
                yield return new TestCaseData(new TestResource() { IsDisposed = false });
            }
        }

        private class TestResource : IResource<int>
        {
            public int Handle { get; set; }
            public bool IsDisposed { get; set; }

            public void Dispose() { }
        }
    }
}
