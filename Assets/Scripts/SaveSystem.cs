using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
   public static void savePlayer(Player player )
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.sff";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        
        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream,data);

        stream.Close();
    }

    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.sff";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    public static void saveInventory(InventoryController ic)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/inventory.sff";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);

        InventoryData data = new InventoryData(ic);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static InventoryData loadInventory()
    {
        string path = Application.persistentDataPath + "/inventory.sff";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InventoryData data = formatter.Deserialize(stream) as InventoryData;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }

    
}
