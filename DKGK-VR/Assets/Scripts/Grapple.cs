using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Grapple : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody Prb;
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;
    public GrappleHead _HeadS;

    public InputActionReference _ReelR;
    public InputActionReference _ReelL;

    public float _GrappleHeadSpeed = 10f;
    public float _ReelPullSpeed = 2f;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    private bool _Shooting=false;
    private bool _ready = true;

    void Start()
    {
        XRGrabInteractable _grabinteractable = GetComponent<XRGrabInteractable>();
        _grabinteractable.activated.AddListener(x=>GrappleShoot());
        _grabinteractable.deactivated.AddListener(x=>GrappleStop());
    
    }

  // add button for reeling
  //move player
    void Update()
    {
        if (!_Shooting)
        {
            _GunDir = (GrappleGun.transform.up * -1);
        }

        if (_Shooting && !_HeadS._hit)
        {
            GrappleHead.transform.position += _GunDir * _GrappleHeadSpeed * Time.deltaTime;
        }
        //get which hand its on and get respective button
        if (_ReelR.action.triggered||_ReelL.action.triggered) { 
            if(!_ready) GrappleReel();
        }


    }
    public void GrappleShoot()
    {
        if (_ready) {
            _Shooting = true;
            _ready = false;
            GrappleHead.transform.SetParent(null,true);
        }
    }
    public void GrappleStop()
    {

        //GrappleHead.transform.SetParent(GrappleGun.transform, true);

    }
    public void GrappleReel()
    {
        if (!_HeadS._hit)
        {
                _ReelDir = BarrelPos.transform.position- GrappleHead.transform.position;
                GrappleHead.transform.position += _ReelDir.normalized * _GrappleHeadSpeed * Time.deltaTime;
        }
        else if (_HeadS._hit) 
        {
            _ReelDir = GrappleHead.transform.position- BarrelPos.transform.position;
            //move player here
            //layer.transform.position+=_ReelDir*Mathf.Lerp(0f,_ReelPullSpeed,0.02f)*Time.deltaTime;
            Prb.AddForce(_ReelDir.normalized*_ReelPullSpeed*Time.deltaTime);
            
        }

        if (_ReelDir.magnitude <= 5f)
        {
            GrappleHead.transform.position = BarrelPos.transform.position;
            _HeadS._hit = false;
            _ready = true;
            _Shooting = false;
        }
        
    }
}
