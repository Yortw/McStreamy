using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McStreamy.Tests
{
	[TestClass]
	public class StringExtensionsTests
	{

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_ToStream_ThrowsOnNullString()
		{
			string s = null;
			s.ToStream();
		}


		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void StringExtensions_ToStream_ThrowsOnNullEncoding()
		{
			string s = "Hello world!";
			s.ToStream(null);
		}

		[TestMethod]
		public void StringExtensions_ToStream_EncodesUsingUtf8AsDefault()
		{
			string s = "Hello world!";
			using (var ms = s.ToStream())
			{
				var buffer = new byte[ms.Length];
				ms.Read(buffer, 0, buffer.Length);
				Assert.AreEqual(12, buffer.Length);
				Assert.AreEqual(s, System.Text.UTF8Encoding.UTF8.GetString(buffer));
			}
		}

		[TestMethod]
		public void StringExtensions_ToStream_EncodesUsingSpecifiedEncoding()
		{
			string s = "Hello world!";
			using (var ms = s.ToStream(System.Text.UTF32Encoding.UTF32))
			{
				var buffer = new byte[ms.Length];
				ms.Read(buffer, 0, buffer.Length);
				Assert.AreEqual(48, buffer.Length);
				Assert.AreEqual(s, System.Text.UTF32Encoding.UTF32.GetString(buffer));
			}
		}

	}
}