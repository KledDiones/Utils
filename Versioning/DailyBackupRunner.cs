#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;

namespace Common.Versioning
{
    /// <summary>
    /// Sistema de backup diário do projeto usando o Version Control.
    /// </summary>
    [InitializeOnLoad]
    public static class DailyBackupRunner
    {
        #region Constants

        private const string LastBackupDateKey = "Sandbox_LastBackupDate";
        private const string WorkingDirectory = @"D:\Projetos\3d-platformer-game\Sandbox";

        #endregion

        #region Fields

        private static readonly IVersionController versionController = new VersionController();

        #endregion

        #region Static Constructor

        static DailyBackupRunner()
        {
            EditorApplication.update += Update;
        }

        #endregion

        #region Private Methods

        private static void Update()
        {
            string lastDateStr = EditorPrefs.GetString(LastBackupDateKey, "");

            if (!DateTime.TryParse(lastDateStr, out DateTime lastDate))
                lastDate = DateTime.MinValue;

            if ((DateTime.Now - lastDate).TotalDays >= 1)
            {
                RunBackup();
                EditorPrefs.SetString(LastBackupDateKey, DateTime.Now.ToString("yyyy-MM-dd"));
            }
            else
            {
                EditorApplication.update -= Update;
            }
        }

        private static void RunBackup()
        {
            var process = new System.Diagnostics.Process
            {
                StartInfo = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "cm",
                    Arguments = $"checkin -m \"Daily Auto commit after version {versionController.GetVersionString()}\" --all",
                    WorkingDirectory = WorkingDirectory,
                    CreateNoWindow = true,
                    UseShellExecute = false
                }
            };

            process.Start();
            process.WaitForExit();

            Debug.Log("[DailyBackupRunner] Backup diário executado.");
            EditorApplication.update -= Update;
        }

        #endregion
    }
}
#endif
