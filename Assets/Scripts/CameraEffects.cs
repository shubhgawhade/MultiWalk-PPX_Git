using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffects : MonoBehaviour
{
    //14 - 160
    float fov;
    public float val;
    //public float timeTo = 10;

    // Start is called before the first frame update
    void Start()
    {
        val = Random.Range(14f, 160f);
    }

    // Update is called once per frame
    void Update()
    {
        fov = Camera.main.fieldOfView;
        if(Mathf.Round(fov) != Mathf.Round(val))
        {
            Camera.main.fieldOfView = Mathf.Lerp(fov, val, Time.deltaTime * 0.5f);
            //print("LERP" + Mathf.Round(fov) + " " + Mathf.Round(val));
        }
        else
        {
            val = Random.Range(14f, 160f);
            //print("FOV");
        }
    }
}
