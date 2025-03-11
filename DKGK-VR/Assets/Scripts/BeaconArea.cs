using UnityEngine;
using UnityEngine.Events;

public class BeaconArea : MonoBehaviour
{
    public int _P=0;
    public UnityEvent<int> _triggered;
    public LineRenderer _LR;
    public Transform _selfpos;


    public void Update()
    {
        _LR.positionCount = 2;
        _LR.SetPosition(0,_selfpos.position);
        _LR.SetPosition(1,new Vector3(_selfpos.position.x,1000, _selfpos.position.z));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6) _triggered.Invoke(_P);
    }
}
