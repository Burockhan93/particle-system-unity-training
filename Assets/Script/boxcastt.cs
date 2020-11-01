﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxcastt : MonoBehaviour
{
    float m_MaxDistance;
    float m_Speed;
    bool m_HitDetect;
    public Material r;
    public GameObject target;
    Collider m_Collider;
    RaycastHit m_Hit;
    public Texture myTexture;


    void Start()
    {
        //Choose the distance the Box can reach to
        m_MaxDistance = 300.0f;
        m_Speed = 1.0f;
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        //Simple movement in x and z axes
        float xAxis = Input.GetAxis("Horizontal") * m_Speed;
        float zAxis = Input.GetAxis("Vertical") * m_Speed;
        transform.Translate(new Vector3(xAxis, 0, zAxis));
    }

    void FixedUpdate()
    {
        //Test to see if there is a hit using a BoxCast
        //Calculate using the center of the GameObject's Collider(could also just use the GameObject's position), half the GameObject's size, the direction, the GameObject's rotation, and the maximum distance as variables.
        //Also fetch the hit data
        m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance);
        if (m_HitDetect)
        {
            //Output the name of the Collider your Box hit
            Debug.Log("Hit : " + m_Hit.collider.name);
            Renderer rend = m_Hit.transform.GetComponent<Renderer>();
            rend.material.color=Color.white;

        }
    }

    //Draw the BoxCast as a gizmo to show where it currently is testing. Click the Gizmos button to see this
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        //Check if there has been a hit yet
        if (m_HitDetect)
        {
            //Draw a Ray forward from GameObject toward the hit
            Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
            //Draw a cube that extends to where the hit exists
            //Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
            //Gizmos.DrawGUITexture(new Rect(10, 10, 20, 20), myTexture);
            Gizmos.DrawIcon(transform.position, "Light Gizmo.tiff", true);
            Gizmos.DrawLine(transform.position, target.transform.position);

        }
        //If there hasn't been a hit yet, draw the ray at the maximum distance
        else
        {
            //Draw a Ray forward from GameObject toward the maximum distance
            Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
            //Draw a cube at the maximum distance
            Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        }
    }
}
