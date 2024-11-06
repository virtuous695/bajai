using UnityEngine;

public class CarSoundController : MonoBehaviour
{
    public AudioSource idleSound;
    public AudioSource gasSound;

    void Update()
    {
        // Jika tombol "W" ditekan
        if (Input.GetKey(KeyCode.W))
        {
            if (!gasSound.isPlaying)
            {
                gasSound.Play();    // Mainkan suara gas
                idleSound.Stop();    // Hentikan suara idle
            }
        }
        else
        {
            if (!idleSound.isPlaying)
            {
                idleSound.Play();    // Mainkan suara idle
                gasSound.Stop();      // Hentikan suara gas
            }
        }
    }
}
