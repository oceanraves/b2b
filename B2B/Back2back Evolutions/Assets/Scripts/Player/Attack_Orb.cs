using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Orb : MonoBehaviour
{
    private Vector3 _direction;
    private float _torque;
    private float _turn;
    [SerializeField] private float _throwPower;
    [SerializeField] private float _upAngle;

    [SerializeField] private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InanimateObject" || other.gameObject.tag == "Enemy")
        {
            _torque = Random.Range(-150f, 300f);
            _turn = Random.Range(20, 50f);

            _direction = other.gameObject.transform.position - player.transform.position;

            //_direction = gameObject.transform.LookAt(other.gameObject.transform.position, Vector3.zero); // Vector3. MoveTowards((other.gameObject.transform.position * -1), (player.transform.position + new Vector3(0f, _upAngle, 0f)), 50f);
                
                //other.gameObject.transform.position
                //+ player.transform.position + player.transform.forward
                //+ new Vector3(0f, _upDirection, 0f);
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().AddForce(_direction * _throwPower, ForceMode.Impulse);
            other.GetComponent<Rigidbody>().AddTorque(new Vector3(100f, 1f, 0f) * _torque * _turn);
        }
    }
}
