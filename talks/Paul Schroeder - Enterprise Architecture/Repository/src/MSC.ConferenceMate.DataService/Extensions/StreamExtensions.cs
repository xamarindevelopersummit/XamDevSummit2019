using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MSC.ConferenceMate.DataService.Extensions
{
	public static class StreamExtensions
	{
		public static byte[] ToArray(this Stream s)
		{
			if (s == null)
				throw new ArgumentNullException(nameof(s));
			if (!s.CanRead)
				throw new ArgumentException("Stream cannot be read");

			MemoryStream ms = s as MemoryStream;
			if (ms != null)
				return ms.ToArray();

			long pos = s.CanSeek ? s.Position : 0L;
			if (pos != 0L)
				s.Seek(0, SeekOrigin.Begin);

			byte[] result = new byte[s.Length];
			s.Read(result, 0, result.Length);
			if (s.CanSeek)
				s.Seek(pos, SeekOrigin.Begin);
			return result;
		}
	}
}