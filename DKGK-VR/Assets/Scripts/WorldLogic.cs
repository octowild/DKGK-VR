using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class WorldLogic : MonoBehaviour
{
    public string[] _Inv;

    public GameObject _Player;
    public GameObject _Rightcontroller;
    public GameObject _Leftcontroller;
    public XRDirectInteractor _LeftDI;
    public XRDirectInteractor _RightDI;

    public InputActionReference _TrigR;
    public InputActionReference _TrigL;
    public InputActionReference _ReelR;
    public InputActionReference _ReelL;
    public InputActionReference _ReleaseR;
    public InputActionReference _ReleaseL;

    //prefabs
    public int _wepcount = 0;
    public bool _dual;
    public bool _cansp=false;
    public bool _spawnonce = true;
    public bool _selectonce = true;
    public GameObject _priwep;
    public GameObject _secwep;
    public GameObject _currpriwep;
    public GameObject _currsecwep;
    public GameObject _Dualgun;
    public GameObject _RocketGun;



    public InputActionReference _RGrip;

    public InputActionReference _RightStick;
    public Vector2 _RS;
 
    private IXRSelectInteractable _prigrabI;
    private IXRSelectInteractable _secgrabI;
    private Grapple _priScript;
    private Grapple _secScript;
    void Update()
    {
        //_Inv.Append("Dual Guns");

        _RS = _RightStick.action.ReadValue<Vector2>();
        if (_RS.x<=-0.5&&_selectonce)
        {
            _wepcount += 1;
            _selectonce = false;
        }
        else if (_RS.x >= 0.5&&_selectonce) { 
            _wepcount -= 1;
            _selectonce=false;
        }
        if (_RS.x == 0f) { _selectonce = true; }
        switch (_wepcount)
        {
            case 0:
                _priwep = null;
                _secwep = null;
                _dual = false;
                _cansp = false;
                break;
            case 1:
                _priwep = _Dualgun;
                _secwep = _Dualgun;
                _dual = true;
                _cansp = true;
                break;
            case 2:
                _priwep = _RocketGun;
                _secwep = null;
                _dual = false;
                _cansp = true;
                break;
            case 3:
                _priwep = _Dualgun;
                _secwep = null;
                _dual = false;
                _cansp = true;
                break;

            default:
                _priwep=null;
                _secwep=null;
                _dual=false;
                _cansp=false;
                break;
        }
        if (_wepcount < 0)
        {
            _wepcount = 0;
            _cansp = false;
        }
        else if (_wepcount >2)
        {
            _wepcount = 2;
            _cansp = false;
        }
        if (_RS.x <= -0.5|| _RS.x >= 0.5 && _cansp && !_spawnonce)
        {
            Destroy(_currpriwep);
            Destroy(_currsecwep);
            _spawnonce = true;
        }

        if (_RS.x <= -0.5 || _RS.x >= 0.5 && _cansp&&_spawnonce)
        {
            _currpriwep=Instantiate(_priwep,_RightDI.attachTransform.position,_RightDI.attachTransform.rotation);
            _prigrabI = _currpriwep.GetComponent<XRGrabInteractable>();
            _RightDI.StartManualInteraction(_prigrabI);
            _priScript=_currpriwep.GetComponent<Grapple>();
            _priScript._Trig = _TrigR;
            _priScript._Reel = _ReelR;
            _priScript._Release = _ReleaseR;
            if (_dual) { 
                _currsecwep = Instantiate(_secwep, _LeftDI.attachTransform.position, _LeftDI.attachTransform.rotation);
                _secgrabI = _currsecwep.GetComponent<XRGrabInteractable>();
                _LeftDI.StartManualInteraction(_secgrabI);
                _secScript=_currsecwep.GetComponent<Grapple>();
                _secScript._Trig = _TrigL;
                _secScript._Reel = _ReelL;
                _secScript._Release = _ReleaseL;
            }
            _spawnonce = false;
        }

    }

}
