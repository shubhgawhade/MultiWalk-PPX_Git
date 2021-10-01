using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] lights;
    [SerializeField] GameObject[] interactables;
    [SerializeField] GameObject lightSwitch;
    [SerializeField] GameObject discoLight;
    [SerializeField] GameObject mainLight;
    [SerializeField] GameObject window;
    [SerializeField] GameObject key;

    [SerializeField] GameObject picked = null;

    public static bool switch1On = false;
    public static bool keyPicked;
    public static bool lvl1Passed;
    public static bool jumped;
    public static bool fainted;

    private Vector3 keyPos;
    private Vector3 switchPos;
    private bool keySpwnd = false;
    private GameObject tempKey;
    private Light mainLightLight;
    private bool allSpawn = false;


    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        mainLightLight = mainLight.GetComponent<Light>();
        //print(MouseLook.mouseSensitivity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!UI.paused)
        {
            Pickup();
        }

        if(player.transform.position.y < -18f)
        {
            GameManager.IsHolding = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            if(GameManager.Level == 2)
            {
                jumped = true;
            }
        }

        switch (GameManager.Level)
        {
            case 0:
                //print("LEVEL 0");
                Level0();
                break;

            case 1:
                //print("LEVEL 1");
                Level1();
                break;

            case 2:
                //print("LEVEL 2");
                Level2();
                break;
        }

        /*
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonUp(0))
        {
            if (GameManager.IsHolding)
            {
                if (picked != null)
                {
                    picked.transform.position = hit.transform.position;
                    picked.gameObject.SetActive(true);
                    picked = null;
                    GameManager.IsHolding = false;
                }
            }
            else
            {
                if (picked == null)
                {
                    picked = hit.transform.gameObject;
                    hit.transform.gameObject.SetActive(false);
                    GameManager.IsHolding = true;
                }
            }
            //print(hit.transform.name + " " + GameManager.IsHolding);
        }
        */

        //foreach(GameObject a in interactables)
        //{
        //    if (a.transform.position == Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2)))
        //    {
        //        Cursor.lockState = CursorLockMode.Locked;
        //        Cursor.visible = true;
        //    }
        //    else
        //    {
        //        Cursor.visible = false;
        //    }
        //}

        //Cursor.SetCursor(cursorTexture, new Vector2(Screen.width / 2, Screen.height / 2), CursorMode.ForceSoftware);

    }

    private void Pickup()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            foreach (GameObject b in interactables)
            {
                if (b == hit.transform.gameObject && Mathf.Abs((player.transform.position - hit.transform.position).magnitude) < 3)
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        if (!GameManager.IsHolding && picked == null)
                        {
                            picked = hit.transform.gameObject;
                            hit.transform.gameObject.SetActive(false);
                            GameManager.IsHolding = true;
                        }
                    }
                    break;
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.E) && Mathf.Abs((player.transform.position - hit.point).magnitude) < 3)
                    {
                        if (GameManager.IsHolding)
                        {
                            if (picked != null)
                            {
                                if (picked.gameObject.tag == "Key")
                                {
                                    if(hit.collider.gameObject.tag == "OfficeDoor" || hit.collider.gameObject.tag == "MainDoor")
                                    {
                                        ///print("A");
                                        return;
                                    }
                                }
                                //picked.GetComponent<Rigidbody>().MovePosition(hit.point);
                                //print("B");
                                picked.transform.position = hit.point;
                                picked.gameObject.SetActive(true);
                                picked = null;
                                GameManager.IsHolding = false;
                                break;
                            }
                        }
                    }
                    Cursor.visible = false;
                }
            }
            //print(Cursor.visible);
        }
    }

    private void Level0()
    {
        //CODE TO ADD INTERACTABLES FOR LEVEL 0

        /*
        for(int i=0; i < 5; i++)
        {
            switch (i)
            {
                case 3:
                    interactables[i] = gameObject;
                    break;
            }
        }
        */
        keyPos = new Vector3(42.748f, -4.235f, 72.44f);

        if (!keySpwnd)
        {
            tempKey = Instantiate(key, keyPos, Quaternion.identity);
            keySpwnd = true;
            interactables[3] = tempKey;
        }

        //print(player.transform.position.z + " > " + transform.position.z);
        if (player.transform.position.z > lights[0].transform.position.z)
        {
            lights[0].GetComponent<Light>().intensity -= Time.deltaTime * 4;

            if(lights[2].GetComponent<Light>().intensity < 11.7f && player.transform.position.z > lights[1].transform.position.z)
            {
                lights[2].GetComponent<Light>().intensity += Time.deltaTime * 2;
            }
        }


        if (picked != null && picked.gameObject.tag == "Key")
        {
            mainLightLight.color = Color.green;
            keyPicked = true;
        }
        else if (picked == null) 
        {
            mainLightLight.color = Color.red;
            keyPicked = false;
        }
        //Instantiate(enemy, Vector3.zero, Quaternion.identity);
    }

    private void Level1()
    {
        keyPos = new Vector3(-3.5f, 0.13f, -16.983f);
        switchPos = new Vector3(30.7728f, -2.357f, 49.394f);

        if (!keySpwnd)
        {
            tempKey = Instantiate(key, keyPos, Quaternion.identity);
            keySpwnd = true;
            interactables[3] = tempKey;
        }

        if (!allSpawn)
        {
            Instantiate(lightSwitch, switchPos, Quaternion.Euler(30, 0, 0));
            discoLight.transform.position = new Vector3(39, -0.84f, 70.05181f);
            allSpawn = true;
        }

        //mainLight.transform.position = new Vector3();
        mainLightLight.color = Color.yellow;
        mainLightLight.intensity = 1.3f;
        mainLightLight.range = 13.24f;
        if (!mainLight.activeSelf && switch1On)
        {
            mainLight.SetActive(true);
            //if disco light is in position and has colour cyan
        }
        else if (!switch1On)
        {
            mainLight.SetActive(false);
        }

        if (discoLight.transform.position.x > 30 && discoLight.transform.position.z < 50)
        {
            if (DiscoLight.colourPick==1 && switch1On)
            {
                lvl1Passed = true;
            }
            else
            {
                lvl1Passed = false;
            }
            //if disco light is cyan and switch1ON == true, lvl 1 passed
        }

        if (picked != null && picked.gameObject.tag == "Key")
        {
            keyPicked = true;
        }
        else if (picked == null)
        {
            keyPicked = false;
        }
        //Instantiate(enemy, Vector3.one, Quaternion.identity);
    }

    private void Level2()
    {
        keyPos = new Vector3(-40.2f, -4.2f, 49.2f);

        if (!keySpwnd)
        {
            tempKey = Instantiate(key, keyPos, Quaternion.Euler(0, 90, 0));
            keySpwnd = true;
            interactables[3] = tempKey;
        }

        if(!PlayerMovement.officeDoorOpen && picked != null && player.transform.position.x > 30 && player.transform.position.z < 49 && !fainted)
        {
            if (picked.gameObject.tag == "Key")
            {
                player.GetComponent<PlayerMovement>().enabled = false;
                fainted = true;
                StartCoroutine("Timer");
            }
        }

        if (fainted)
        {
            window.SetActive(false);

            if (jumped)
            {
                window.SetActive(true);
            }
        }

        if (picked != null && picked.gameObject.tag == "Key")
        {
            keyPicked = true;
        }
        else if (picked == null)
        {
            keyPicked = false;
        }

        //Instantiate(enemy, Vector3.one, Quaternion.identity);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}