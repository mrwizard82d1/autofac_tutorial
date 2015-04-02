using System;
using Autofac;
using Autofac.Core;

namespace DemoApp
{
    class Program
    {
        static void Main()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConsoleOutput>().As<IOutput>();
            builder.RegisterType<TodayWriter>().As<IDateWriter>();
            Container = builder.Build();

            // The WriteDate method is where we'll make use of our
            // dependency injection. We'll define that in a bit.
            WriteDate();

            // Ensure the created window **does not** disappear when
            // run from Visual Studio.
            Console.WriteLine("Press <ENTER> when finished.");
            Console.ReadLine();
        }

        public static IContainer Container { get; set; }

        private static void WriteDate()
        {
            // Create a lifetime scope (to avoid Autofac holding references
            // to objects for the lifetime of the container (that is, for
            // the duration of the entire application).
            using (var lifetimeScope = Container.BeginLifetimeScope())
            {
                // Within this scope, the container resolves the concrete
                // implementation of IDateWriter. Because it is resolved
                // within a specific scope, the container itself **does
                // not** hold onto the created reference throughout the 
                // durationof the application.
                var writer = lifetimeScope.Resolve<IDateWriter>();

                writer.WriteDate();
            }
        }

    }
}
