//Essa interface exige NewtonSoft:
//com.unity.nuget.newtonsoft-json
using Newtonsoft.Json.Linq;

/// <summary>
/// Interface base para objetos salváveis.
/// </summary>
public interface ISaveable
{
    string SaveKey();
    object CaptureState();
    void RestoreState(JToken state);
}
