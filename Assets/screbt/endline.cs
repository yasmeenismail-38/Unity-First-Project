using UnityEngine;

public class EndLine : MonoBehaviour
{
    public Light winLight;

    private void Start()
    {
        if (winLight != null)
        {

            winLight.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.StopPlayer();
            }


            if (winLight != null)
            {
                winLight.enabled = true;
            }
            GameManager.Instance.ShowWin();
        }
    }
}