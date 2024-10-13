using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private PlayerAction ActionScript;
    public Transform DestroyLine;
    private int BlockCount = 1;
    public int BlockDistance = 150;
    private int CurPosZ;

    public List<GameObject> Blocks = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if(Player!= null)
        {
            ActionScript = Player.GetComponent<PlayerAction>();
        }
        CurPosZ = BlockCount * BlockDistance;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, 0, Player.transform.position.z - 100);

        if (Player.transform.position.z >= CurPosZ)
        {
            print("position.z = " + Player.transform.position.z);
            BlockCount++;
            CurPosZ = BlockCount * BlockDistance;


            GameObject selectedPrefab = GetRandomObject();

            Instantiate(selectedPrefab, new Vector3(0,15,CurPosZ), Quaternion.identity);
        }
    }

    GameObject GetRandomObject()
    {
        // 리스트에서 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, Blocks.Count);
        return Blocks[randomIndex];
    }
}
