using UnityEngine;
using Unity;
using Utils;
public class PlayerMov : MonoBehaviour
{
    #region SerializaedFields

    [SerializeField] private float _moveSpeed = 5f;
        
    #endregion

    #region private Variables

    private Rigidbody2D _rigidbody2D;
    private Vector2 _movementVector2;

    private Animator _animator;
    private string _horizontalAnim = "Horizontal";
    private string _verticalAnim = "Vertical";
       
    private bool _isMoving;
    private bool _canMove = true;
        
    #endregion
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!_canMove) return;
            
        _movementVector2.Set(InputManager.MovementVector2.x, InputManager.MovementVector2.y);
        _rigidbody2D.linearVelocity = _movementVector2 * _moveSpeed;
            
        if (_movementVector2 != Vector2.zero)
        {
            _animator.SetFloat(_horizontalAnim, _movementVector2.x);
            _animator.SetFloat(_verticalAnim, _movementVector2.y);
        }
            
        _isMoving = _movementVector2 != Vector2.zero;
        _animator.SetBool("Moving", _isMoving);

    }
        
    public void EnableMovement(bool enable)
    {
        _canMove = enable;
        _rigidbody2D.linearVelocity = Vector2.zero;
        _animator.SetBool("Moving", false);
    }
    public void SetAnimToFixedDirection(Vector2 direction)
    {
        _animator.SetFloat(_horizontalAnim, direction.x);
        _animator.SetFloat(_verticalAnim, direction.y);
    }
}
