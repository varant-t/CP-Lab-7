using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;

public static class SaveSystem
{

    public static void SavePlayer(Player player)
    {
        try
        {
            Aes aes = Aes.Create();

            byte[] key = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, };
            byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, };

            //Create Binary Formatter to write to a file
            BinaryFormatter formatter = new BinaryFormatter();

            //Path to our file creation - Unity chooses the filepath based on operating system
            string path = Application.persistentDataPath + "/playerdata.io";

            //Creating our stream to save the file
            FileStream stream = new FileStream(path, FileMode.Create);

            //Create a CryptoStream, pass it the FileStream, and encrypt
            //it with the Aes class.  
            CryptoStream cryptStream = new CryptoStream(stream, aes.CreateEncryptor(key, iv), CryptoStreamMode.Write);

            //Create a StreamWriter for easy writing to the
            //file stream.  
            StreamWriter sWriter = new StreamWriter(cryptStream);


            //PlayerData is now being created
            PlayerData data = new PlayerData(player);

            //Serializing the PlayerData
            formatter.Serialize(stream, data);

            //Write to the stream.  
            sWriter.WriteLine("Hello World!");

            //Closing the stream - VERY IMPORTANT STEP
            sWriter.Close();
            cryptStream.Close();
            stream.Close();
        }


        catch
        {
            //Inform the user that an exception was raised.  
            Debug.Log("The encryption failed.");
            throw;
        }
    }

    public static PlayerData LoadPlayer()
    {
        //Path to our file - Unity chooses the filepath based on operating system
        string path = Application.persistentDataPath + "/playerdata.io";

        //Check to make sure if the file exists
        if (File.Exists(path))
        {
            //Create Binary Formatter to write to a file
            BinaryFormatter formatter = new BinaryFormatter();

            //Creating our stream to open the file
            FileStream stream = new FileStream(path, FileMode.Open);

            //Deserializing our data - Deserialize function ouputs an object so we have to cast the object to our PlayerData class
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            //Closing the stream - VERY IMPORTANT STEP
            stream.Close();

            //Return our PlayerData
            return data;
        }

        //if file doesn't exist, output an error.
        else
        {
            Debug.LogError("File does not exist");
            return null;
        }
    }
}
