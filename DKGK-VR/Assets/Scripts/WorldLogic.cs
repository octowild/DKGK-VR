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
    public bool _dual=false;
    public bool _cansp=false;
    public bool _spawnonce = true;
    public bool _selectonce = true;
    public int _maxwep = 3;
    public GameObject _priwep;
    public GameObject _secwep;
    public GameObject _currpriwep;
    public GameObject _currsecwep;
    
    public GameObject _Dualgun;
    public GameObject _RocketGun;
    public GameObject _Bow;
    public GameObject _Quiver;


    public InputActionReference _slam;
    public InputActionReference _wep;
    public InputActionReference _RightStick;
    public Vector2 _RS;
 
    private IXRSelectInteractable _prigrabI;
    private IXRSelectInteractable _secgrabI;
    private weaponHandler _priScript;
    private weaponHandler _secScript;
    void Update()
    {
        //_Inv.Append("Dual Guns");
        /*
        _RS = _RightStick.action.ReadValue<Vector2>();
        if (_RS.x<=-0.5&&_selectonce)
        {
            if (_wepcount < _maxwep) _wepcount += 1;
            else _wepcount = 0;
            _selectonce = false;
        }
        else if (_RS.x >= 0.5&&_selectonce) { 
            if (_wepcount>0)_wepcount -= 1;
            else _wepcount = _maxwep;
            _selectonce=false;
        }
        if (_RS.x == 0f) { _selectonce = true; }
        _wepcount = 0;
        switch (_wepcount)
        {
            case 0:
                _priwep = _Dualgun;
                _secwep = _Dualgun;
                _dual = true;
                _cansp = true;
                break;
            case 1:
                _priwep = _RocketGun;
                _secwep = null;
                _dual = false;
                _cansp = true;
                break;
            case 2:
                _priwep = _Quiver;
                _secwep = _Bow;
                _dual = true;
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
        */
        _priwep = _Dualgun;
        _secwep = _Dualgun;
        _dual = true;

        if (_priScript != null)
        {
            if (_secScript != null)
            {
                if (_priScript._hookhit|| _secScript._hookhit) _cansp = false;  
                else _cansp = true;
            }
            else
            {
                if (_priScript._hookhit)_cansp = false;
                else _cansp = true;
            }
        }
/*
        if (_wep.action.triggered && _cansp && !_spawnonce)
        {
            if (_priScript!=null)_priScript.Reset();
            if (_secScript!= null) { _secScript.Reset(); }
            Destroy(_currpriwep);
            //_priScript = null;
            Destroy(_currsecwep);
            //_secScript = null;
            _spawnonce = true;
        }
*/

        if (_wep.action.triggered && _spawnonce)
        {
            _currpriwep=Instantiate(_priwep,_RightDI.attachTransform.position,_RightDI.attachTransform.rotation);
            _prigrabI = _currpriwep.GetComponent<XRGrabInteractable>();
            _RightDI.StartManualInteraction(_prigrabI);
            _priScript=_currpriwep.GetComponent<weaponHandler>();
            _priScript._Trig = _TrigR;
            _priScript._Reel = _ReelR;
            _priScript._Release = _ReleaseR;
            if (_dual) { 
                _currsecwep = Instantiate(_secwep, _LeftDI.attachTransform.position, _LeftDI.attachTransform.rotation);
                _secgrabI = _currsecwep.GetComponent<XRGrabInteractable>();
                _LeftDI.StartManualInteraction(_secgrabI);
                _secScript=_currsecwep.GetComponent<weaponHandler>();
                _secScript._Trig = _TrigL;
                _secScript._Reel = _ReelL;
                _secScript._Release = _ReleaseL;
            }
            _spawnonce = false;
        }

    }

}
