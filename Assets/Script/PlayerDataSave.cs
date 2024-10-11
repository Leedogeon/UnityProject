using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDataSave
{
    public int Level;
    public int HP;
    public float[] position;

    public PlayerDataSave (PlayerAction action,PlayerInfo info)
    {
        position = new float[3];
        position[0] = action.transform.position.x;
        position[1] = action.transform.position.y;
        position[2] = action.transform.position.z;
        Level = info.Level;
        HP = info.HP;
    }

}
