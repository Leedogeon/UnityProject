using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public Transform Player;
    public TextMeshProUGUI scoreText;
    [SerializeField] private PlayerAction ActionScript;
    private void Start()
    {
        scoreText= GetComponent<TextMeshProUGUI>();
        if(Player !=null)
        {
            ActionScript = Player.GetComponent<PlayerAction>();
        }
    }
    private void Update()
    {
        scoreText.text = ActionScript.MaxDistance.ToString();
    }
}
