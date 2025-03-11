using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{
    public WorldLogic _worldLogic;
    public TextMeshPro _package;
    public TextMeshPro _speedOMeter;

    void Start()
    {
        _worldLogic = GameObject.FindGameObjectWithTag("Logic").GetComponent<WorldLogic>();
    }

    void Update()
    {
        
    }
}
