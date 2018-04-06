using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Text currentScoreText;
    public Text highScoreText;
    public Font textFont;

	// Use this for initialization
	void Start () {
        currentScoreText.font = textFont;
        highScoreText.font = textFont;
        currentScoreText.text = "Your Current Total Damages are $" + PlayerController.currentDamages.ToString();
        highScoreText.text = "Your Highest Total Damage is $" + PlayerPrefs.GetInt("HighScore", 0).ToString();
	}
	
}
