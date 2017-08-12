using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour {

    [SerializeField]
    private int score;
    private int cubeScore;

    private void Awake()
    {
        score = 0;
        cubeScore = 0;
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
            Debug.Log(score);
            if (score >= 15)
            {
                Application.LoadLevel("Level_2");
            }
        }

        if (other.gameObject.CompareTag("Cube"))
        {
            cubeScore++;
            if(cubeScore%27 == 0)
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
            Debug.Log(score);
            if (score >= 3)
            {
                Application.LoadLevel("Level_1");
            }
        }
    }    

}
