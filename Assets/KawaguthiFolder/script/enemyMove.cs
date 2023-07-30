using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyMove : MonoBehaviour
{
    /// <summary>移動速度</summary>
    [SerializeField] float _speed = 1f;
    /// <summary>プレイヤーよりどれくらい上で動きを変化するか</summary>
    [SerializeField] float _playerOffsetY = 5f;
    /// <summary>カーブする時にかける力</summary>
    [SerializeField] float _chasingPower = 1f;
    Rigidbody _rb;
    GameObject _player;
    [SerializeField] Animator _animator;
    /// <summary>曲がる方向</summary>
    bool _isMove;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isMove)
        {
            //プレイヤーとある程度近づいたら
            Vector3 v = _player.transform.position - this.transform.position;
            v = v.normalized * _speed;
            _rb.velocity = v;
            v.y = 0;
            transform.rotation = Quaternion.LookRotation(v);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.Play("UD_infantry_07_attack_A");
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _animator.Play("UD_infantry_05_combat_idle");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // プレイヤーがいない時は何もしない
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.Play("UD_infantry_06_combat_walk");
            _isMove = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.Play("UD_infantry_05_combat_idle");
            _isMove = false;
        }
    }
}
