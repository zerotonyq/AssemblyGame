using AssemblySystem.Command;

namespace AssemblySystem.Scheme.CommandValidators.Base
{
    public interface IValidatable
    {
        static void ValidateCommand<T1, T2>(T1 command, T2 commandSo){}
    }
}