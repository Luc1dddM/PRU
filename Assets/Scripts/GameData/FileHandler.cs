using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileHandler
{
    private string dataDirPath = string.Empty;
    private string dataFileName = string.Empty;

    public FileHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData LoadFromFile()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData readData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToRead = "";
                using(FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        dataToRead = reader.ReadToEnd();
                    }
                }

                readData =  JsonUtility.FromJson<GameData>(dataToRead);
            }catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
        return readData;
    }

    public void SaveToFile(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToWrite = JsonUtility.ToJson(data, true);

            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToWrite);
                }
            }
        }
        catch(Exception ex)
        {
            Debug.LogError(ex);
        }
    }

    public void DeleteFile()
    {
        string filePath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log("Deleted save file: " + dataFileName); // Thêm log để xác nhận file đã được xóa
            }
            else
            {
                Debug.Log("No save file found to delete: " + dataFileName); // Thêm log để xác nhận không tìm thấy file để xóa
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Failed to delete save file: " + dataFileName + ". Error: " + e.Message); // Thêm log để hiển thị lỗi nếu có lỗi xảy ra
        }
    }
}
