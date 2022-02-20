using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class scores : MonoBehaviour
{
    public int score;
    public Text ScoreText;
    private void Start()
    {
        score = 0;
    }
    public void  Updatescore()
    {
        score++;
        ScoreText.text = score.ToString();
    }
    
}
