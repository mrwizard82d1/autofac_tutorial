using System;

namespace DemoApp
{
    /// <summary>
    /// Specifices the services offered by implementers.
    /// </summary>
    /// <remarks>
    /// This interface helps decouple the concept of "writing output" from 
    /// the <see cref="Console"/> class. We don't really care how the write
    /// operation works, just that we can write some content.
    /// </remarks>
    public interface IOutput
    {
        void Write(string content);
    }

    /// <summary>
    /// Implements the <see cref="IOutput"/> interface.
    /// </summary>
    /// <remarks>
    /// This concrete implementation of the <see cref="IOutput"/> interface
    /// defines how we actually write to the <see cref="Console"/>. We
    /// could define similar classes like 'DebugOutput' or 'Trace...Output'
    /// that write to other streams.
    /// </remarks>
    public class ConsoleOutput : IOutput
    {
        public void Write(string content)
        {
            Console.WriteLine(content);
        }
    }

    /// <summary>
    /// Specifies the services needed to write a date.
    /// </summary>
    /// <remarks>
    /// this interface decouples the notion of writing dates from the 
    /// actual mechanism that performs the writing. Similar to IOutput, 
    /// the concrete process is abstracted behind an interface.
    /// </remarks>
    public interface IDateWriter
    {
        void WriteDate();
    }

    /// <summary>
    /// Writes today's date (in short format).
    /// </summary>
    /// <remarks>
    /// This class is where everything comes together. The constructor 
    /// takes an argument of type <see cref="IOutput"/>. This allows to 
    /// write to any "sink" depending on the concrete implementation 
    /// passed to us at run-time. Further, it implements 
    /// <see cref="IDateWriter"/> in such a way that today's date is 
    /// written out. A different implementation could write either a 
    /// different date or use a different format.
    /// </remarks>
    public class TodayWriter : IDateWriter
    {
        private readonly IOutput _sink;

        public TodayWriter(IOutput sink)
        {
            _sink = sink;
        }

        public void WriteDate()
        {
            _sink.Write(DateTime.Today.ToShortDateString());
        }
    }
}
