using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingManager
{
    public static string directory = "SaveData";
    public static string fileName = "moves1.txt";

    public static void Save(PlayerMoves moves)
    {
        if (!DirectoryExists())
        {
            Directory.CreateDirectory(GetFullDirectory());
        }
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(GetFullPath());
        Debug.Log(GetFullPath());
        binaryFormatter.Serialize(fileStream, moves);
        fileStream.Close();
    }

    public static PlayerMoves Load()
    {
        PlayerMoves playerMoves = null;
        if (SaveExists())
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(GetFullPath(), FileMode.Open);
                playerMoves = (PlayerMoves) binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
            }
            catch (SerializationException)
            {
                Debug.Log("Failed to load");
            }
            
        }
        return playerMoves;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetFullPath());
    }

    private static bool DirectoryExists()
    {
        return File.Exists(GetFullDirectory());
    }

    private static string GetFullPath()
    {
        return Application.persistentDataPath + "/" + directory + "/" + fileName;
    }

    private static string GetFullDirectory()
    {
        return Application.persistentDataPath + "/" + directory;
    }
}
