#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

[InitializeOnLoad]
public class GitSubmoduleUpdater
{
    const string EditorPrefKey = "GitSubmoduleUpdaterHasRun";

    static GitSubmoduleUpdater()
    {
        if (!EditorPrefs.GetBool(EditorPrefKey, false))
        {
            EditorApplication.update += UpdateSubmoduleOnce;
        }
    }

    private static void UpdateSubmoduleOnce()
    {
        EditorApplication.update -= UpdateSubmoduleOnce;

        UpdateSubmodule();

        EditorPrefs.SetBool(EditorPrefKey, true);
    }

    private static void UpdateSubmodule()
    {
        string projectRoot = Path.GetFullPath(Application.dataPath + "/..");
        string submodulePath = Path.Combine(Application.dataPath, "Scripts/Utils");
        string submoduleGitFolder = Path.Combine(submodulePath, ".git");

        if (Directory.Exists(submoduleGitFolder))
        {
            // Submodule é um repo Git independente
            RunGitCommand("pull", submodulePath);
        }
        else
        {
            // Submodule é gerenciado pelo git do projeto pai
            RunGitCommand("submodule update --remote Assets/Scripts/Utils", projectRoot);
        }
    }

    private static void RunGitCommand(string args, string workingDir)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "git",
            Arguments = args,
            WorkingDirectory = workingDir,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        try
        {
            using Process proc = Process.Start(psi);
            string output = proc.StandardOutput.ReadToEnd();
            string error = proc.StandardError.ReadToEnd();
            proc.WaitForExit();

            UnityEngine.Debug.Log($"Git command 'git {args}' output:\n{output}");
            if (!string.IsNullOrEmpty(error))
                UnityEngine.Debug.LogWarning($"Git command 'git {args}' error:\n{error}");
        }
        catch (System.Exception ex)
        {
            UnityEngine.Debug.LogError($"Failed to run git command 'git {args}': {ex.Message}");
        }
    }
}
#endif
