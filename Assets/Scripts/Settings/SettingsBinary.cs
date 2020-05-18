using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public static class SettingsBinary
{
    public static void SaveSettingData(SettingsMenu settings)
    {
        //New Binary Formatter
        BinaryFormatter formatter = new BinaryFormatter();
        //New string path for the application saving location
        string path = Application.persistentDataPath + "/Config.cfg";
        //New file stream using path
        FileStream stream = new FileStream(path, FileMode.Create);
        //New SettingsData called data
        SettingsData data = new SettingsData(settings);
        //Serialize the stream
        formatter.Serialize(stream, data);
        //Close the stream
        stream.Close();
    }
    public static SettingsData LoadSettingsData()
    {
        //New string path for the application loading location
        string path = Application.persistentDataPath + "/Config.cfg";
        //If the path exists
        if (File.Exists(path))
        {
            //New Binary Formatter
            BinaryFormatter formatter = new BinaryFormatter();
            //New file stream using path
            FileStream stream = new FileStream(path, FileMode.Open);
            //Deserialize the stream into data
            SettingsData data = formatter.Deserialize(stream) as SettingsData;
            //Close the stream
            stream.Close();
            //Return data
            return data;
        }
        else
        {
            return null;
        }
    }
}