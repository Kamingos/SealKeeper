
using UnityEngine;

public class FoodBin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;

        if (rb != null && rb.CompareTag("Food"))
        {
            Destroy(rb.gameObject);
            Debug.Log("Food попал в цилиндр!");
        }
    }
}
