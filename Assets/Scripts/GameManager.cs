using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Manager { get { return _instance; } }

    private List<IManagement> managedObjects = new List<IManagement>();

    public bool pause = false;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
            foreach (IManagement item in managedObjects)
            {
                item.MPaused(pause);
            }
        }

        if (!pause)
        {
            foreach (IManagement item in managedObjects)
            {
                item.MUpdate();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!pause)
        {
            foreach (IManagement item in managedObjects)
            {
                item.MFixedUpdate();
            }
        }
    }


    public void AddManaged(IManagement item)
    {
        managedObjects.Add(item);
    }

}
