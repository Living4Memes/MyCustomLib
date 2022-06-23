using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCustomLib.IO
{
      public class XorStream : Stream
      {
            //Класс полностью сделан хорошим человеком с CyberForum - SSTREGG https://www.cyberforum.ru/members/105416.html
            private readonly Stream _parent;
            private readonly byte _xor;

            public XorStream(Stream stream, byte xorValue)
            {
                  _parent = stream;
                  _xor = xorValue;
            }

            public override bool CanRead
            {
                  get { return _parent.CanRead; }
            }

            public override bool CanSeek
            {
                  get { return _parent.CanSeek; }
            }

            public override bool CanWrite
            {
                  get { return _parent.CanWrite; }
            }

            public override void Flush()
            {
                  _parent.Flush();
            }

            public override long Length
            {
                  get { return _parent.Length; }
            }

            public override long Position
            {
                  get { return _parent.Position; }
                  set { _parent.Position = value; }
            }

            public override int Read(byte[] buffer, int offset, int count)
            {
                  int readed = _parent.Read(buffer, offset, count);
                  for (int i = offset; i < readed; ++i)
                        buffer[i] ^= _xor;
                  return readed;
            }

            public override long Seek(long offset, SeekOrigin origin)
            {
                  return _parent.Seek(offset, origin);
            }

            public override void SetLength(long value)
            {
                  _parent.SetLength(value);
            }

            public override void Write(byte[] buffer, int offset, int count)
            {
                  for (int i = offset; i < count; ++i)
                        buffer[i] ^= _xor;
                  _parent.Write(buffer, offset, count);
            }

            public override void Close()
            {
                  _parent.Close();
            }
      }
}
