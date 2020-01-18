using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody2D rb2d;
    [SerializeField]
    private Text pointText, fuelText;
    // Floats that can be changed from unity
    [SerializeField]
    private float accelerationPower = 5f, steeringPower = 5f, totalSpeed, boostAmount = 5f, maxFuel = 1000f;
    // Floats that cannot be changed elsewhere
    private float speed, steeringAmount, direction, boost, fuel, brake;
    private int points;
    private bool controlsOn, scoreInserted;
    GameObject SceneSet;
    // The database object, that will be referenced in start
    SQLiteTest db;
    // Timer object, that is referenced in start
    Timer timer;

    /// <summary>
    /// Finds the necessary components and adds them to variables and sets correct values to varaibles
    /// </summary>
    void Start()
    {
        SceneSet = GameObject.Find("Scene follower");
        points = 0;
        db = GetComponent<SQLiteTest>();
        rb2d = GetComponent<Rigidbody2D>();
        timer = GetComponent<Timer>();
        fuel = maxFuel;
        controlsOn = true;
        scoreInserted = false;
        updateFuelUI();
    }

    /// <summary>
    /// calculates and applies player physics
    /// </summary>
    void FixedUpdate()
    {
        //used to disable the controls while jumping
        if (controlsOn)
        {
            // All the necessary float numbers are set
            // Controls how well the player can turn
            steeringAmount = -Input.GetAxis("Horizontal");
            // Controls the basic speed and direction(foward/backward) of the player
            speed = Input.GetAxis("Vertical") * accelerationPower;
            // Controls which way the player moves
            direction = Mathf.Sign(Vector2.Dot(rb2d.velocity, rb2d.GetRelativeVector(Vector2.up)));
            // Controls which way the player is facing
            rb2d.rotation += steeringAmount * rb2d.velocity.magnitude * direction * steeringPower / ((totalSpeed) / 600 + 1.2f);
            // Adds boost to speed to get totalSpeed
            totalSpeed = speed + boost;
            // Makes sure reversing is slower than driving forwards
            if (totalSpeed < -300)
                totalSpeed = -300;

            // Applies the forces to the car
            rb2d.AddRelativeForce(Vector2.right * rb2d.velocity.magnitude * steeringAmount / 2);
            rb2d.AddRelativeForce(Vector2.up * totalSpeed);


        }

    }
    /// <summary>
    /// Checks when the player touches a trigger collider (e.i. a beer) and acts accordingly (e.i. adds a point to the counter)
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        //If the trigger is a hay bale and the player is boosting, the hay bale is disabled
        if (other.gameObject.CompareTag("Hay Bale") && boost > 0)
        {
            other.gameObject.SetActive(false);
        }
        // UFO killing line
        if (other.gameObject.CompareTag("UFO"))
        {
            PlayerKiller();
        }
        //if the trigger is fuel and the player does not have full fuel, the fuel object is disabled and the player gains full fuel
        if (other.gameObject.CompareTag("Fuel") && fuel < maxFuel)
        {
            other.gameObject.SetActive(false);
            fuel = maxFuel;
            updateFuelUI();
        }

        // If the trigger is finish line, level complete menu will open. Sets current scene into scenefollower
        if (other.gameObject.CompareTag("FinishLine"))
        {
            bool tempCheck = false;
            if (tempCheck == false)
            {
                SceneSet.GetComponent<SceneFollower>().SetScene(SceneNumber());
                SceneSet.GetComponent<SceneFollower>().SetBool(true);
                double timeDouble = timer.getTime();
                int time = (int)timeDouble;
                if (scoreInserted==false)
                {
                    db.InsertScore(SceneNumber(), points, time);
                    scoreInserted = true;

                }
                
                if (SceneNumber() == 4)
                {
                    SceneManager.LoadScene("Story ending");
                    tempCheck = true;
                }
                else
                {
                    SceneManager.LoadScene("Level Complete Menu");
                    tempCheck = true;
                }
            }
        }
    }
    /// <summary>
    /// Calculates the boost and fuel usage
    /// </summary>
    private void Update()
    {
        // Boosts if the conditions are met, uses up 10 fuel each update
        if (Input.GetKey(KeyCode.Space) && fuel > 0)
        {
            boost = boostAmount;
            fuel -= 10;
            updateFuelUI();
        }
        // After player stops boosting, this slowly slows the speed down to the normal level
        else if (boost > 0)
        {
            boost -= 10;
        }
    }
    /// <summary>
    /// Returns cars boost amount
    /// </summary>
    /// <returns>boost</returns>
    public float GetBoost()
    {
        return boost;
    }
    /// <summary>
    /// Returns the beer point value when called. Called when level completed
    /// </summary>
    /// <returns></returns>
    public int GetPoints()
    {
        return points;
    }

    /// <summary>
    /// A method used to "kill" the player and end the game
    /// </summary>
    public void PlayerKiller()
    {
        SceneSet.GetComponent<SceneFollower>().SetScene(SceneNumber());
        SceneSet.GetComponent<SceneFollower>().SetBool(false);
        controlsOn = false;
        rb2d.velocity = new Vector2(0.0f, 0.0f);

        StartCoroutine(PlayerDeath());
    }
    /// <summary>
    /// Connected to the playerkiller method. Makes it that there is a delay between death and loading the end scene
    /// </summary>
    /// <returns></returns>
    IEnumerator PlayerDeath()
    {
        yield return new WaitForSeconds(1 / 2);
        SceneManager.LoadScene("Game Over Menu");
    }
    /// <summary>
    /// Gives current scene a numerical value so that the data can be saved into  database with it
    /// </summary>
    /// <returns></returns>
    public int SceneNumber()
    {
        Scene Scene = SceneManager.GetActiveScene();
        string name = Scene.name;
        int number;
        if (name.Equals("Tutorial Map"))
        {
            number = 0;
            return number;
        }
        if (name.Equals("Map 1"))
        {
            number = 1;
            return number;
        }
        if (name.Equals("Map 2"))
        {
            number = 2;
            return number;
        }
        if (name.Equals("Map 3"))
        {
            number = 3;
            return number;
        }
        if (name.Equals("Map 4"))
        {
            number = 4;
            return number;
        }
        return 100;
    }

    /// <summary>
    /// Updates the fuel count on screen
    /// </summary>
    private void updateFuelUI()
    {
        fuelText.text = "Fuel: " + fuel;
    }
    public void addPoint()
    {
        points++;
        pointText.text = "Points: " + points;
    }
}
