using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action Pause;

    public static int Level { get; set; }
    public static bool IsHolding { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        //DontDestroyOnLoad(this.gameObject);
        //Level = 0;
        //Level = 1;
        Level = 2;
        print("LEVEL: " + Level);
    }

    // Update is called once per frame
    void Update()
    {
        //print("IsHolding: " + IsHolding);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause != null)
            {
                Pause();
            }
        }
    }
}
