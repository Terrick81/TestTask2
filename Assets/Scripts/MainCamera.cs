using UnityEngine;
using UnityEngine.EventSystems;

public class MainCamera : MonoBehaviour
{
    private const int MAX_ZOOM_DISTANCE = 80;
    private const int MIN_ZOOM_DISTANCE = 1;
    private const float  MOUSE_SENS = 15f;
    private const float DURATION = 0.3f;

    [SerializeField]
    private Transform _targetTransform; //невидимая цель для камеры
    private Camera _camera;
    
    float _scrollWheel;
    float _timeElapsed = 0;

    private bool _move = false;
    private Vector3 _startPosition;
    private Vector3 _targetPosition;
    private Vector3 _previousPosition;
    private Vector3 _newMousePosition;
    private Vector3 _offset;
    private bool _mouseStart = false;
    private bool _mouseOverOnUI = false;
    private float _distanceToTarget = 10;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _targetPosition = _camera.transform.position;
        Detail.AddListener(SetTarget);
    }

    private void MovementToPoint()
    {
        if (_move)
        {
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _timeElapsed / DURATION);
            _timeElapsed += Time.deltaTime;
            if (transform.position == _targetPosition)
            {
                _move = false;
            }

        }
    }

    private void UpdatePosition(bool immediately = false)
    {
        if (immediately)
        {
            transform.position = _targetPosition;
        }
        else
        {
            _startPosition = transform.position;
            _timeElapsed = 0;
            _move = true;
            MovementToPoint();
        }
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }


    public void SetTarget(GameObject target)
    { 
        if ( target == null)
        {
            _targetTransform = null;
        }
        else
        {
            _targetTransform = target.transform;
            _targetPosition = _targetTransform.position + new Vector3(0, 0, -_distanceToTarget);
            UpdatePosition(immediately: false);
        }
    }

    private void LookingAround()
    {
        float rotationAroundYAxis = -_offset.x * 180;
        float rotationAroundXAxis = _offset.y * 180;
        if (_targetTransform)
        {
            transform.position = _targetTransform.position;
        }
        transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
        transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World);
        if (_targetTransform)
        {
            transform.Translate(new Vector3(0, 0, -_distanceToTarget));
        }
        new Vector3(0, 0, -_distanceToTarget);
        _previousPosition = _newMousePosition;
    }
    private void Move()
    {
        Detail.RemoveTarget();
        _camera.transform.Translate(new Vector3(_offset.x * MOUSE_SENS, _offset.y * MOUSE_SENS, 0));
        _previousPosition = _newMousePosition;
    }
    private void Zoom()
    {
        if (_distanceToTarget - _scrollWheel * MOUSE_SENS < MAX_ZOOM_DISTANCE)
        {
            if (_distanceToTarget - _scrollWheel * MOUSE_SENS > MIN_ZOOM_DISTANCE)
            {
                _distanceToTarget -= _scrollWheel * MOUSE_SENS;
                _camera.transform.Translate(new Vector3(0, 0, (_scrollWheel * MOUSE_SENS)));
            }
        }
    }

    void Update()
    {
        _scrollWheel = Input.GetAxis("Mouse ScrollWheel");

        if (Input.anyKeyDown)
        {
            _previousPosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            _mouseStart = false;
            _mouseOverOnUI = false;
        }
        
        if(Input.anyKey)
        {   
            _newMousePosition = _camera.ScreenToViewportPoint(Input.mousePosition);
            _offset = _previousPosition - _newMousePosition;
            if (!_mouseStart)
            {
                _mouseStart = true;
                if (IsMouseOverUI()) _mouseOverOnUI = true;
            }            
        }

        if (!_mouseOverOnUI)
        {
            if (Input.GetMouseButton(0))
                LookingAround();
            else if (Input.GetMouseButton(2))
                Move();
            else if (_scrollWheel != 0 && !IsMouseOverUI())
                Zoom();
        }
    }

}