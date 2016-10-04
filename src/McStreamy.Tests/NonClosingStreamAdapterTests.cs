using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using McStreamy;
using System.Threading.Tasks;
using System.Text;

namespace McStreamy.Tests
{
	[TestClass]
	public class NonClosingStreamAdapterTests
	{

		#region Constructor Tests

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void NonClosingStreamAdapter_Constructor_ThrowsOnNullStream()
		{
			var stream = new NonClosingStreamAdapter(null);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Constructor_ConstructsWithValidStream()
		{
			var stream = new NonClosingStreamAdapter(new System.IO.MemoryStream());
		}

		#endregion

		[TestMethod]
		public void NonClosingStreamAdapter_Dispose_DoesNotCloseInnerStream()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			adapter.Dispose();

			Assert.AreEqual(stream.Length, adapter.Length);
			byte[] data = new byte[stream.Length];
			stream.Read(data, 0, (int)stream.Length);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_CanRead()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			Assert.AreEqual(stream.CanRead, adapter.CanRead);
		}

		public void NonClosingStreamAdapter_CanSeek()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			Assert.AreEqual(stream.CanSeek, adapter.CanSeek);
		}

		public void NonClosingStreamAdapter_CanWrite()
		{
			System.IO.Stream stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			Assert.AreEqual(stream.CanWrite, adapter.CanWrite);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_CanTimeout()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			Assert.AreEqual(stream.CanTimeout, adapter.CanTimeout);
		}

		[TestMethod]
		public async Task NonClosingStreamAdapter_CopyToAsync()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			var outStream = new System.IO.MemoryStream();
			await adapter.CopyToAsync(outStream);
			Assert.AreEqual(stream.Length, outStream.Length);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Length()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			Assert.AreEqual(stream.Length, adapter.Length);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Position()
		{
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes("Some test content"));
			var adapter = new NonClosingStreamAdapter(stream);
			stream.Seek(10, System.IO.SeekOrigin.Begin);
			Assert.AreEqual(stream.Position, adapter.Position);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Read()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			byte[] buffer = new byte[stream.Length];
			adapter.Read(buffer, 0, (int)stream.Length);
			Assert.AreEqual(testContent, System.Text.UTF8Encoding.UTF8.GetString(buffer));
		}

		[TestMethod]
		public async Task NonClosingStreamAdapter_ReadAsync()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			byte[] buffer = new byte[stream.Length];
			await adapter.ReadAsync(buffer, 0, (int)stream.Length);
			Assert.AreEqual(testContent, System.Text.UTF8Encoding.UTF8.GetString(buffer));
		}

		[TestMethod]
		public void NonClosingStreamAdapter_ReadByte()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			var sb = new StringBuilder();
			while (adapter.Position < adapter.Length)
			{
				sb.Append((char)adapter.ReadByte());
			}
			Assert.AreEqual(testContent, sb.ToString());
		}

		[TestMethod]
		public void NonClosingStreamAdapter_ReadTimeout()
		{
			var stream = new MockStream();
			var adapter = new NonClosingStreamAdapter(stream);
			stream.ReadTimeout = 1;
			Assert.AreEqual(stream.ReadTimeout, adapter.ReadTimeout);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Seek()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			adapter.Seek(10, System.IO.SeekOrigin.Begin);
			Assert.AreEqual(10, stream.Position);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_SetLength()
		{
			var stream = new MockStream();
			var adapter = new NonClosingStreamAdapter(stream);
			adapter.SetLength(100);
			Assert.AreEqual(100, stream.Length);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Write()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			var buffer = System.Text.UTF8Encoding.UTF8.GetBytes("Some test content");
			adapter.Write(buffer, 0, buffer.Length);
			Assert.AreEqual(buffer.Length, stream.Length);
		}

		[TestMethod]
		public async Task NonClosingStreamAdapter_WriteAsync()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			var buffer = System.Text.UTF8Encoding.UTF8.GetBytes("Some test content");
			await adapter.WriteAsync(buffer, 0, buffer.Length);
			Assert.AreEqual(testContent.Length, stream.Length);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_WriteByte()
		{
			var testContent = "Some test content";
			var stream = new System.IO.MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(testContent));
			var adapter = new NonClosingStreamAdapter(stream);
			var buffer = System.Text.UTF8Encoding.UTF8.GetBytes("Some test content");
			foreach (var b in buffer)
			{
				adapter.WriteByte(b);
			}
			adapter.Seek(0, System.IO.SeekOrigin.Begin);
			Assert.AreEqual(testContent, new System.IO.StreamReader(adapter).ReadToEnd());
		}

		[TestMethod]
		public void NonClosingStreamAdapter_WriteTimeout()
		{
			var stream = new MockStream();
			var adapter = new NonClosingStreamAdapter(stream);
			adapter.WriteTimeout = 1;
			Assert.AreEqual(1, adapter.WriteTimeout);
		}

		[TestMethod]
		public void NonClosingStreamAdapter_Flush()
		{
			var stream = new MockStream();
			var adapter = new NonClosingStreamAdapter(stream);
			adapter.Flush();
			Assert.AreEqual(true, stream.FlushCalled);
		}
	}
}