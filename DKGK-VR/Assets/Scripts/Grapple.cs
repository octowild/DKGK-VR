using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Hands;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Grapple : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody Prb;
    public GameObject GrappleGun;
    public GameObject GrappleHead;
    public GameObject BarrelPos;
    public GrappleHead _HeadS;

    private XRGrabInteractable _grabinteractable;

    public InputActionReference _ReelR;
    public InputActionReference _ReelL;

    public float _GrappleHeadSpeed = 10f;
    public float _ReelPullSpeed = 2f;

    private Vector3 _GunDir;
    private Vector3 _ReelDir;
    private bool _Shooting=false;
    private bool _ready = true;
    private int _RH = 0;

    void Start()
    {
        _grabinteractable = GetComponent<XRGrabInteractable>();
        _grabinteractable.activated.AddListener(x=>GrappleShoot());
        _grabinteractable.deactivated.AddListener(x => GrappleStop());
        _grabinteractable.selectEntered.AddListener(OnGrab);
        _grabinteractable.selectExited.AddListener(x=>OnRelease());
    
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
        //if (_grabinteractable.holdingHand == XRHand.Right){ }
        if (_ReelR.action.triggered||_ReelL.action.triggered) {
            if (!_ready) { GrappleReel(); }
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
    public void OnRelease()
    {
        _RH = 0;
    }


    private void OnGrab(SelectEnterEventArgs args)
    {
        XRBaseInteractor currentInteractor = (XRBaseInteractor)args.interactorObject;

        if (currentInteractor.CompareTag("LeftHand"))
        {
            _RH = 2;
        }
        else if (currentInteractor.CompareTag("RightHand"))
        {
            _RH = 1;
        }
    }
}
