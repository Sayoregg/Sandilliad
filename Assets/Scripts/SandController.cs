using UnityEngine;

public class SandController : MonoBehaviour
{
    public float scaleCap;
    public float sandAmount;
    private GameObject _player;
    private GameObject _sandText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _sandText = GameObject.FindGameObjectWithTag("SandText");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < scaleCap)
        {
            GetDestroyed();
        }
    }

    public void GetDestroyed()
    {
        _player.GetComponent<PlayerController>().AddSand(sandAmount);
        _sandText.GetComponent<SandTextManager>().sandCollected++;
        Destroy(gameObject);
    }
}
