using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
	public GameObject camThirdPerson;
	public GameObject camArea;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camThirdPerson.SetActive(false);
            camArea.SetActive(true);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            camThirdPerson.SetActive(true);
            camArea.SetActive(false);
        }
    }
}
