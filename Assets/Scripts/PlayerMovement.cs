using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 12f;
    public float jumpForce = 3f;

    public static bool officeDoorOpen;
    public static bool mainDoorOpen;

    //Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        //startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = transform.forward * verticalInput;
        Vector3 sideway = transform.right * horizontalInput;

        transform.position += (sideway + forward).normalized * speed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.tag == "End")
        {
            if (GameManager.Level > 2)
            {
                Application.Quit();
            }
            GameManager.Level++;
            SceneManager.LoadScene(0);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //print(officeDoorOpen);
        if (GameManager.Level == 0)
        {
            //CONVERT TO SWITCH CASE WHILE BUILDING FINAL GAME

            if (other.tag == "OfficeDoor" && !officeDoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && DiscoLight.colourPick == 4)
                {
                    //other.gameObject.SetActive(false);
                    other.transform.RotateAround(new Vector3(34.839f, other.transform.position.y, other.transform.position.z), Vector3.up, -67.328f);
                    officeDoorOpen = true;
                }
            }

            if (other.tag == "MainDoor" && !mainDoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && officeDoorOpen)
                {
                    //other.gameObject.SetActive(false);
                    other.transform.RotateAround(new Vector3(other.transform.position.x , other.transform.position.y, other.transform.position.z + other.GetComponent<BoxCollider>().size.x), Vector3.up, -90f);
                    mainDoorOpen = true;
                }
            }
        }
        else if (GameManager.Level == 1)
        {
            if (other.tag == "OfficeDoor" && !officeDoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && Levels.keyPicked)
                {
                    //other.gameObject.SetActive(false);
                    other.transform.RotateAround(new Vector3(34.839f, other.transform.position.y, other.transform.position.z), Vector3.up, -67.328f);
                    officeDoorOpen = true;
                }
            }

            if(other.tag=="Switch" && !Levels.switch1On && Input.GetKeyDown(KeyCode.E))
            {
                other.transform.RotateAround(new Vector3(30.7728f, -2.1658f, 48.978f), Vector3.right, -60);
                Levels.switch1On = true;
            }
            else if(other.tag == "Switch" && Levels.switch1On && Input.GetKeyDown(KeyCode.E))
            {
                other.transform.RotateAround(new Vector3(30.7728f, -2.1658f, 48.978f), Vector3.right, 60);
                Levels.switch1On = false;
            }

            if (other.tag == "MainDoor" && !mainDoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && Levels.lvl1Passed)
                {
                    other.transform.RotateAround(new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + other.GetComponent<BoxCollider>().size.x), Vector3.up, -90f);
                    mainDoorOpen = true;
                }
            }
        }
        else if (GameManager.Level == 2)
        {
            if (other.tag == "OfficeDoor" && !officeDoorOpen && Levels.jumped)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //other.gameObject.SetActive(false);
                    other.transform.RotateAround(new Vector3(34.839f, other.transform.position.y, other.transform.position.z), Vector3.up, -67.328f);
                    officeDoorOpen = true;

                }
            }

            if (other.tag == "MainDoor" && !mainDoorOpen)
            {
                if (Input.GetKeyDown(KeyCode.E) && Levels.keyPicked && Levels.jumped)
                {
                    other.transform.RotateAround(new Vector3(other.transform.position.x, other.transform.position.y, other.transform.position.z + other.GetComponent<BoxCollider>().size.x), Vector3.up, -90f);
                    mainDoorOpen = true;
                }
            }
        }
    }

    private void OnDisable()
    {
        officeDoorOpen = false;
        mainDoorOpen = false;
        GameManager.IsHolding = false;
        Levels.keyPicked = false;
        Levels.switch1On = false;
        Levels.lvl1Passed = false;
    }
}