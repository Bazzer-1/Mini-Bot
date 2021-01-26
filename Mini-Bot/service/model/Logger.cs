using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mini_Bot.service.model
{
    public static class Logger
    {
        public static StringBuilder LogString = new StringBuilder();
        public static void WriteLine(string str)
        {
            Console.WriteLine(str);
            LogString.Append(str).Append(Environment.NewLine);
        }
        public static void Write(string str)
        {
            Console.Write(str);
            LogString.Append(str);

        }
        public static void SaveLog(bool Append = false, string Path = "./Log.txt")
        {
            if (LogString != null && LogString.Length > 0)
            {
                if (Append)
                {
                    using (StreamWriter file = System.IO.File.AppendText(Path))
                    {
                        file.Write(LogString.ToString());
                        file.Close();
                        file.Dispose();
                    }
                }
                else
                {
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(Path))
                    {
                        file.Write(LogString.ToString());
                        file.Close();
                        file.Dispose();
                    }
                }
            }
        }
    }

    public class MultiTextWriter
    : System.IO.TextWriter
    {

        protected System.Text.Encoding m_encoding;
        protected System.Collections.Generic.IEnumerable<System.IO.TextWriter> m_writers;


        public override System.Text.Encoding Encoding => this.m_encoding;


        public override System.IFormatProvider FormatProvider
        {
            get
            {
                return base.FormatProvider;
            }
        }


        public MultiTextWriter(System.Collections.Generic.IEnumerable<System.IO.TextWriter> textWriters, System.Text.Encoding encoding)
        {
            this.m_writers = textWriters;
            this.m_encoding = encoding;
        }


        public MultiTextWriter(System.Collections.Generic.IEnumerable<System.IO.TextWriter> textWriters)
            : this(textWriters, textWriters.GetEnumerator().Current.Encoding)
        { }


        public MultiTextWriter(System.Text.Encoding enc, params System.IO.TextWriter[] textWriters)
            : this((System.Collections.Generic.IEnumerable<System.IO.TextWriter>)textWriters, enc)
        { }


        public MultiTextWriter(params System.IO.TextWriter[] textWriters)
            : this((System.Collections.Generic.IEnumerable<System.IO.TextWriter>)textWriters)
        { }


        public override void Flush()
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                thisWriter.Flush();
            }
        }

        public async override System.Threading.Tasks.Task FlushAsync()
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                await thisWriter.FlushAsync();
            }

            await System.Threading.Tasks.Task.CompletedTask;
        }


        public override void Write(char[] buffer, int index, int count)
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                thisWriter.Write(buffer, index, count);
            }
        }


        public override void Write(System.ReadOnlySpan<char> buffer)
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                thisWriter.Write(buffer);
            }
        }


        public async override System.Threading.Tasks.Task WriteAsync(char[] buffer, int index, int count)
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                await thisWriter.WriteAsync(buffer, index, count);
            }

            await System.Threading.Tasks.Task.CompletedTask;
        }


        public async override System.Threading.Tasks.Task WriteAsync(System.ReadOnlyMemory<char> buffer, System.Threading.CancellationToken cancellationToken = default)
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                await thisWriter.WriteAsync(buffer, cancellationToken);
            }

            await System.Threading.Tasks.Task.CompletedTask;
        }


        protected override void Dispose(bool disposing)
        {
            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                thisWriter.Dispose();
            }
        }

        public override void Close()
        {

            foreach (System.IO.TextWriter thisWriter in this.m_writers)
            {
                thisWriter.Close();
            }

        } // End Sub Close 


    } // End Class MultiTextWriter 

    public class ConsoleOutputMultiplexer
        : System.IDisposable
    {

        protected System.IO.TextWriter m_oldOut;
        protected System.IO.FileStream m_logStream;
        protected System.IO.StreamWriter m_logWriter;

        protected MultiTextWriter m_multiPlexer;

        private async Task<int> createReadAndWriteAndGetLogCounter(FileInfo counterFile)
        {
            if (!counterFile.Exists)
            {
                //Create a file to write to.
                using (StreamWriter sw = counterFile.CreateText())
                {
                    sw.WriteLine(1);
                }
            }

            int logCounter = 0;
            try
            {
                using (StreamReader streamReader = new StreamReader("counter.txt"))
                {
                    using (StreamWriter streamWriter = new StreamWriter("counter.txt"))
                    {
                        logCounter = int.Parse(await streamReader.ReadToEndAsync() + 1);
                        await streamWriter.WriteLineAsync(logCounter.ToString());
                    }
                }      
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return logCounter;
        }

        public static String fileName;

        public ConsoleOutputMultiplexer()
        {
            this.m_oldOut = System.Console.Out;

            FileInfo counterFile = new FileInfo("counter.txt");

            fileName = $"log{DateTime.Now.ToShortDateString()}[{createReadAndWriteAndGetLogCounter(counterFile)}].txt";
            try
            {
                this.m_logStream = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                this.m_logWriter = new System.IO.StreamWriter(this.m_logStream);
                this.m_multiPlexer = new MultiTextWriter(this.m_oldOut.Encoding, this.m_oldOut, this.m_logWriter);

                System.Console.SetOut(this.m_multiPlexer);
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine($"Cannot open {fileName} for writing");
                System.Console.WriteLine(e.Message);
                return;
            }

        } // End Constructor 


        void System.IDisposable.Dispose()
        {
            System.Console.SetOut(this.m_oldOut);

            if (this.m_multiPlexer != null)
            {
                this.m_multiPlexer.Flush();
                if (this.m_logStream != null)
                    this.m_logStream.Flush();

                this.m_multiPlexer.Close();
            }

            if (this.m_logStream != null)
                this.m_logStream.Close();
        } // End Sub Dispose 


    } // End Class ConsoleOutputMultiplexer 
}
