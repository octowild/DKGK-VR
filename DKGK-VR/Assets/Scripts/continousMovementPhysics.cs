using UnityEngine;
using UnityEngine.InputSystem;

public class continousMovementPhysics : MonoBehaviour
{

    public float _WalkSpeed = 1;
    public InputActionReference _walkInput;
    public Rigidbody _rb;
    public CapsuleCollider _bodyCol;

    public LayerMask _GroundLayer;

    public Transform _DirSrc;

    public bool _IsGrounded = false;

    private Vector2 _walkAxis;

    void Start()
    {
        
    }

    void Update()
    {
        _walkAxis=_walkInput.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        _IsGrounded = GroundCheck();
        if (_IsGrounded)
        {
            Quaternion yaw = Quaternion.Euler(0, _DirSrc.eulerAngles.y, 0);
            Vector3 _dir = yaw * new Vector3(_walkAxis.x, 0, _walkAxis.y);
            _rb.MovePosition(_rb.position + _dir * _WalkSpeed * Time.fixedDeltaTime);
        }
    }

    public bool GroundCheck()
    {
        Vector3 _start = _bodyCol.transform.TransformPoint(_bodyCol.center);
        float _raylen = _bodyCol.height / 2-_bodyCol.radius+0.05f;
        bool _hashit = Physics.SphereCast(_start, _bodyCol.radius, Vector3.down, out RaycastHit hitInfo, _raylen,_GroundLayer);
        return _hashit;
    }

}
