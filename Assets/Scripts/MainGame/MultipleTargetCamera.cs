﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    private List<Transform> targets;

    public Vector3 offset;

    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;
    
    public Vector3 velocity;
    private Camera cam;
    
    private bool _cameraUpdated = false;

    
    public void UpdateCamera()
        {
            targets = new List<Transform>();
            cam = GetComponent<Camera>();
            if (GameManager.Instance.players.Length>0)
            {
                foreach (var player in GameManager.Instance.players)
                {
                    if (player != null)
                    {
                        targets.Add(player.gameObject.transform);
                    }
                }
            }
            _cameraUpdated = true;
        }

        void LateUpdate()
        {
            if (!_cameraUpdated)
            {
                return;
            }
            Move();
            Zoom();
        }

        private void Zoom()
        {
            float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / 7f);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        }

        private float GetGreatestDistance()
        {
            var bounds = new Bounds(targets[0].position, Vector3.zero);
            for (int i = 0; i < targets.Count; i++)
            {
                bounds.Encapsulate(targets[i].position);
            }

            return bounds.size.x;
        }

        private void Move()
        {
            Vector3 centerPoint = GetCenterPoint();

            Vector3 newPosition = centerPoint+offset;

            transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
        }

        private Vector3 GetCenterPoint()
        {
            if (targets.Count == 1)
            {
                return targets[0].position;
            }
            var bounds = new Bounds(targets[0].position, Vector3.zero);
            foreach (var t in targets)
            {
                bounds.Encapsulate(t.position);
            }
            return bounds.center;
        }
}
