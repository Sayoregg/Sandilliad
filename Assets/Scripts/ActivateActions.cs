using UnityEngine;

public class ActivateActions : MonoBehaviour
{

    [SerializeField]
    private GameObject _sandParticlePrefab;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
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

        if (Input.GetMouseButtonDown(1) && _player.GetComponent<PlayerController>().sandAmount >= 10f)
        {
            ValueManager.IsBlowing = true;
            //Particle
            GameObject sandProjectile = Instantiate(_sandParticlePrefab, transform.position, Quaternion.identity);
            sandProjectile.GetComponent<Rigidbody>().AddForce(transform.forward * 800);

            //Player
            _player.GetComponent<PlayerController>().RemoveSand(10f);
            _player.GetComponent<PlayerController>().velocity -= transform.forward * 12;
        }
        else
        {
            ValueManager.IsBlowing = false;
        }
        
    }
}
