using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Common.Save
{
    /// <summary>
    /// Sistema de salvamento e carregamento de dados.
    /// Baseado em multicast delegate.
    /// </summary>
    public static class SaveSystem
    {
        public delegate ISaveable CallToSave();
        public static event CallToSave OnCallToSave;

        private const string SaveFileName = "save.json";

        public static void Save()
        {
            List<ISaveable> list = new();

            if (OnCallToSave != null)
            {
                foreach (CallToSave call in OnCallToSave.GetInvocationList().Cast<CallToSave>())
                {
                    ISaveable result = call.Invoke();
                    if (result != null)
                        list.Add(result);
                }
            }

            var saveData = new Dictionary<string, object>();

            foreach (var item in list)
            {
                if (!saveData.ContainsKey(item.SaveKey()))
                    saveData[item.SaveKey()] = item.CaptureState();
                else
                    Debug.LogWarning("Chave duplicada no Save: " + item.SaveKey());
            }

            string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
            string path = Path.Combine(Application.persistentDataPath, SaveFileName);

            try
            {
                File.WriteAllText(path, json);
            }
            catch (Exception ex)
            {
                Debug.LogError("Erro ao salvar: " + ex.Message);
            }
        }

        public static void Load()
        {
            string path = Path.Combine(Application.persistentDataPath, SaveFileName);
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            var saveData = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(json);

            if (OnCallToSave == null) return;

            foreach (CallToSave call in OnCallToSave.GetInvocationList().Cast<CallToSave>())
            {
                ISaveable saveable = call.Invoke();
                if (saveable == null) continue;

                if (saveData.TryGetValue(saveable.SaveKey(), out var state))
                {
                    saveable.RestoreState(state);
                }
            }
        }
    }
}

