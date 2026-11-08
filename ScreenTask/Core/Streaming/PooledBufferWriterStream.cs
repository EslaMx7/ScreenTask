using System;
using System.Buffers;
using System.IO;

namespace ScreenTask.Core.Streaming
{
    /// <summary>
    /// A high-performance, seekable Stream backed by ArrayPool&lt;byte&gt;.Shared.
    /// Eliminates MemoryStream and byte[] allocations during frequent JPEG frame encoding.
    /// </summary>
    public sealed class PooledBufferWriterStream : Stream
    {
        private byte[] _buffer;
        private int _position;
        private int _length;
        private bool _isDisposed;

        public PooledBufferWriterStream(int initialCapacity = 65536)
        {
            _buffer = ArrayPool<byte>.Shared.Rent(Math.Max(4096, initialCapacity));
            _position = 0;
            _length = 0;
        }

        public byte[] Buffer => _buffer;

        public ArraySegment<byte> WrittenSegment => new ArraySegment<byte>(_buffer, 0, _length);

        public ReadOnlySpan<byte> WrittenSpan => new ReadOnlySpan<byte>(_buffer, 0, _length);

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => true;
        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set
            {
                if (value < 0 || value > int.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value));
                _position = (int)value;
                if (_position > _length)
                    _length = _position;
            }
        }

        public override void Flush() { }

        public override int Read(byte[] buffer, int offset, int count)
        {
            ValidateNotDisposed();
            int available = _length - _position;
            if (available <= 0) return 0;
            int toRead = Math.Min(count, available);
            System.Buffer.BlockCopy(_buffer, _position, buffer, offset, toRead);
            _position += toRead;
            return toRead;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            ValidateNotDisposed();
            long newPos = origin switch
            {
                SeekOrigin.Begin => offset,
                SeekOrigin.Current => _position + offset,
                SeekOrigin.End => _length + offset,
                _ => throw new ArgumentOutOfRangeException(nameof(origin))
            };

            if (newPos < 0 || newPos > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(offset));

            _position = (int)newPos;
            if (_position > _length)
                _length = _position;

            return _position;
        }

        public override void SetLength(long value)
        {
            ValidateNotDisposed();
            if (value < 0 || value > int.MaxValue)
                throw new ArgumentOutOfRangeException(nameof(value));

            EnsureCapacity((int)value);
            _length = (int)value;
            if (_position > _length)
                _position = _length;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            ValidateNotDisposed();
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0 || count < 0 || offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));

            EnsureCapacity(_position + count);
            System.Buffer.BlockCopy(buffer, offset, _buffer, _position, count);
            _position += count;
            if (_position > _length)
                _length = _position;
        }

        public override void Write(ReadOnlySpan<byte> source)
        {
            ValidateNotDisposed();
            EnsureCapacity(_position + source.Length);
            source.CopyTo(_buffer.AsSpan(_position));
            _position += source.Length;
            if (_position > _length)
                _length = _position;
        }

        public override void WriteByte(byte value)
        {
            ValidateNotDisposed();
            EnsureCapacity(_position + 1);
            _buffer[_position++] = value;
            if (_position > _length)
                _length = _position;
        }

        private void EnsureCapacity(int requiredCapacity)
        {
            if (requiredCapacity <= _buffer.Length) return;

            int newCapacity = Math.Max(requiredCapacity, _buffer.Length * 2);
            var newBuffer = ArrayPool<byte>.Shared.Rent(newCapacity);
            System.Buffer.BlockCopy(_buffer, 0, newBuffer, 0, _length);
            ArrayPool<byte>.Shared.Return(_buffer);
            _buffer = newBuffer;
        }

        public void Reset(int startOffset = 0)
        {
            _position = startOffset;
            _length = startOffset;
        }

        private void ValidateNotDisposed()
        {
            ObjectDisposedException.ThrowIf(_isDisposed, this);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (_buffer != null)
                {
                    ArrayPool<byte>.Shared.Return(_buffer);
                    _buffer = null!;
                }
            }
            base.Dispose(disposing);
        }
    }
}
