using UnityEngine;

public class Bow : MonoBehaviour
{

    public weaponHandler _handler;
    public GameObject _Ashelf;
    public GameObject _Anock;
    public WorldLogic _worldlogic;
    public GameObject _Arrow;
    void Start()
    {
        _worldlogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
        _Arrow = _worldlogic._currpriwep;
    }

    void Update()
    {
        
    }
}
