using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObjects : MonoBehaviour
{
    //Objecto a Agarrar 
    public GameObject ObjectToPickUp;

    //objecto Agarrado
    public GameObject PickedObject;

    //Zona que mantiene el objecto agarrado (lo unico que tendra puesto en el player)[unico a poner algo en escena]
    public Transform interactionZone;

   

    void Update()
    {
        if (ObjectToPickUp!=null && ObjectToPickUp.GetComponent<PickableObject>().isPickable == true && PickedObject == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                //ya agarramos el objecto
                PickedObject = ObjectToPickUp;
                //le decimos que no puede ser agarrado de nuevo (ya que lo tenemos ensima)
                PickedObject.GetComponent<PickableObject>().isPickable = false;

                //ya es hijo de InteractionZone
                PickedObject.transform.SetParent(interactionZone);

                //setear el objeto en la posicion de la ZONA INTERACION
                PickedObject.transform.position = interactionZone.position;

                //ya no le afecta la gravedad
                PickedObject.GetComponent<Rigidbody>().useGravity = false;

                //se hace kinematico osea no tiene accion con el mundo que lo rodea (no afecta la fisica)
                PickedObject.GetComponent<Rigidbody>().isKinematic = true;
            }
            else if (PickedObject != null)
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    
                    //se puede agarrar de nuevo
                    PickedObject.GetComponent<PickableObject>().isPickable = true;

                    //ya no tiene Padre
                    PickedObject.transform.SetParent(null);

                    //ya no le afecta la gravedad
                    PickedObject.GetComponent<Rigidbody>().useGravity = true;

                    //se hace kinematico osea no tiene accion con el mundo que lo rodea
                    PickedObject.GetComponent<Rigidbody>().isKinematic = false;

                    //no tenemos nada agarrado
                    PickedObject = null;

                }
            }



        }



    }
}
