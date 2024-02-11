using UnityEngine;

public class KeyDoorController : MonoBehaviour
{
    public string keyTagPrefix = "Key";
    public string doorTagPrefix = "Door";

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            string keyTag = gameObject.tag;

            string doorTag = keyTag.Replace(keyTagPrefix, doorTagPrefix);

            GameObject door = GameObject.FindGameObjectWithTag(doorTag);

            if (door != null)
            {
                Destroy(gameObject);
                Destroy(door);
            }
            else
            {
                Debug.LogWarning("Door not found for key: " + keyTag);
            }
        }
    }
}
