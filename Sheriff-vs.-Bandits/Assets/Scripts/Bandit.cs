using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : MonoBehaviour
{
    private bool banditIsDying = false;
    private AudioSource banditAudioSource;

    private void Start()
    {
        banditAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (banditIsDying)
        {
            Quaternion onGround = Quaternion.Euler(-90.0f, 180.0f, 0.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, onGround, 5 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            banditAudioSource.Play();
            banditIsDying = true;
        }
    }
}
