using UnityEngine;

public class Bow : MonoBehaviour
{

    public weaponHandler _handler;
    public GameObject _Ashelf;
    public GameObject _Anock;
    public WorldLogic _worldlogic;
    public GameObject _Arrow;
    public LineRenderer _LineR;
    public Transform _BowTop;
    public Transform _BowBot;
    void Start()
    {
        _worldlogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        _Arrow = _worldlogic._currpriwep;
        _LineR.positionCount = 3;
    }

    void Update()
    {
        DrawRope();
    }

    public void DrawRope()
    {
        _LineR.SetPosition(0, _BowTop.transform.position);
        _LineR.SetPosition(1, _Anock.transform.position);
        _LineR.SetPosition(2, _BowBot.transform.position);
    }

}
