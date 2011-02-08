namespace LinqToImap.Tests.Linq
{
    using FakeItEasy;
    using LinqToImap.Imap;
    using LinqToImap.Imap.Commands;

    public static class CommandExtensions
    {
        public static ICommandExecutor Executor;
        public static void Returns<T>(this Command<T> command, T value)
        {
            A.CallTo(() => Executor.Execute(command))
                .Returns(value);
        }

        public static void MustHaveHappened<T>(this Command<T> command)
        {
            A.CallTo(() => Executor.Execute(command))
                .MustHaveHappened();
        }
    }
}