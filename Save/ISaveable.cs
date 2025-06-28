using Newtonsoft.Json.Linq;

/// <summary>
/// Interface base para objetos salv�veis.
/// </summary>
public interface ISaveable
{
    string SaveKey();
    object CaptureState();
    void RestoreState(JToken state);
}
