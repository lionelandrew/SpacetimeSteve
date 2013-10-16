using UnityEngine;
using System.Collections;

public class PlayerScore : MonoBehaviour {

    public UILabel scoreLabel;

    static int score;

    public static void ResetScore()
    {
        score = 0;
    }

    public static void AddPlayerScore(int newScore)
    {
        score += newScore;
    }

    public static int GetPlayerScore()
    {
        return score;
    }
	
	// Update is called once per frame
	void Update () {
        scoreLabel.text = "Score: " + score.ToString();
	}
}
