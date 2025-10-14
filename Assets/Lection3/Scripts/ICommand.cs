/// <summary>
/// Command interface
/// </summary>
public interface ICommand {
    void Execute();
    void Undo();
}
