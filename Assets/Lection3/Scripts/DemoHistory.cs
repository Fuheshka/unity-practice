using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Show demo history
/// </summary>
public class DemoHistory : MonoBehaviour {

    /// <summary>
    /// Cached waiter
    /// </summary>
    static readonly WaitForSeconds _waiter = new WaitForSeconds(0.01f);

    /// <summary>
    /// Counter text
    /// </summary>
    [SerializeField]
    TextMeshProUGUI _counter = null;

    /// <summary>
    /// Command history stack
    /// </summary>
    readonly Stack<ICommand> _history = new Stack<ICommand>();

    /// <summary>
    /// Add command to history
    /// </summary>
    /// <param name="command">Command to add</param>
    public void Add(ICommand command) {
        _history.Push(command);
        _counter.SetText(_history.Count.ToString());
    }

    /// <summary>
    /// Undo last command
    /// </summary>
    public void Undo() {
        if (_history.Count > 0) {
            var command = _history.Pop();
            command.Undo();
        }
        _counter.SetText(_history.Count.ToString());
    }

    /// <summary>
    /// Undo all commands
    /// </summary>
    public void UndoAll() {
        StartCoroutine(UndoAllCoroutine());
    }

    /// <summary>
    /// Undo all commands in coroutine
    /// </summary>
    IEnumerator UndoAllCoroutine() {
        while (_history.Count > 0) {
            var command = _history.Pop();
            command.Undo();
            _counter.SetText(_history.Count.ToString());
            yield return _waiter;
        }
    }
}
