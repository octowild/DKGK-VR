using UnityEngine;

public class DeliverySystem : MonoBehaviour
{

    public bool _outForDelivery=false;
    public bool _BeaconSpawned = false;

    public WorldLogic _WorldLogic;
    public GameObject _CurrentBeacon;
    public BeaconArea _beaconS;

    public GameObject _DeliveryPickupBeaconPrefab;
    public GameObject _DeliveryDropoffBeaconPrefab;

    private Vector3 _spawnpoint;
    private int _buffer;

    public GameObject watch;

    void Update()
    {
        _spawnpoint = new Vector3(Random.Range(-3000, 2600), 0, Random.Range(-3400, 2700));
        if (!_BeaconSpawned) SpawnBeacon();      
    }

    public void SpawnBeacon()
    {
        if (!_outForDelivery)
        {
            _CurrentBeacon = Instantiate(_DeliveryPickupBeaconPrefab,_spawnpoint,Quaternion.identity);
            _buffer = 1;
        }
        if (_outForDelivery)
        {
            _CurrentBeacon = Instantiate(_DeliveryDropoffBeaconPrefab,_spawnpoint , Quaternion.identity);
            _buffer = -1;
        }
        _beaconS = _CurrentBeacon.GetComponent<BeaconArea>();
        _beaconS._triggered.AddListener(Trig);
        _beaconS._P = _buffer;
        _BeaconSpawned = true;
    }
    public void Trig(int _Trig)
    {
        if (!_outForDelivery)
        {
            _outForDelivery = true;
        }
        else if (_outForDelivery)
        {
            _outForDelivery = false;
        }
        _WorldLogic._packageCount += _Trig;
        watch.GetComponent<AudioSource>().Pause();
        watch.GetComponent<AudioSource>().Play();
        _beaconS._triggered.RemoveListener(Trig);
        Destroy( _CurrentBeacon );
        _BeaconSpawned = false;
    }
}
