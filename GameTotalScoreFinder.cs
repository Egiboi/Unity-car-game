using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTotalScoreFinder : SQLiteTest {

    [SerializeField]
    Text scoreText;


    /// <summary>
    /// gets total beer amount collected
    /// </summary>
    void Start()
    {

        int x = GetTotalBeer();
        scoreText.text = "Total Amount of Beer Collected: " + x;


    }


}
