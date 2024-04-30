using System;
using System.Collections.Generic;
using Game.Scripts.Events;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CameraToggle : MonoBehaviour
{
    [SerializeField] private List<CameraData> _cameras = new List<CameraData>();

    [SerializeField] private EventBool OnMove;
    [SerializeField] private EventBool OnFinish;

    private void OnEnable()
    {
        OnMove.OnEvent += Moving;
        OnFinish.OnEvent += Finish;
    }

    private void Moving(bool value)
    {
        ResetAllCameras();
        _cameras.FirstOrDefault(cameraData => cameraData.Type == CameraData.CameraType.Run)
            .Camera.Priority = 10;
    }

    private void Finish(bool value)
    {
        ResetAllCameras();
        _cameras.FirstOrDefault(cameraData => cameraData.Type == CameraData.CameraType.Finish)
            .Camera.Priority = 10;
    }

    private void ResetAllCameras()
    {
        foreach (var camera in _cameras)
        {
            camera.Camera.Priority = 0;
        }
    }

    private void OnDisable()
    {
        OnMove.OnEvent -= Moving;
        OnFinish.OnEvent -= Finish;
    }
}

[Serializable]
public struct CameraData 
{
    public enum CameraType
    {
        Start,
        Run,
        Finish
    }
    public CinemachineVirtualCamera Camera;
    public CameraType Type;
}
