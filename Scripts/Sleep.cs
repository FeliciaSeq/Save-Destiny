using UnityEngine;

public class Sleep : MonoBehaviour
{
    public AudioClip clip;

    private bool hasPlayed = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !hasPlayed)
        {
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.PlayOneShot(clip);
            hasPlayed = true;
        }
    }
}
