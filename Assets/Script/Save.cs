using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class Save
{
    public static void SavePlayer(PlayerAction action, PlayerInfo info)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        // ������Ʈ ��ο��� Save ������ ����
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // ������Ʈ ���
        string savePath = Path.Combine(projectPath, "Save", "player.save"); // Save ���� ���

        // Save ������ �������� ������ ����
        if (!Directory.Exists(Path.GetDirectoryName(savePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        }

        using (FileStream stream = new FileStream(savePath, FileMode.Create))
        {
            PlayerDataSave data = new PlayerDataSave(action, info);
            formatter.Serialize(stream, data);
        }
    }

    public static PlayerDataSave LoadPlayer(PlayerAction action, PlayerInfo info)
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // ������Ʈ ���
        string loadPath = Path.Combine(projectPath, "Save", "player.save"); // Save ���� ���

        if (File.Exists(loadPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(loadPath, FileMode.Open))
            {
                PlayerDataSave data = formatter.Deserialize(stream) as PlayerDataSave;
                return data;
            }
        }
        else
        {
            Debug.Log("Save file not found in " + loadPath);
            return null;
        }
    }
}