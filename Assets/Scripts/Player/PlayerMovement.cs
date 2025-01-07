using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] private float movementSpeed = 15;
    [SerializeField] private float movementDeceleration = 5;
    [SerializeField] private float movementAcceleration = 1.5f;
    [SerializeField] private float velocityMaxValue = 10;
    [Header("Camera Variables")] 
    [SerializeField] private float mouseSensitivity = 3;
    [SerializeField] private float verticalLookMaxValue = 90f;
    [SerializeField] private GameObject cameraPivot;
    [SerializeField] private float cameraDamping = 10;
    [Header("Jump Variables")] 
    [SerializeField] private float jumpHeight = 3;
    [SerializeField] private float fallMultiplier = 2.5f;
    [Header("Sprint Variables")]
    [SerializeField] private float sprintSpeed = 25f;
    [SerializeField] private float sprintVelocityMaxValue = 8;
    [SerializeField] private float sprintAcceleration = 2.5f;
    [SerializeField] private float sprintDeceleration = 2.5f;


    private float _xRotation;
    private float _yRotation;
    private float _defaultSpeed;
    private float _defaultVelocityMaxValue;
    private Vector3 _velocity;
    private Camera _camera;
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _camera = Camera.main;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _defaultSpeed = movementSpeed;
        _defaultVelocityMaxValue = velocityMaxValue;
    }

    void Update()
    {
        Move();
        Jump();
        Sprint();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    private void Move()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;
        moveDirection = moveDirection.normalized * movementSpeed;

        if (_characterController.isGrounded)
        {
            _velocity.x = Mathf.Lerp(_velocity.x, moveDirection.x, movementAcceleration * Time.deltaTime);
            _velocity.z = Mathf.Lerp(_velocity.z, moveDirection.z, movementAcceleration * Time.deltaTime);
            if (moveHorizontal == 0 && moveVertical == 0)
            {
                _velocity.x = Mathf.Lerp(_velocity.x, 0, movementDeceleration * Time.deltaTime);
                _velocity.z = Mathf.Lerp(_velocity.z, 0, movementDeceleration * Time.deltaTime);
            }
        }
        else
        {
            _velocity.x = moveDirection.x;
            _velocity.z = moveDirection.z;
        }

        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (_characterController.isGrounded && Input.GetKey(KeyCode.Space))
        {
            _velocity.y = jumpHeight;
        }
        else if (!_characterController.isGrounded || Input.GetKeyUp(KeyCode.Space))
        {
            _velocity.y += Physics.gravity.y * fallMultiplier * Time.deltaTime;
        }
    }

    private void Sprint()
    {
        bool isSpring = Input.GetKey(KeyCode.LeftShift);
        if (isSpring)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, sprintAcceleration * Time.deltaTime);
            velocityMaxValue = sprintVelocityMaxValue;  
        }       

        else
        {
            movementSpeed = _defaultSpeed;
            velocityMaxValue = Mathf.Lerp(velocityMaxValue, _defaultVelocityMaxValue,
                sprintDeceleration * Time.deltaTime);
        }
    }

    private void CameraRotation()
    {
        _camera.transform.position = Vector3.Lerp(_camera.transform.position,cameraPivot.transform.position,cameraDamping*Time.deltaTime);
        // _camera.transform.position = cameraPivot.transform.position;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;         
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;          
        _xRotation -= mouseY; 
        _yRotation += mouseX;
        _xRotation = Mathf.Clamp(_xRotation, -verticalLookMaxValue, verticalLookMaxValue);
        transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + mouseX, 0);
        _camera.transform.localRotation = Quaternion.Euler(_xRotation, _yRotation, 0f);
    }
}