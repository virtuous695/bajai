// Countdown.cs
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public Text countdownText; 
    public float countdownTime = 3f; // Waktu countdown dimulai dari 3

    void Start()
    {
        // Mulai countdown saat game dimulai
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float currentTime = countdownTime;

        while (currentTime > 0)
        {
            // Update teks countdown
            countdownText.text = currentTime.ToString("0");
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        // Tampilkan "GO!" saat countdown selesai
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        // Hapus teks countdown setelah "GO!"
        countdownText.text = "";

        // Aktifkan kendali kendaraan pemain setelah countdown selesai
        FindObjectOfType<CarController>().isCountdownComplete = true;

        // Aktifkan pergerakan kendaraan lawan setelah countdown selesai
        OpponentCarWaypoints[] opponentCars = FindObjectsOfType<OpponentCarWaypoints>();
        foreach (var opponentCar in opponentCars)
        {
            opponentCar.isCountdownComplete = true;
        }
    }
}
