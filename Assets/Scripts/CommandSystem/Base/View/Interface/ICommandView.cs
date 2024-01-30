using System;

namespace AssemblySystem.Views.Interface
{
    public interface ICommandView
    {
        void Subscribe(Action<Command.Command> subscriber);
        void Unsubscribe(Action<Command.Command> subscriber);
    }
}