using UnityEngine;

public class PlayerPhy : MonoBehaviour
{
    public Transform _selfT;
    public Rigidbody _Rb;
    public float _gravity = 9.8f;
    public Vector3 _velocity = Vector3.zero;
    void Start()
    {
        _Rb.freezeRotation = true;
    }
    
    void Update()
    {
        _velocity += Vector3.down * _gravity;


        //_selfT.position += _velocity*Time.deltaTime;
    }
}
