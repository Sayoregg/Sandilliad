using UnityEngine;

public class SandController : MonoBehaviour
{
    public float scaleCap;
    public float sandAmount;
    private GameObject _player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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
        Destroy(gameObject);
    }
}
