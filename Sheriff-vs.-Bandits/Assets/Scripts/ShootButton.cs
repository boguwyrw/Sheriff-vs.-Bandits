using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private RectTransform background = null;
    [SerializeField]
    private Rigidbody bulletPrefab = null;
    [SerializeField]
    private Transform shootPoint = null;

    private Vector3 target;
    private float playerTime = 0.5f;
    private Vector3 bulletVelocity;
    private Vector3 result;
    private Rigidbody clone;
    private RaycastHit raycastHit;
    private float range = 50.0f;
    private AudioSource revolverAudioSource;

    private void Start()
    {
        revolverAudioSource = GetComponent<AudioSource>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        clone = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        clone.velocity = bulletVelocity;
        revolverAudioSource.Play();
    }

    private void Update()
    {
        Shooting();
    }

    private void Shooting()
    {
        Vector3 centerPoint = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        if (Physics.Raycast(centerPoint, Camera.main.transform.forward, out raycastHit, range))
        {
            target = raycastHit.point;
        }
        else
        {
            target = centerPoint + Camera.main.transform.forward * range;
        }

        bulletVelocity = CalculateVelocity(target, shootPoint.position, playerTime);
    }

    private Vector3 CalculateVelocity(Vector3 endPoint, Vector3 startPoint, float ballTime)
    {
        Vector3 vectorDistance = endPoint - startPoint;
        Vector3 vectorDistanceXZ = vectorDistance;
        vectorDistanceXZ.y = 0;

        float distanceY = vectorDistance.y;
        float distanceXZ = vectorDistanceXZ.magnitude;

        float velocityXZ = distanceXZ / ballTime;
        float velocityY = distanceY / ballTime + 0.5f * Mathf.Abs(Physics.gravity.y) * ballTime;

        result = vectorDistanceXZ.normalized;
        result *= velocityXZ;
        result.y = velocityY;

        return result;
    }
}
