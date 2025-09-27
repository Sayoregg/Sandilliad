using UnityEngine;

public class Blowing : MonoBehaviour
{
    [SerializeField]
    private GameObject _blowedSand;

    [SerializeField]
    private int _addForceMultiplier = 800;

    private bool _hasAddedForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (ValueManager.IsBlowing)
        {
            _blowedSand = Instantiate(_blowedSand, transform.position, Quaternion.identity);
            _hasAddedForce = false;

        }
        if (_hasAddedForce == false&&_blowedSand!=null)
        {
            _blowedSand.GetComponent<Rigidbody>().AddForce(transform.forward*800);
            _hasAddedForce = true;
        }

    }
}
