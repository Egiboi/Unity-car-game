using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreFinder : SQLiteTest{
    GameObject SceneSet;
    [SerializeField]
    Text scoreText;

    /// <summary>
    /// Simple code used to set score into a text after a level is completed/failed
    /// </summary>
    void Start() {
        SceneSet = GameObject.Find("Scene follower");

        string x = GetHighScoreOfLevel(SceneSet.GetComponent<SceneFollower>().GetSceneNumber());
        scoreText.text = SceneSet.GetComponent<SceneFollower>().GetScene()+ " Stats \n" +
                         "Your Current Best: " + x;
	
	}
	


}
