using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFollower : MonoBehaviour {
    public static SceneFollower instance;
    
    private int lastScene;
    bool lastSuccess;
    Dictionary<int, string> Scenes = new Dictionary<int, string>();
    /// <summary>
    /// Sets this to not be destroyed on during scene changes and adds a scene dictionary
    /// </summary>
    void Awake () {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Scenes.Add(0, "Tutorial Map");
        Scenes.Add(1, "Map 1");
        Scenes.Add(2, "Map 2");
        Scenes.Add(3, "Map 3");
        Scenes.Add(4, "Map 4");
        lastScene = 1;
    }
    /// <summary>
    /// Sets scene that was finished
    /// </summary>
    /// <param name="scene"></param>
    public void SetScene(int scene)
    {
        lastScene = scene;

    }
    public void SetBool(bool boolean)
    {
        lastSuccess = boolean;
    }
	/// <summary>
    /// Gets scene that was last finished (Used in retry button)
    /// </summary>
    /// <returns></returns>
    public string GetScene()
    {
        string tempScene = Scenes[lastScene];
        return tempScene;
    }
    /// <summary>
    /// Gets scene for next level (For next level button). If next scene should be 4, sets it to 1;
    /// </summary>
    /// <returns></returns>
    public string GetNextScene()
    {

            string tempScene1 = Scenes[lastScene + 1];
            if (lastScene + 1 == 5)
                return Scenes[1];
            return tempScene1;

    }
    /// <summary>
    /// Used by database class for numerical saving of a map number
    /// </summary>
    /// <returns></returns>
    public int GetSceneNumber()
    {
        return lastScene;
    }
    public bool GetSuccess()
    {
        return lastSuccess;
    }
        
}
