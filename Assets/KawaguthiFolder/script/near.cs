using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class near : MonoBehaviour
{
    public bool _isEnemyMove;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isEnemyMove = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isEnemyMove = false;
        }
    }
}
