using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private Transform _target;
    [SerializeField] private float _heightOffset;
    [SerializeField] private float _followOffset;
    [SerializeField] private KeyCode _buttonToLookAround = KeyCode.Mouse1;
    [SerializeField] private Joystick _joystick;

    private float _mouseSensitivityY = 2f;
    private float _mouseSensitivityX = 2f;
    private const float LOW_LIMIT = 0.0f;
    private const float HIGH_LIMIT = 85.0f;

    private bool _isPhone;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        _isPhone = YG.YandexGame.EnvironmentData.isMobile;
    }

    private void Update()
    {
        _camera.transform.position = _target.position + new Vector3(0, _heightOffset, 0);
        _camera.transform.position -= _camera.transform.forward * _followOffset;

        if (_isPhone)
        {
            _camera.transform.position = _target.position + new Vector3(0, _heightOffset, 0);

            Vector2 cameraMovement = new Vector2(_joystick.Horizontal, -_joystick.Vertical);


            _camera.transform.eulerAngles = new Vector3(
                Mathf.Clamp(_camera.transform.eulerAngles.x + cameraMovement.y * _mouseSensitivityY, LOW_LIMIT, HIGH_LIMIT)
                , _camera.transform.eulerAngles.y + cameraMovement.x * _mouseSensitivityX, 0);

            _camera.transform.position -= _camera.transform.forward * _followOffset;
        }
        else
        {
            if (Input.GetKey(_buttonToLookAround))
            {
                HideCursor();
                _camera.transform.position = _target.position + new Vector3(0, _heightOffset, 0);

                Vector2 cameraMovement = new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));


                _camera.transform.eulerAngles = new Vector3(
                    Mathf.Clamp(_camera.transform.eulerAngles.x + cameraMovement.y * _mouseSensitivityY, LOW_LIMIT, HIGH_LIMIT)
                    , _camera.transform.eulerAngles.y + cameraMovement.x * _mouseSensitivityX, 0);

                _camera.transform.position -= _camera.transform.forward * _followOffset;
            }
            if (Input.GetKeyUp(_buttonToLookAround))
            {
                UnHideCursor();
            }
        }
    }
    private void HideCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;   
        Cursor.visible = false;

    }

    private void UnHideCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ChangeSensitivityY(float value) => _mouseSensitivityY = value;
    public void ChangeSensitivityX(float value) => _mouseSensitivityX = value;
    public void ChangeFollowOffset(float value) => _followOffset = value;
    public void ChangeLookButton(KeyCode keyCode) => _buttonToLookAround = keyCode;
}
