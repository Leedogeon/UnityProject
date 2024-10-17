using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTable : MonoBehaviour
{
    private GameObject LeftRank;
    private GameObject rank;
    private GameObject score;
    
    public List<int> Scores;
    private void Start()
    {
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // ������Ʈ ���
        string savePath = Path.Combine(projectPath, "Save", "player.save"); // Save ���� ���
        if (File.Exists(savePath))
        {
            print(savePath);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(savePath, FileMode.Open))
            {
                PlayerDataSave data = formatter.Deserialize(stream) as PlayerDataSave;
                Scores = data.score;
            }
        }
        LeftRank = GameObject.Find("LeftRank");
        rank = GameObject.Find("LRank");
        score = GameObject.Find("LScore");
        GenerateScoreboard();
    } 

    void GenerateScoreboard()
    {
        for (int i = 1; i < Scores.Count+1; i++)
        {
            int ranks = i;

            // Rank ���� �� y�� ��ȭ
            GameObject newRank = Instantiate(rank, LeftRank.transform);
            Vector3 newRankPos = newRank.transform.localPosition;
            newRankPos.y -= i * 70;  // i��ŭ y�� ���� (������ 20���� ����)
            newRank.transform.localPosition = newRankPos;

            newRank.GetComponent<TextMeshProUGUI>().text = ranks.ToString();

            // Score ���� �� y�� ��ȭ
            GameObject newScore = Instantiate(score, LeftRank.transform);
            Vector3 newScorePos = newScore.transform.localPosition;
            newScorePos.y -= i * 70;  // �����ϰ� y�� ����
            newScore.transform.localPosition = newScorePos;

            newScore.GetComponent<TextMeshProUGUI>().text = Scores[i-1].ToString();
        }
    }
}
