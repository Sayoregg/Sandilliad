using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SandTextManager : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int sandNumber;
    public int sandCollected;
    public GameObject winText;

    //Input actions
    private InputAction reloadAction;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        sandNumber = GameObject.FindGameObjectsWithTag("Sand").Count() -1;
        sandCollected = 0;
        winText.SetActive(false);

        //Inputs
        reloadAction = InputSystem.actions.FindAction("Crouch");
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{sandCollected}" + "/" + $"{sandNumber}";

        if (sandNumber == sandCollected)
        {
            winText.SetActive(true);

            if (reloadAction.WasPressedThisFrame()) 
            {
                SceneManager.LoadScene("MainGame");
            }
        }
    }
}
