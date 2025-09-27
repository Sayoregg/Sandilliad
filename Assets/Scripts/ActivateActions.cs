using UnityEngine;

public class ActivateActions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ValueManager.IsSucking = true;
        }
        else
        {
            ValueManager.IsSucking = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            ValueManager.IsBlowing = true;
        }
        else
        {
            ValueManager.IsBlowing = false;
        }
    }
}
