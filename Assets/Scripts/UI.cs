using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject PauseMenuUI;
    [SerializeField] GameObject OptionsButtonUI;
    [SerializeField] Slider mouseSensitivity;
    [SerializeField] TextMeshProUGUI mouseSensitivityValueText;

    [SerializeField] GameObject Level0;
    [SerializeField] GameObject Level1;
    [SerializeField] GameObject Level2;

    public static bool paused { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Pause += PauseMenu;
    }

    // Update is called once per frame
    void Update()
    {
        Cues();
    }

    private void Cues()
    {
        switch (GameManager.Level)
        {
            case 0:
                Level0.SetActive(true);
                Level1.SetActive(false);
                Level2.SetActive(false);
                break;

            case 1:
                Level0.SetActive(false);
                Level1.SetActive(true);
                Level2.SetActive(false);
                break;

            case 2:
                if (Levels.fainted)
                {
                    Level0.SetActive(false);
                    Level1.SetActive(false);
                    Level2.SetActive(true);
                }
                break;

            default:
                Level0.SetActive(false);
                Level1.SetActive(false);
                Level2.SetActive(false);
                break;
        }
    }

    public void OptionsButtonGet()
    {
        OptionsButtonUI.SetActive(true);
        PauseMenuUI.SetActive(false);
        mouseSensitivity.value = MouseLook.mouseSensitivity / 1000;

    }

    public void MouseSensitivitySet()
    {
        MouseLook.mouseSensitivity = mouseSensitivity.value * 1000;
        mouseSensitivityValueText.text = mouseSensitivity.value.ToString("#0.000");
        //mouseSensitivityValueText.text = MouseLook.mouseSensitivity.ToString("#0.00");
    }

    public void ResumeButton()
    {
        PauseMenuUI.SetActive(false);
        OptionsButtonUI.SetActive(false);
        paused = false;
        Time.timeScale = 1;
    }

    private void PauseMenu()
    {
        if (!paused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            OptionsButtonUI.SetActive(false);
            paused = true;
            PauseMenuUI.SetActive(true);
        }
        else
        {
            paused = false;
            PauseMenuUI.SetActive(false);
            OptionsButtonUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitButton()
    {
        Application.Quit();
    }
    private void OnDisable()
    {
        GameManager.Pause -= PauseMenu;
    }
}