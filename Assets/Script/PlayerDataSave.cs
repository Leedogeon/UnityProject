using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[System.Serializable]
public class PlayerDataSave
{
    public int Level;
    public int HP;
    public List<int> score;
    public float[] position;
    
    public PlayerDataSave (PlayerAction action,PlayerInfo info, List<int> existingScores)
    {
        position = new float[3];
        position[0] = action.transform.position.x;
        position[1] = action.transform.position.y;
        position[2] = action.transform.position.z;
        Level = info.Level;
        HP = info.HP;
        score = new List<int>(existingScores);
        if(score.Count == 10)
        {
            int minScoreIndex = score.IndexOf(score.Min());
            if (action.MaxDistance > score[minScoreIndex])
            {
                score[minScoreIndex] = action.MaxDistance; // 큰 값으로 교체
            }
        }
        else score.Add(action.MaxDistance);

        score.Sort((a,b)=> b.CompareTo(a));
    }

}
