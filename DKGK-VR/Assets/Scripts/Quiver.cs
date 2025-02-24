using UnityEngine;

public class Quiver : MonoBehaviour
{

    public weaponHandler _handler;
    public GameObject _bow;
    public WorldLogic _worldlogic;
    public GameObject _drawnarrow;
    public Transform _attach;
    public GameObject _arrowprefab;


    public bool _drawarrow=false;
    void Start()
    {
        _worldlogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        _bow=_worldlogic._currsecwep;
    }


    void Update()
    {
        //if (_drawarrow) DrawArrow();
    }

    public void DrawArrow()
    {
        _drawnarrow=Instantiate(_arrowprefab,_attach);
    }

    public void RotateArrow(Transform _start,Transform _end)
    {

    }
}
