#if UNITY_EDITOR
using UnityEditor;
#endif

using System;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Common.Versioning
{
    /// <summary>
    /// Controlador de versão de build do projeto.
    /// </summary>
    public class VersionController : IVersionController
    {
        #region Constants

        private const string VersionFileName = "Version.json";

        #endregion

        #region Static Fields

        private static readonly string VersionFilePath = Path.Combine(Application.streamingAssetsPath, VersionFileName);
        public static string CurrentVersion { get; private set; } = "2024.01.01.1";

        #endregion

        #region Constructors

        public VersionController()
        {
            LoadVersionFromFile();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retorna a versão atual como string.
        /// </summary>
        public string GetVersionString()
        {
            // Garante que a versão está carregada
            LoadVersionFromFile();

            string currentDate = DateTime.Now.ToString("yyyy.MM.dd");
            string[] versionParts = CurrentVersion.Split('.');

            int buildNumber = int.Parse(versionParts[3]);
            return $"{currentDate}.{buildNumber}";
        }

        /// <summary>
        /// Atualiza a versão e salva no arquivo.
        /// </summary>
        [ContextMenu("Update Version")]
        public void UpdateVersion()
        {
            CurrentVersion = GenerateNewVersion(CurrentVersion);
            SaveVersionToFile(CurrentVersion);
            UpdateWindowTitle();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Chamado antes de cada build (usar via Build Pipeline).
        /// </summary>
        public void OnPreBuild()
        {
            CurrentVersion = GenerateNewVersion(CurrentVersion);
            SaveVersionToFile(CurrentVersion);
            Debug.Log($"[VersionControl] Build versão atualizada para: {CurrentVersion}");
        }
#endif

        /// <summary>
        /// Atualiza o título da janela com a versão atual (Windows apenas).
        /// </summary>
        public static void UpdateWindowTitle()
        {
#if UNITY_STANDALONE_WIN
            SetWindowText(GetActiveWindow(), $"{Application.productName} - Version {CurrentVersion}");
#endif
        }

        #endregion

        #region Private Methods

        private static void LoadVersionFromFile()
        {
            if (File.Exists(VersionFilePath))
            {
                string json = File.ReadAllText(VersionFilePath);
                VersionData data = JsonUtility.FromJson<VersionData>(json);
                CurrentVersion = data.version;
            }
            else
            {
                CurrentVersion = "2024.01.01.1";
                SaveVersionToFile(CurrentVersion);
            }
        }

        private static void SaveVersionToFile(string version)
        {
            string directory = Path.GetDirectoryName(VersionFilePath);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string json = JsonUtility.ToJson(new VersionData { version = version }, true);
            File.WriteAllText(VersionFilePath, json);
        }

        private static string GenerateNewVersion(string currentVersion)
        {
            DateTime today = DateTime.Today;
            string[] versionParts = currentVersion.Split('.');

            if (versionParts.Length == 4 &&
                int.TryParse(versionParts[0], out int year) &&
                int.TryParse(versionParts[1], out int month) &&
                int.TryParse(versionParts[2], out int day) &&
                new DateTime(year, month, day) == today)
            {
                int buildNumber = int.Parse(versionParts[3]) + 1;
                return $"{today:yyyy.MM.dd}.{buildNumber}";
            }
            else
            {
                return $"{today:yyyy.MM.dd}.1";
            }
        }

        #endregion

        #region Structs

        [Serializable]
        private struct VersionData
        {
            public string version;
        }

        #endregion

        #region Native Methods (Windows)

#if UNITY_STANDALONE_WIN
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr GetActiveWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern bool SetWindowText(IntPtr hwnd, string lpString);
#endif

        #endregion
    }
}
