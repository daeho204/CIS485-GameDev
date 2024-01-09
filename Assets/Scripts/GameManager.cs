using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float GameTime = 10;
    public Text GameTimeText;

    void Update()
    {
        if ((int)GameTime == 0)
        {
            Debug.Log("Game Over");
            //GameTimeText.text = "Game Over";
            SceneManager.LoadScene("Ending");  
        } else
        {
            GameTime -= Time.deltaTime;
            Debug.Log((int)GameTime);
            GameTimeText.text = "Time: " + (int)GameTime;    
        }
    }
}
