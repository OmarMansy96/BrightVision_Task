using DG.Tweening;
using UnityEngine;

public class Doors : MonoBehaviour
{

    public bool isOpen;
    public bool pull;
    public float openAngle;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isOpen)
        {
            isOpen = true;
            transform.DORotate(new Vector3(0, (pull? -openAngle : openAngle), 0), 2);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && isOpen)
        {
            isOpen = false;
            transform.DORotate(new Vector3(0, 0, 0), 2);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player") && !isOpen)
    //    {
    //        Debug.Log("Enter");

    //        isOpen = true;
    //        transform.DORotate(new Vector3(0, 88, 0), 2);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.collider.CompareTag("Player") && isOpen)
    //    {
    //        Debug.Log("Exit");
    //        isOpen = false;
    //        transform.DORotate(new Vector3(0, 0, 0), 2);
    //    }
    //}
}
