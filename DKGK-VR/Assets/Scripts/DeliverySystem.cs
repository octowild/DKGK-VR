using UnityEngine;

public class DeliverySystem : MonoBehaviour
{

    public bool _outForDelivery=false;
    public bool _BeaconSpawned = false;

    public GameObject _CurrentBeacon;
    public BeaconArea _beaconS;

    public GameObject _DeliveryPickupBeaconPrefab;
    public GameObject _DeliveryDropoffBeaconPrefab;


    void Start()
    {
        
    }



    void Update()
    {
        if (!_BeaconSpawned)
        {
            if (!_outForDelivery) 
            { 
                _CurrentBeacon = Instantiate(_DeliveryPickupBeaconPrefab);
            }
            if (_outForDelivery) 
            {
                _CurrentBeacon = Instantiate(_DeliveryDropoffBeaconPrefab);
            }
            _beaconS = _CurrentBeacon.GetComponent<BeaconArea>();
            _BeaconSpawned = true;
        }
        if (_beaconS._trigger)
        {
            if (!_outForDelivery)
            {
                _outForDelivery = true;
            }
            else if (_outForDelivery) {
                _outForDelivery = false;
            }

            _BeaconSpawned = false;
        }
    }
}
