using UnityEngine;

public class PlayerPhy : MonoBehaviour
{
    public Transform _playerhead;
    public CapsuleCollider _bodyCol;

    public float _bodyHeightMin = 0.5f;
    public float _bodyHeightMax = 2f;

    void Start()
    {
        
    }
    
    void FixedUpdate()
    {
        _bodyCol.height = Mathf.Clamp(_playerhead.localPosition.y, _bodyHeightMin, _bodyHeightMax);
        _bodyCol.center=new Vector3(_playerhead.localPosition.x,_bodyCol.height/2,_playerhead.localPosition.z);
    }
}
