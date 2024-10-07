using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Camera _camera;
    private Rigidbody _rigidbody;
    private float hor, ver;

    public bool OnConveirt;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(hor, 0, ver).normalized;
        Vector3 camFwd  = _camera.transform.forward;
        Vector3 camRight = _camera.transform.right;
        
        camFwd.y = 0;
        camRight.y = 0;

        Vector3 move = (moveDir.x * camRight + moveDir.z * camFwd ) * _speed;


        _rigidbody.velocity = move + new Vector3(0, _rigidbody.velocity.y, 0);


        if (moveDir != Vector3.zero)
        {

            transform.rotation = Quaternion.LookRotation(camRight * moveDir.x + camFwd * moveDir.z);
        }

        //Vector3 move = new Vector3(hor , 0, ver ).normalized;
        //_rigidbody.velocity = move * _speed + new Vector3(0, _rigidbody.velocity.y,0) ;

    }
}
