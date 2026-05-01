using UnityEngine;

public class Killbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            other.transform.position = new Vector3 (0, 5, 0);
            other.gameObject.SetActive(true);
        }
        else
        {
            other.transform.position = new Vector3(0, 5, 0);
        }
        
        Debug.LogWarning("Uh oh! " + other.name + " has enetered the void!");
    }
}
