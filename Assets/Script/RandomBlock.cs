using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlock : MonoBehaviour
{
    public Transform Player;
    [SerializeField] private PlayerAction ActionScript;
    public Transform DestroyLine;
    private int BlockCount = 1;
    public int BlockDistance = 50;
    public int WallDistance = 100;
    private int CurPosZ;

    public List<GameObject> Blocks = new List<GameObject>();
    public List<GameObject> Walls  = new List<GameObject>();

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
            BlockCount++;
            CurPosZ = BlockCount * BlockDistance;

            int randomCount = Random.Range(1, 4);

            for(int i = 0; i< randomCount; i++)
            {
                CreateBlock();
            }
            int WallPosZ = WallDistance * (BlockCount);
            for(int i = 0; i<Walls.Count; i++)
            {
                Quaternion rotation = Quaternion.Euler(0, (i==0? 180:0), -90);
                Instantiate(Walls[i],new Vector3(30*(i==0 ? 1:-1), 15, WallPosZ),rotation);
            }
        }
    }

    GameObject GetRandomObject()
    {
        // 리스트에서 랜덤 인덱스 선택
        int randomIndex = Random.Range(0, Blocks.Count);
        return Blocks[randomIndex];
    }
    void CreateBlock()
    {
        GameObject selectedPrefab = GetRandomObject();
        int randomX = Random.Range(-15, 15);

        Instantiate(selectedPrefab, new Vector3(randomX, 15, CurPosZ), Quaternion.identity);

    }
}
