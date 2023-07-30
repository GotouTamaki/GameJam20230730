using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHp = 1;
    [SerializeField] float _hp = 1;
    // 左右移動する力
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _runPower = 2f;
    [SerializeField] float _smooth = 10f;
    [SerializeField] float _rotationSpeed = 1.0f;
    [SerializeField] UIManager _uiManager = null;
    // 各種初期化
    Rigidbody _rb = default;
    Animator _animator = default;   
    Vector3 _latestPos = Vector3.zero;
    // x軸方向の入力値
    float _h = 0;
    // z軸方向の入力値
    float _v = 0;
    // Animatorのハッシュ値を取る
   static readonly int SpeedHash = Animator.StringToHash("Speed");
    //float _idleTime;
    float _elapsed;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        //_uiManager = GetComponent<UIManager>();
        _elapsed = 0;
        // _idleTime = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        _h = Input.GetAxis("Horizontal");
        _v = Input.GetAxis("Vertical");
        _animator.SetFloat("Speed", _v);
        _animator.SetFloat("Direction", _h);

        if (_h == 0 && _v == 0)
        {
            //_animator.Play("Wait");
            //_animator.SetFloat("Velocity", 0);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeed * _runPower;
            //_animator.Play("Run");
            //_animator.SetFloat("Velocity", _moveSpeed);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = _moveSpeed / _runPower;
            //_animator.Play("Walk");
            //_animator.SetFloat("Velocity", _moveSpeed);
        }

        // 何も入力がない間は _elapsed の経過時間を増やす
        _elapsed += Time.deltaTime;
        if (_v != 0 || _h != 0) 
        { 
            _elapsed = 0; 
        }
        _animator.SetBool("Rest", _elapsed > 3); // 3秒経過でフラグを立てる
    }

    private void FixedUpdate()
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * _v + Camera.main.transform.right * _h;
        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す。
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
        // キャラクターの向きを進行方向に
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward * _rotationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            _uiManager.Money++;
            Destroy(other.gameObject);
        }
    }
}
