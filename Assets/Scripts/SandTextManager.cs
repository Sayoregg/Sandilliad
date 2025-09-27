using NUnit.Framework;
using System.Linq;
using TMPro;
using UnityEngine;

public class SandTextManager : MonoBehaviour
{
    private TextMeshProUGUI text;
    private int sandNumber;
    public int sandCollected;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        sandNumber = GameObject.FindGameObjectsWithTag("Sand").Count();
        sandCollected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = $"{sandCollected}" + "/" + $"{sandNumber}";
    }
}
