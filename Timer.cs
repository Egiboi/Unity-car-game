using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour
{
    GameObject Player;
    [SerializeField]
    private Text timeText;
    //maxTime is the time the player can be in the level before failing.
    //bronze-, silver-, and goldTime are for testing how well the player did in the level.
    //timer is the double used for counting time

    [SerializeField]
    private double maxTime, bronzeTime, silverTime, goldTime, timer;
    //Boolean for turning the timer off if needed
    private bool timerOn = true;
    /// <summary>
    /// sets timer to 0 and if medal times have not been set, this will set them automaticly
    /// </summary>
    void Start()
    {
        Player = GameObject.Find("Player");
        timer = 0;
        //if bronze-, silver-, and goldTimes have not been set this will automaticly set them
        if (bronzeTime == 0)
            bronzeTime = maxTime * 0.90;
        if (silverTime == 0)
            silverTime = maxTime * 0.75;
        if (goldTime == 0)
            goldTime = maxTime * 0.60;
    }

    /// <summary>
    /// Counts up time and brings up the game over screen if timer reaches the max time
    /// </summary>
    void Update()
    {
        if (timerOn)
            timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            //Fail the level and bring up the game over screen
            timerOn = false;
            Debug.Log("Time out");
            Player.GetComponent<PlayerController>().PlayerKiller();

        }
        timeText.text = "Time: " + getTime();
    }

    /// <summary>
    /// returns what medal the player has
    /// </summary>
    /// <returns>medal</returns>
    public string TimeMedal()
    {
        if (timer < goldTime)
            return "gold";
        else if (timer < silverTime)
            return "silver";
        else if (timer < bronzeTime)
            return "bronze";
        else
            return "none";
    }
    /// <summary>
    /// returns current count in timer rounded to 2 decimals
    /// </summary>
    public double getTime()
    {
        return System.Math.Round(timer, 2);
    }
    /// <summary>
    /// toggles timer on and off
    /// </summary>
    public void toggleTimer()
    {
        timerOn = !timerOn;
    }
}
