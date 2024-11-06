using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCarWaypoints : MonoBehaviour
{
    public Transform[] waypoints; // Array untuk menyimpan waypoint     
    public float speed = 2f; // Kecepatan bajaj
    public float stoppingDistance = 0.1f; // Jarak berhenti sebelum mencapai waypoint

    private int currentWaypointIndex = 0; // Indeks waypoint saat ini
    private Transform opponentCarTransform; // Transform dari mobil lawan (bajaj)

    // Tambahkan variabel ini untuk countdown
    public bool isCountdownComplete = false;

    void Start()
    {
        // Dapatkan transform bajaj (mobil lawan)
        opponentCarTransform = transform; // Mengambil transform dari objek ini
    }

    void Update()
    {
        // Cek apakah countdown sudah selesai
        if (isCountdownComplete)
        {
            MoveToWaypoint();
        }
    }

    private void MoveToWaypoint()
    {
        // Pastikan ada waypoint yang dituju
        if (waypoints.Length == 0) return;

        // Ambil posisi waypoint saat ini
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Hitung jarak ke waypoint
        float distance = Vector3.Distance(opponentCarTransform.position, targetWaypoint.position);

        // Jika jarak cukup dekat, pindah ke waypoint berikutnya
        if (distance < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop kembali ke awal
        }
        else
        {
            // Bergerak menuju waypoint
            Vector3 direction = (targetWaypoint.position - opponentCarTransform.position).normalized;
            opponentCarTransform.position += direction * speed * Time.deltaTime;

            // Menghadapkan bajaj ke arah gerakan
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            opponentCarTransform.rotation = Quaternion.Slerp(opponentCarTransform.rotation, lookRotation, Time.deltaTime * speed);
        }
    }
}
