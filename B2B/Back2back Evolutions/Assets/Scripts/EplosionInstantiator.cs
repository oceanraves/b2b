using UnityEngine;

public class EplosionInstantiator : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ExplosionTrigger")
        {
            Vector3 position = gameObject.transform.position;
            GameObject explosionClone = Instantiate(Resources.Load("Explosion_0_Slow") as GameObject, position, Quaternion.identity);
            explosionClone.transform.localScale = new Vector3(30f, 30f, 30f);
            Destroy(other.gameObject);
            Destroy(explosionClone, 3f);
            Destroy(gameObject, 3.2f);
        }
    }
}
