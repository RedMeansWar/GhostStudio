﻿using System;
using System.IO;

namespace Endian
{
    public class EndianIO : IDisposable
    {
        public bool disposed = false;
        private readonly EndianStyle _endianStyle;
        private readonly string _filePath;
        private readonly bool _isFile;

        public EndianIO(Stream Stream, EndianStyle EndianStyle)
        {
            _filePath = string.Empty;
            _endianStyle = EndianStyle.LittleEndian;
            _endianStyle = EndianStyle;
            this.Stream = Stream;
            _isFile = false;
            Open();
        }

        public EndianIO(string FilePath, EndianStyle EndianStyle)
        {
            _filePath = string.Empty;
            _endianStyle = EndianStyle.LittleEndian;
            _endianStyle = EndianStyle;
            _filePath = FilePath;
            _isFile = true;
            Open();
        }

        public EndianIO(byte[] Buffer, EndianStyle EndianStyle)
        {
            _filePath = string.Empty;
            _endianStyle = EndianStyle.LittleEndian;
            _endianStyle = EndianStyle;
            Stream = new MemoryStream(Buffer);
            _isFile = false;
            Open();
        }

        public EndianIO(string FilePath, EndianStyle EndianStyle, FileMode FileMode)
        {
            _filePath = string.Empty;
            _endianStyle = EndianStyle.LittleEndian;
            _endianStyle = EndianStyle;
            _filePath = FilePath;
            _isFile = true;
            Open(FileMode);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (Opened)
                    {
                        Stream.Close();
                        Reader.Close();
                        Writer.Close();
                        Opened = false;
                    }
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void Close()
        {
            if (Opened)
            {
                Stream.Close();
                Reader.Close();
                Writer.Close();
                Opened = false;
            }
        }

        public void Open()
        {
            Open(FileMode.Open);
        }

        public void Open(FileMode FileMode)
        {
            if (!Opened)
            {
                if (_isFile)
                    Stream = new FileStream(_filePath, FileMode, FileAccess.ReadWrite);
                Reader = new EndianReader(Stream, _endianStyle);
                Writer = new EndianWriter(Stream, _endianStyle);
                Opened = true;
            }
        }

        public byte[] ToArray()
        {
            return ((MemoryStream)Stream).ToArray();
        }

        public bool Opened { get; set; }

        public long Position
        {
            get { return Stream.Position; }
            set { Stream.Position = value; }
        }

        public EndianReader Reader { get; set; }

        public Stream Stream { get; set; }

        public EndianWriter Writer { get; set; }
    }
}
