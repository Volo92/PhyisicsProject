using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logic : MonoBehaviour {

    [SerializeField]
    private int score;
    private int cubeScore;
    [SerializeField]
    private Text scoreText;

    private void Awake()
    {
        score = 0;
        cubeScore = 0;
        if (Application.loadedLevelName.Equals("Level_1"))
        {
            scoreText.text = "0 / 15";
        } else if (Application.loadedLevelName.Equals("Level_2"))
        {
            scoreText.text = "0 / 405";
        } else
        {
            scoreText.text = "0 / 3";
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            score++;
            scoreText.text = score.ToString() + " / 15";
            Debug.Log(score);
            if (score >= 15)
            {
                Application.LoadLevel("Level_2");
            }
        }

        if (other.gameObject.CompareTag("Cube"))
        {
            cubeScore++;
            scoreText.text = cubeScore.ToString() + " / 405";
            if (cubeScore%27 == 0)
            {
                score++;
                Debug.Log(score);
                if (score >= 15)
                {
                    Application.LoadLevel("Level_3");
                }
            }
        }

        if (other.gameObject.CompareTag("HardGoal"))
        {
            score++;
            scoreText.text = score.ToString() + " / 3";
            Debug.Log(score);
            if (score >= 3)
            {
                Application.Quit();
            }
        }
    }    

}
