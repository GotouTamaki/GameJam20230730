using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingArea : MonoBehaviour
{

    [SerializeField] private Animator _animator;

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
        _animator.SetBool("Flag", true);
    }

    private void OnTriggerExit(Collider other)
    {
        _animator.SetBool("Flag", false);
    }
}
