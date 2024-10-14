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

        // 프로젝트 경로에서 Save 폴더에 저장
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // 프로젝트 경로
        string savePath = Path.Combine(projectPath, "Save", "player.save"); // Save 폴더 경로

        // Save 폴더가 존재하지 않으면 생성
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
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // 프로젝트 경로
        string loadPath = Path.Combine(projectPath, "Save", "player.save"); // Save 폴더 경로

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