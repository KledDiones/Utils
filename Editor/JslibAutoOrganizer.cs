using UnityEngine;
using UnityEditor;
using System.IO;

public class JslibAutoOrganizer : AssetPostprocessor
{
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
                                       string[] movedAssets, string[] movedFromAssetPaths)
    {
        Debug.Log("Rodando post");

        string sourceFolder = "Assets/Scripts/Utils/Plugins/WebGL";
        string pluginsFolder = "Assets/Plugins/KledUtils/WebGL";

        if (!Directory.Exists(pluginsFolder))
            Directory.CreateDirectory(pluginsFolder);

        var sourceFiles = Directory.GetFiles(sourceFolder, "*.jslib", SearchOption.TopDirectoryOnly);

        bool changed = false;

        foreach (var sourceFile in sourceFiles)
        {
            string fileName = Path.GetFileName(sourceFile);
            string destPath = Path.Combine(pluginsFolder, fileName);

            bool needCopy = false;

            if (!File.Exists(destPath))
                needCopy = true;
            else
            {
                var srcTime = File.GetLastWriteTimeUtc(sourceFile);
                var dstTime = File.GetLastWriteTimeUtc(destPath);
                if (srcTime > dstTime)
                    needCopy = true;
            }

            if (needCopy)
            {
                File.Copy(sourceFile, destPath, true);
                Debug.Log($"[JslibAutoOrganizer] Copiado {fileName} para {pluginsFolder}");
                changed = true;
            }
        }

        if (changed)
            AssetDatabase.Refresh();
    }
}
