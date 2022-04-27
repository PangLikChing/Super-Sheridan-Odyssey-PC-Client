using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    public float grabRange = 20.0f;
    public float grabRadius = 5.0f;
    public float grabSpeed = 50.0f;
    public float throwForce = 50.0f;
    public LayerMask throwableLayer;
    public ObjectAnchor objectAnchor;

    private RaycastHit hit;
    private Transform cameraPosition;
    private GameObject grabbedObject;
    private GameObject grabbingObject;
    private Rigidbody objectRb;
    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Instance.inputHandler.attack.AddListener(Attack);
        //objectAnchor.ObjectGrabbed.AddListener(OnObjectGrabbed);
        cameraPosition = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbingObject != null)
        {
            grabbingObject.transform.position = Vector3.Lerp(grabbingObject.transform.position,objectAnchor.transform.position,grabSpeed*Time.deltaTime);
        }
    }

    private void Attack()
    {
        if (grabbedObject == null && grabbingObject == null)
        {
            Physics.SphereCast(cameraPosition.position, grabRadius, cameraPosition.forward, out hit, grabRange, throwableLayer, QueryTriggerInteraction.Ignore);
            
            if (hit.collider != null)
            {
                if (hit.collider.GetComponent<Rigidbody>() != null)
                {
                    grabbingObject = hit.collider.gameObject;
                    objectRb = hit.collider.GetComponent<Rigidbody>();
                    objectAnchor.ObjectGrabbed.AddListener(OnObjectGrabbed);
                }
                
            }
        }
        else
        {
            objectRb.isKinematic = false;
            grabbedObject.GetComponent<Rigidbody>().AddForce(cameraPosition.forward*throwForce,ForceMode.Impulse);
            grabbedObject.GetComponent<Rigidbody>().AddTorque(cameraPosition.right * throwForce, ForceMode.Impulse);
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
            
        }
    }

    public void DetachObject()
    {
        if (grabbedObject != null && objectRb != null)
        {
            objectRb.isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
    }

    private void OnObjectGrabbed()
    {
        grabbingObject.GetComponent<PhotonView>().TransferOwnership(PhotonNetwork.LocalPlayer);
        grabbingObject.transform.SetParent(objectAnchor.transform);
        objectRb.isKinematic = true;
        grabbedObject = grabbingObject;
        grabbingObject = null;
        objectAnchor.ObjectGrabbed.RemoveListener(OnObjectGrabbed);
    }

}
