using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Assets.Scripts.Models;

public class FileDataHandler
{
    private readonly string FILENAME = "saveData";
    private readonly string encryptionCodeWord = "uoctfmsrivaskey";

    public SaveGame Load(string slotNumber) 
    {
        if (slotNumber == null) return null;

        string fullPath = Path.Combine(Application.persistentDataPath, slotNumber, FILENAME);
        SaveGame loadedData = null;
        if (File.Exists(fullPath)) 
        {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                }
            }

            dataToLoad = EncryptDecrypt(dataToLoad);
            loadedData = JsonUtility.FromJson<SaveGame>(dataToLoad);
        }
        return loadedData;
    }

    public void Save(SaveGame data, string slotNumber) 
    {
        if (slotNumber == null) return;
        string fullPath = Path.Combine(Application.persistentDataPath, slotNumber, FILENAME);
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(data, true);

        dataToStore = EncryptDecrypt(dataToStore);

        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream)) 
            {
                writer.Write(dataToStore);
            }
        }
    }

    public Dictionary<string, SaveGame> LoadAllProfiles() 
    {
        Dictionary<string, SaveGame> slotDictionary = new Dictionary<string, SaveGame>();

        IEnumerable<DirectoryInfo> dirInfos = new DirectoryInfo(Application.persistentDataPath).EnumerateDirectories();
        foreach (DirectoryInfo dirInfo in dirInfos) 
        {
            string slotId = dirInfo.Name;
            string fullPath = Path.Combine(Application.persistentDataPath, slotId, FILENAME);
            if (!File.Exists(fullPath)) continue;

            SaveGame slotData = Load(slotId);
            if (slotData != null) 
                slotDictionary.Add(slotId, slotData);
        }

        return slotDictionary;
    }

    private string EncryptDecrypt(string data) 
    {
        string modifiedData = "";
        for (int i = 0; i < data.Length; i++) 
        {
            modifiedData += (char) (data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
