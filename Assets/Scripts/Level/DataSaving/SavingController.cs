using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavingController
{
    public static string directory = "SaveData";
    public static string fileName = "level1.txt";

    // нужно добавить удаление сейва когда уровень проигран

    public static void Save(LevelState moves)
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

    public static LevelState Load()
    {
        LevelState playerMoves = null;
        if (SaveExists())
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(GetFullPath(), FileMode.Open);
                playerMoves = (LevelState) binaryFormatter.Deserialize(fileStream);
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
