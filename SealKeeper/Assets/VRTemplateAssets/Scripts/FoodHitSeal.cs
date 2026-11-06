
using UnityEngine;

public class FoodHitSeal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Seal")
        {
            Destroy(gameObject);
        }
    }
}
