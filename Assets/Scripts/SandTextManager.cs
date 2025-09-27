using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;

public class SandTextManager : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int sandNumber;
    public int sandCollected;
    public GameObject winText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        sandNumber = GameObject.FindGameObjectsWithTag("Sand").Count() -1;
        sandCollected = 0;
        winText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{sandCollected}" + "/" + $"{sandNumber}";

        if (sandNumber == sandCollected)
        {
            winText.SetActive(true);
        }
    }
}
