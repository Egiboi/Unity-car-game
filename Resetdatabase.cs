using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resetdatabase : MonoBehaviour {
    SQLiteTest db;
    Button button;

    // Use this for initialization
    void Start() {
        db = GetComponent<SQLiteTest>();
        button = GetComponent<Button>();
        button.onClick.AddListener(cleardb);
    }
    public void cleardb()
    {
        db.EmptyTable();
        Debug.Log("Table emptied");
    }
	

}
