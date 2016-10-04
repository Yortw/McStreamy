using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McStreamy.Tests
{
	/// <exclude />
	public class MockStream : System.IO.Stream
	{
		private long _Length;
		private int _ReadTimeout;
		private int _WriteTimeout;

		public override int ReadTimeout
		{
			get
			{
				return _ReadTimeout;
			}

			set
			{
				_ReadTimeout = value;
			}
		}

		public override int WriteTimeout
		{
			get
			{
				return _WriteTimeout;
			}

			set
			{
				_WriteTimeout = value;
			}
		}

		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		public override long Length
		{
			get
			{
				return _Length;
			}
		}

		public override long Position
		{
			get
			{
				return _Length;
			}

			set
			{
				_Length = value;
			}
		}

		public override void Flush()
		{
			FlushCalled = true;
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			return count;
		}

		public override long Seek(long offset, SeekOrigin origin)
		{
			if (origin == SeekOrigin.Begin)
				Position = offset;
			else if (origin == SeekOrigin.End)
				Position = Math.Max(0, this.Length - offset);
			else
				Position += offset;

			return Position;
		}

		public override void SetLength(long value)
		{
			_Length = value;
		}

		public override void Write(byte[] buffer, int offset, int count)
		{
		}

		public bool FlushCalled { get; set; }
	}
}