using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movSpeed, turnSpeed, airTimer, mH, mV;
    public GameObject pumpkin, cam, offSet, possessTarg;
    public bool isGrounded, isPossessing;
    public Collider bodyCollider;
    // Start is called before the first frame update
    void Start()
    {
        movSpeed = 5;
        turnSpeed = 4;
    }
    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        mH = Input.GetAxis("Horizontal");
        mV = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                isPossessing = false;
                Debug.Log("JUMPED");
                GetComponent<Rigidbody>().useGravity = true;
                GetComponent<Rigidbody>().AddForce(0, 6, 0, ForceMode.Impulse);
            }
        }
        if (isPossessing)
        {
            transform.position = possessTarg.gameObject.transform.position;
            if (mV != 0)
                possessTarg.GetComponent<PossessObj>().mainBod.GetComponent<Rigidbody>().transform.Rotate(0, mH * turnSpeed * Time.deltaTime * 30, 0);
            else
                possessTarg.GetComponent<PossessObj>().mainBod.GetComponent<Rigidbody>().transform.Rotate(0, mH * turnSpeed * Time.deltaTime * 15, 0);
        }
        else
        {
            if (mV != 0)
                transform.Rotate(0, mH * turnSpeed * Time.deltaTime * 30, 0);
            else
                transform.Rotate(0, mH * turnSpeed * Time.deltaTime * 15, 0);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isPossessing)
        {
            GetComponent<Rigidbody>().useGravity = true;
            if (!isGrounded)
            {
                airTimer += Time.deltaTime;
            }


            //transform.Translate(0, 0, mV * movSpeed * Time.deltaTime);
            //GetComponent<Rigidbody>().MovePosition(transform.position + (transform.forward * mV * movSpeed * Time.deltaTime));
            GetComponent<Rigidbody>().transform.position = (transform.position + (transform.forward * mV * movSpeed * Time.deltaTime));

            pumpkin.transform.Rotate(mV * turnSpeed * Time.deltaTime * 125, 0, 0);

            if (mV == 0)
            {
                pumpkin.transform.rotation = Quaternion.Lerp(pumpkin.transform.rotation, transform.rotation, 1);
            }
            if (isGrounded)
            {
                cam.transform.position = Vector3.Lerp(cam.transform.position, offSet.transform.position, .5f);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, offSet.transform.rotation, .5f);
            }
            else if (!isGrounded)
            {
                if (Vector3.Distance(cam.transform.position, offSet.transform.position) > 5)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, offSet.transform.position, .75f);
                    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, offSet.transform.rotation, .75f);
                }
                else if (Vector3.Distance(cam.transform.position, offSet.transform.position) < 5 && airTimer < 1.9f)
                {
                    cam.transform.position = Vector3.Lerp(cam.transform.position, offSet.transform.position, .01f);
                    cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, offSet.transform.rotation, .01f);
                }
                else if (airTimer >= 2)
                {
                    cam.transform.position = offSet.transform.position;
                }
            }
        }
        else if (isPossessing)
        {

            GetComponent<Rigidbody>().useGravity = false;

            if (mV != 0)
            {
                possessTarg.GetComponent<PossessObj>().anim.SetBool("Move", true);
            }
            else
            {
                possessTarg.GetComponent<PossessObj>().mainBod.GetComponent<Rigidbody>().velocity = Vector3.zero;
                possessTarg.GetComponent<PossessObj>().anim.SetBool("Move", false);
            }
            if (Input.GetMouseButtonDown(0))
            {
                possessTarg.GetComponent<PossessObj>().LeftClick();
            }
            if (Input.GetMouseButtonDown(1))
            {
                possessTarg.GetComponent<PossessObj>().RightClick();
            }

            possessTarg.GetComponent<PossessObj>().mainBod.GetComponent<Rigidbody>().transform.position = (possessTarg.GetComponent<PossessObj>().mainBod.transform.position + (possessTarg.GetComponent<PossessObj>().mainBod.transform.forward * mV * movSpeed * Time.deltaTime)); ;

            transform.rotation = possessTarg.GetComponent<PossessObj>().mainBod.transform.rotation;


            cam.transform.position = Vector3.Lerp(cam.transform.position, offSet.transform.position, .5f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, offSet.transform.rotation, .5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            isPossessing = true;
            possessTarg = other.gameObject;
            transform.SetParent(possessTarg.transform);
            transform.position = other.gameObject.transform.position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            bodyCollider.enabled = false;
        }
        isGrounded = true;
        transform.rotation = Quaternion.Euler(Quaternion.identity.x, transform.eulerAngles.y, Quaternion.identity.z);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            isPossessing = false;
            transform.SetParent(null);
            possessTarg.GetComponent<PossessObj>().anim.SetBool("Move", false);
            possessTarg.GetComponent<PossessObj>().anim.SetBool("Use", false);
            possessTarg.GetComponent<PossessObj>().anim.SetBool("Use2", false);
            bodyCollider.enabled = true;
        }
        isGrounded = false;
        airTimer = 0;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isGrounded)
        {
            isGrounded = true;
        }
    }
}