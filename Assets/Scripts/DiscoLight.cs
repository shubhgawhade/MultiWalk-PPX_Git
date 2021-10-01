using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoLight : MonoBehaviour
{
    public static int colourPick;

    public float angleToRotateX;
    public float angleToRotateY;
    public bool colourChanged;

    // Start is called before the first frame update
    void Start()
    {
        angleToRotateX = Random.Range(0, 180);
        angleToRotateY = Random.Range(0, 180);
    }

    // Update is called once per frame
    void Update()
    {

        /*
        print(Mathf.Round(transform.rotation.eulerAngles.x) + " " + Mathf.Round(angleToRotateX));

        if(Mathf.Round(transform.rotation.eulerAngles.x) != Mathf.Round(angleToRotateX) && Mathf.Round(transform.rotation.eulerAngles.y) != Mathf.Round(angleToRotateY))
        {
            transform.Rotate(angleToRotateX, angleToRotateY, 0);
        }
        else
        {
            angleToRotateX = Random.Range(0, 180);
            angleToRotateY = Random.Range(0, 180);
        }
        */
        ColourChange();
    }

    private void ColourChange()
    {
        if (!colourChanged)
        {
            colourPick = Random.Range(0, 7);
            colourChanged = true;
            StartCoroutine("Wait");
            switch (colourPick)
            {
                case 0:
                    gameObject.GetComponent<Light>().color = Color.white;
                    break;

                case 1:
                    gameObject.GetComponent<Light>().color = Color.cyan;
                    break;

                case 2:
                    if (GameManager.Level != 1)
                    {
                        gameObject.GetComponent<Light>().color = Color.green;
                    }
                    break;

                case 3:
                    gameObject.GetComponent<Light>().color = Color.yellow;
                    break;

                case 4:
                    gameObject.GetComponent<Light>().color = Color.blue;
                    break;

                case 5:
                    gameObject.GetComponent<Light>().color = Color.magenta;
                    break;

                case 6:
                    gameObject.GetComponent<Light>().color = Color.red;
                    break;
            }
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        colourChanged = false;
    }

    private void OnEnable()
    {
        colourChanged = false;
        ColourChange();
    }
}
