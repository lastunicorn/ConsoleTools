using System;
using System.IO;
using Xunit;

namespace TableStreaming.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void WhenWritingOneBorder_ThenOutputIsEmpty()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                TableTextStreamWriter tableTextStreamWriter = new TableTextStreamWriter(ms);

                BorderX borderX = new BorderX();
                tableTextStreamWriter.EnqueueHorizontalBorder(borderX);
                tableTextStreamWriter.EndTable();

                string expected = string.Empty;
                AssertOutput(ms, expected);
            }
        }
        
        [Fact]
        public void WhenWritingOneBorderAndOneRow_ThenOutputContainsBorderRowBorder()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                TableTextStreamWriter tableTextStreamWriter = new TableTextStreamWriter(ms);

                BorderX borderX = new BorderX();
                tableTextStreamWriter.EnqueueHorizontalBorder(borderX);
                
                RowX rowX = new RowX();
                tableTextStreamWriter.WriteRow(rowX, borderX);
                
                tableTextStreamWriter.EndTable();

                string expected = string.Empty;
                AssertOutput(ms, expected);
            }
        }

        private static void AssertOutput(Stream stream, string expected)
        {
            stream.Position = 0;

            StreamReader sr = new StreamReader(stream);
            string actual = sr.ReadToEnd();

            Assert.Equal(expected, actual);
        }
    }

    public class RowX
    {
    }

    public class BorderX
    {
    }

    public class TableTextStreamWriter
    {
        private readonly MemoryStream memoryStream;
        private BorderX borderX;

        public TableTextStreamWriter(MemoryStream memoryStream)
        {
            this.memoryStream = memoryStream ?? throw new ArgumentNullException(nameof(memoryStream));
        }

        public void EnqueueHorizontalBorder(BorderX borderX)
        {
            this.borderX = borderX;
        }

        public void EndTable()
        {
        }

        public void WriteRow(RowX rowX)
        {
        }
    }
}
