using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrajectoryPredictor))]
public class Shooter : MonoBehaviour
{
    public GameObject bulletStartPos;
    public GameObject hitPointPredictor;
    public GameObject bulletPrefab;
    public float bulletForce;
    public TrajectoryPredictor tp;

    public void Awake()
    {
        tp = GetComponent<TrajectoryPredictor>();
        tp.predictionType = TrajectoryPredictor.predictionMode.Prediction3D;
        tp.drawDebugOnPrediction = true;
    }

    public void Update()
    {
        tp.Predict3D(bulletStartPos.transform.position, bulletStartPos.transform.forward * bulletForce, Physics.gravity);
        RaycastHit raycastHit = tp.hitInfo3D;
        hitPointPredictor.transform.position = raycastHit.point;

        float dotProd = Vector3.Dot(bulletStartPos.transform.forward * bulletForce, raycastHit.normal); // prediect next arch
        Vector3 reflectionVector = bulletStartPos.transform.forward * bulletForce - 2f * dotProd * raycastHit.normal;
        //reflectionVector = reflectionVector * bulletForce;
        hitPointPredictor.GetComponent<TrajectoryPredictor>().Predict3D(raycastHit.point, reflectionVector, Physics.gravity);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject currentBullet = Instantiate(bulletPrefab, bulletStartPos.transform.position, Quaternion.identity);
            Rigidbody rb = currentBullet.GetComponent<Rigidbody>();
            rb.velocity = bulletStartPos.transform.forward * bulletForce;
        }

        if (Input.GetKey(KeyCode.E))
        {
            bulletForce -= 0.04f;
        }

        if (Input.GetKey(KeyCode.R))
        {
            bulletForce += 0.04f;
        }

    }




}
