using UnityEngine;

public class WeepingAngel : MonoBehaviour
{
    public GameObject Player;
    public GameObject Self;
    public GameObject PlayerCam;

    public float _speed=5;
    public float _angle = 45;
    private Vector3 _dir;
    private bool _looking;

    void Start()
    {
        
    }

    void Update()
    {
        _dir=(Player.transform.position-Self.transform.position).normalized;

        if (Vector3.Angle(PlayerCam.transform.forward, (-1 * _dir)) >= _angle)
        {
            _looking = false;
        }
        else { _looking = true; }

        if (!_looking)
        {
            Self.transform.position += _dir * _speed * Time.deltaTime;
            Self.transform.rotation = Quaternion.LookRotation(_dir);
        }
        else { Self.transform.rotation *= Quaternion.Euler(Vector3.up*1); }
    }
}
