using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McStreamy.Tests
{
	[TestClass]
	public class ByteArrayExtensionTests
	{

		private byte[] TestStreamContents = System.Text.UTF8Encoding.UTF8.GetBytes("Some test content for streams.");

		[TestMethod]
		public void ByteArrayExtensions_ToStream_ConstructsSystem()
		{
			using (var stream = TestStreamContents.ToStream())
			{
				Assert.AreEqual(TestStreamContents.Length, stream.Length);
			}
		}

		[ExpectedException(typeof(ArgumentNullException))]
		[TestMethod]
		public void ByteArrayExtensions_ToStream_ThrowsOnNullStream()
		{
			byte[] buffer = null;
			buffer.ToStream();
		}

	}
}