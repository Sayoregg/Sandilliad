using Unity.VisualScripting;
using UnityEngine;

public class GuzzlerColliderController : MonoBehaviour
{
    public bool isColliding;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionStay(Collision collision)
    {
    }
}
