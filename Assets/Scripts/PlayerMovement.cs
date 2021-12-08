using SimpleInputNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerAnim))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speedOfMovement = 0;
    
    [Tooltip("Only Y and Z")] 
    [SerializeField] private Vector3 _jumpForce;
    
    [SerializeField] private Transform _rotationCalcFrom;
    [SerializeField] private Joystick _joystick;
    
    private Rigidbody _rb;
    private Vector3 _movement;

    private float _xDir;
    private float _zDir;

    private Transform _transform;
    private Vector3 _objRot;

    private PlayerAnim _playerAnim;
    private bool _canJump;
    private Vector3 _positionToStartRaycast;
    private bool _jumped = true;

    private bool _dead;
    private bool _win;
    
    private void Awake()
    {
        _transform = transform;
        _rb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<PlayerAnim>();
    }

    private void FixedUpdate()
    {
        if(_dead || _win) return;
        
        Movement();
    }

    private void Movement()
    {
        _positionToStartRaycast = _transform.position;
        _positionToStartRaycast.y += 0.95f;
        _canJump = Physics.Raycast(_positionToStartRaycast, Vector3.down, 1);
        
        if (_canJump && _joystick.Value.magnitude > 0)
        {
            Move();
            RotateObj();
        }
        else if(!_jumped && _joystick.Value.magnitude <= 0)
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (!_canJump) return;
        
        _canJump = false;
        _playerAnim.SetJump();
        
        var direction = transform.forward * _jumpForce.z + transform.up * _jumpForce.y;
        _rb.AddForce(direction, ForceMode.Impulse);
        _jumped = true;
    }
    
    private void Move()
    {
        _jumped = false;
        _xDir = _joystick.Value.x;
        _zDir = _joystick.Value.y;
        
        SetVelocity(_rotationCalcFrom.forward * _zDir + _rotationCalcFrom.right * _xDir);
    }

    private void RotateObj()
    {
        var position = _transform.position;
        _objRot = position + _movement;
        _objRot.y = position.y;
        _transform.LookAt(_objRot);
    }

    private void SetVelocity(Vector3 velocity)
    {
        _movement = velocity * _speedOfMovement;
        _movement.y = _rb.velocity.y;
        
        _playerAnim.SetMovement(_joystick.Value.magnitude);
       
        _rb.velocity = _movement;
    }

    private void OnCollisionEnter(Collision other)
    {
        SetVelocity(Vector3.zero);
        _rb.angularVelocity = Vector3.zero;
    }

    public void Die()
    {
        _dead = true;
        _playerAnim.SetDeath(_dead);
    }

    public void Win()
    {
        SetVelocity(Vector3.zero);
        _rb.angularVelocity = Vector3.zero;
        _playerAnim.SetMovement(0);
        
        _win = true;
    }

    public void Respawn(Transform respawnPoint)
    {
        _dead = false;
        transform.position = respawnPoint.position;
        _playerAnim.SetDeath(_dead);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_positionToStartRaycast, _positionToStartRaycast + Vector3.down * 1);
        Gizmos.color = Color.red;
    }
}
