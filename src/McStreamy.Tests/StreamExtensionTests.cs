using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McStreamy;

namespace McStreamy.Tests
{
	[TestClass]
	public class StreamExtensionTests
	{
		private byte[] TestStreamContents = System.Text.UTF8Encoding.UTF8.GetBytes("Some test content for streams.");

		#region ReadAllBytes 

		[TestMethod]
		public void StreamExtension_ReadAllBytes_ReadsContent()
		{
			using (var stream = TestStreamContents.ToStream())
			{
				var bytes = stream.ReadAllBytes();
				Assert.IsNotNull(bytes);
				AssertByteArraysMatch(TestStreamContents, bytes);
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public void StreamExtension_ReadAllBytes_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			stream.ReadAllBytes();
		}

		[TestMethod]
		public async Task StreamExtension_ReadAllBytesAsync_ReadsContent()
		{
			using (var stream = TestStreamContents.ToStream())
			{
				var bytes = await stream.ReadAllBytesAsync();
				Assert.IsNotNull(bytes);
				AssertByteArraysMatch(TestStreamContents, bytes);
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public async Task StreamExtension_ReadAllBytesAsync_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			await stream.ReadAllBytesAsync();
		}

		#endregion

		#region WriteAllBytes

		[TestMethod]
		public void StreamExtension_WriteAllBytes_WritesContentCorrectly()
		{
			using (var stream = new System.IO.MemoryStream())
			{
				stream.WriteAllBytes(TestStreamContents);
				AssertByteArraysMatch(TestStreamContents, stream.ToArray());
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public void StreamExtension_WriteAllBytes_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			stream.WriteAllBytes(TestStreamContents);
		}
		[TestMethod]
		public async Task StreamExtension_WriteAllBytesAsync_WritesContentCorrectly()
		{
			using (var stream = new System.IO.MemoryStream())
			{
				await stream.WriteAllBytesAsync(TestStreamContents);
				AssertByteArraysMatch(TestStreamContents, stream.ToArray());
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public async Task StreamExtension_WriteAllBytesAsync_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			await stream.WriteAllBytesAsync(TestStreamContents);
		}

		#endregion

		#region ReadAsString

		[TestMethod]
		public void StreamExtension_ReadAsString_ReadsContent()
		{
			using (var stream = TestStreamContents.ToStream())
			{
				var result = stream.ReadAsString();
				Assert.AreEqual(System.Text.UTF8Encoding.UTF8.GetString(TestStreamContents), result);
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public void StreamExtension_ReadAsString_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			stream.ReadAsString();
		}

		[TestMethod]
		public async Task StreamExtension_ReadAsStringAsync_ReadsContent()
		{
			using (var stream = TestStreamContents.ToStream())
			{
				var result = await stream.ReadAsStringAsync();
				Assert.AreEqual(System.Text.UTF8Encoding.UTF8.GetString(TestStreamContents), result);
			}
		}

		[ExpectedException(typeof(System.ArgumentNullException))]
		[TestMethod]
		public async Task StreamExtension_ReadAsStringAsync_ThrowsOnNullStream()
		{
			System.IO.Stream stream = null;
			await stream.ReadAsStringAsync();
		}

		#endregion

		private void AssertByteArraysMatch(byte[] expectedBytes, byte[] actualBytes)
		{
			Assert.AreEqual(expectedBytes.Length, actualBytes.Length);
			for (int cnt = 0; cnt < expectedBytes.Length; cnt++)
			{
				Assert.AreEqual(expectedBytes[cnt], actualBytes[cnt]);
			}
		}

	}
}