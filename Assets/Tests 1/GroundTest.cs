using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class GroundTest
{

    private GameObject archer;
    private CharacterController testController;

    public void Default_start()
    {
        archer = GameObject.FindGameObjectWithTag("Player");
        testController = archer.GetComponent<CharacterController>();
        return;
    }
    private bool Ground_check()
    {
        return testController.isGrounded;
    }
    //[Test]
    //public void NewTestScriptSimplePasses() {
    //    // Use the Assert class to test conditions.
    //}

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode

    [UnityTest]
    [Timeout(100000000)]
    public IEnumerator GroundMoveTest()
    {
        Time.timeScale = 10f;
        bool check_false = true;
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(0.3f);
        Default_start();
        yield return new WaitUntil(Ground_check);

        float start = Time.time;

        while (Time.time - start < 100)
        {
            yield return null;
            yield return new WaitForFixedUpdate();

            Vector3 bottom = testController.transform.position - new Vector3(0, testController.height, 0);
            RaycastHit hit;

            if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit, 5f))
            {
                if(hit.distance > 5f)
                {
                    Debug.Log(hit.distance);
                    check_false = false;
                }
                
            }

        }
        Assert.IsTrue(check_false, "Move doesn't work poperly ");
    }


    [UnityTest]
    public IEnumerator GroundJumpTest()
    {
        bool check_false = true;
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(0.3f);
        Default_start();
        yield return new WaitUntil(Ground_check);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForFixedUpdate();
        archer.GetComponent<PlayerMovement>().jump_test = true;
        yield return new WaitForSeconds(1f);
        Vector3 bottom = testController.transform.position - new Vector3(0, testController.height, 0);
        RaycastHit hit;
        if (Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit, 5f))
            {
                if (hit.distance < 2f)
                {
                    check_false = false;
                    Debug.Log("błąd");
                }

            }
        Debug.Log(hit.distance);
        Assert.IsTrue(check_false, "Jump doesn't work properly");
    }

    [UnityTest]
    public IEnumerator SpawnGroundTest()
    {
        //bool check_false = true;
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(0.3f);
        Default_start();
        Vector3 bottom = testController.transform.position - new Vector3(0, testController.height, 0);
        RaycastHit hit;
        Assert.IsTrue(Physics.Raycast(bottom, new Vector3(0, -1, 0), out hit), "spawn in wrong place");
    }
}