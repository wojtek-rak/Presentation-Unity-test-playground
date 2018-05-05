using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Shoot_arrow : MonoBehaviour
{
    public Rect button_jump;
    public Quaternion rotation = Quaternion.identity;
    public GameObject strzala;
    private Rigidbody2D arrow_rigidbody2D;
    public float force;
    Vector2 objPos;

    Touch touch;
    
    Rect buttonRect = new Rect(1, 1, Screen.width/2, Screen.height/5);
    /*
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Vector3 position = transform.position;// + new Vector3(2.0f, 0, 0);
            GameObject arrow = Instantiate(strzala, position, transform.rotation);
            //Instantiate (meat, position, Random.rotation);
            //Vector3 direction;
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            arrow.GetComponent<Rigidbody2D>().AddForce(ray.direction * force);
            Debug.Log(ray.direction);
        }
    }
    */

    void Update()
    {
        //Debug.Log("Screen Height : " + Screen.height);
        //Debug.Log("Screen Width : " + Screen.width);
        if (Input.touchCount > 0)
        touch = Input.GetTouch(0);
        if (buttonRect.Contains(touch.position)) return;



        //Debug.Log(!CrossPlatformInputManager.GetButtonDown("Jump"));
        if (Input.GetMouseButtonDown(0))
            {
            //Debug.Log("CREAT");
                Vector2 archerPos;
                Vector3 direct;
                Vector3 position = transform.position + new Vector3(0, -10f, 31f);
                GameObject arrow = Instantiate(strzala, position, transform.rotation);
                //Instantiate (meat, position, Random.rotation);
                //Vector3 direction;
                Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                //Debug.Log(ray);
                direct = ray.origin;
                direct.z = 0;
                archerPos = transform.position + new Vector3(0, -4.0f, 12.4f);
                direct.x -= archerPos.x;
                direct.y -= archerPos.y;
                if (direct.x < 0)
                {
                    arrow.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    arrow.transform.rotation = new Quaternion(arrow.transform.rotation.x, arrow.transform.rotation.y, arrow.transform.rotation.z, arrow.transform.rotation.w);

                    //Debug.Log(arrow.transform.rotation);
                    //Debug.Log("TRANFORMING");
                }
                arrow_rigidbody2D = arrow.GetComponent<Rigidbody2D>();
                arrow_rigidbody2D.velocity = direct * force;
                //arrow_rigidbody2D.velocity = Vector2.ClampMagnitude(arrow_rigidbody2D.velocity, 90f);
                //Debug.Log(ray.origin);
                //Debug.Log(direct);
            }
        

        /*
        Vector2 mouse = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Ray ray2;
        ray2 = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        Debug.Log("");
        if (Physics.Raycast(ray2, out hit, 10))
        {
            if (hit.point.x < transform.position.x)
                Debug.Log("Left");
            else
                Debug.Log("Right");
        }
        */

    }
}
