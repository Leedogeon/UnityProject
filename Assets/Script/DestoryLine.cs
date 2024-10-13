using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class DestoryLine : MonoBehaviour
{
    public List<string> Tags = new List<string>();

    private void Start()
    {
        Tags.Add("Block");
        Tags.Add("Floor");
    }
    void OnTriggerEnter(Collider other)
    {
        print("Tag : " + other.gameObject.name);

        for(int i = 0; i < Tags.Count; i++)
        {
            if (other.CompareTag(Tags[i]))
            {
                Destroy(other.gameObject);
            }
        }
        
    }
}
