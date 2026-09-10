using System;

namespace ScreenTask.Core.Streaming
{
    public class FrameData
    {
        public byte[] Bytes { get; set; } = Array.Empty<byte>();
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public long Timestamp { get; set; }
        public long FrameNumber { get; set; }
    }
}
