using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _maxHp = 1;
    [SerializeField] float _hp = 1;
    // ���E�ړ������
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _runPower = 2f;
    [SerializeField] float _smooth = 10f;
    [SerializeField] float _rotationSpeed = 1.0f;
    [SerializeField] UIManager _uiManager = null;
    // �e�평����
    Rigidbody _rb = default;
    Animator _animator = default;   
    Vector3 _latestPos = Vector3.zero;
    // x�������̓��͒l
    float _h = 0;
    // z�������̓��͒l
    float _v = 0;
    // Animator�̃n�b�V���l�����
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

        // �������͂��Ȃ��Ԃ� _elapsed �̌o�ߎ��Ԃ𑝂₷
        _elapsed += Time.deltaTime;
        if (_v != 0 || _h != 0) 
        { 
            _elapsed = 0; 
        }
        _animator.SetBool("Rest", _elapsed > 3); // 3�b�o�߂Ńt���O�𗧂Ă�
    }

    private void FixedUpdate()
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * _v + Camera.main.transform.right * _h;
        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂��B
        _rb.velocity = moveForward * _moveSpeed + new Vector3(0, _rb.velocity.y, 0);
        // �L�����N�^�[�̌�����i�s������
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
