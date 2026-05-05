using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{   // Dash will be done by briefly turning on kinematics then turning it off again.
    // Jump will be a basic jump
    //Stomp is uhhhh basically the dash logic but downwards.
    private Rigidbody rb;
    int jumpStocks = 300;
    int dashStocks = 500;
    int stompStocks = 300;
    int fragments;
    int health;
    bool invincible = false;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Fragments;
    public TextMeshProUGUI Stocks;
    public float dashForce = 50f;
    public float dashTime = .15f;
    public float stompForce = 20f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        fragments = 0;
        health = 3;
        setHealth();
        setFragments();
        setStocks();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (jumpStocks <= 0)
            {
                return;
            }
            else
            {
                extraJump(rb);
                jumpStocks--;
                setStocks();
            }  
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (dashStocks <= 0)
            {
                return;
            }
            else
            {
                dash(rb);
                dashStocks--;
                setStocks();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (stompStocks <= 0)
            {
                return;
            }
            else
            {
                stomp(rb);
                stompStocks--;
                setStocks();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            other.gameObject.SetActive(false);
            fragments += 1;
            setFragments();
        }
    }
    // Extra jump logic
    void extraJump(Rigidbody rb)
   {
        
        rb.AddForce(Vector3.up * 100 * 4);
    }
   // Dash logic
   void dash(Rigidbody rb)
   {
        StartCoroutine(DashCoroutine(rb));
    }
    // Coroutine for dash logic. Stops momentum, applies dash force, then turns gravity back on after a short time.
    IEnumerator DashCoroutine(Rigidbody rb)
    {
        rb.linearVelocity = Vector3.zero;

        Vector3 dashDirection = Camera.main.transform.forward;
        dashDirection.y = 0f;
        dashDirection.Normalize();

        rb.AddForce(dashDirection * dashForce, ForceMode.Impulse);

        rb.useGravity = false;

        float elapsed = 0f;
        while (elapsed < dashTime)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.useGravity = true;
    }

    // Stomp logic. Stops momentum, applies downward force.
    void stomp(Rigidbody rb)
   {
  
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(Vector3.down * stompForce, ForceMode.Impulse);
    }


    void setHealth()
    {
        Health.text = "Health: " + health.ToString();
    }

    void setFragments()
    {
        Fragments.text = "Fragments: " + fragments.ToString();
    }

    void setStocks()
    {
            Stocks.text = "Jump Stocks: " + jumpStocks.ToString() + "\nDash Stocks: " + dashStocks.ToString() + "\nStomp Stocks: " + stompStocks.ToString();
    }
}
