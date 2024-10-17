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
        string projectPath = Directory.GetParent(Application.dataPath).FullName; // 프로젝트 경로
        string savePath = Path.Combine(projectPath, "Save", "player.save"); // Save 폴더 경로
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

            // Rank 생성 및 y값 변화
            GameObject newRank = Instantiate(rank, LeftRank.transform);
            Vector3 newRankPos = newRank.transform.localPosition;
            newRankPos.y -= i * 70;  // i만큼 y값 감소 (간격을 20으로 설정)
            newRank.transform.localPosition = newRankPos;

            newRank.GetComponent<TextMeshProUGUI>().text = ranks.ToString();

            // Score 생성 및 y값 변화
            GameObject newScore = Instantiate(score, LeftRank.transform);
            Vector3 newScorePos = newScore.transform.localPosition;
            newScorePos.y -= i * 70;  // 동일하게 y값 감소
            newScore.transform.localPosition = newScorePos;

            newScore.GetComponent<TextMeshProUGUI>().text = Scores[i-1].ToString();
        }
    }
}
