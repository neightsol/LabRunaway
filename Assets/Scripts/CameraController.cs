using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, IManagement
{
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        GameManager.Manager.AddManaged(this);
    }

    public void MFixedUpdate()
    {
        
    }

    public void MLateUpdate()
    {
        
    }

    public void MUpdate()
    {
        
    }

    public void MPaused(bool pause)
    {
        if (pause)
        {
            GetComponent<Cinemachine.CinemachineFreeLook>().enabled = false;
        } else
        {
            GetComponent<Cinemachine.CinemachineFreeLook>().enabled = true;
        }
    }
}
