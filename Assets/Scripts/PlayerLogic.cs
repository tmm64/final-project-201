using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{   // Dash will be done by briefly turning on kinematics then turning it off again.
    // Jump will be a basic jump
    //Stomp is uhhhh basically the dash logic but downwards.
    private Rigidbody rb;
    public int jumpStocks;
    public int dashStocks;
    public int stompStocks;
    public int fragments;
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
        jumpStocks = 0; 
        dashStocks = 0;
        stompStocks = 0;
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

        if (other.CompareTag("Projectile"))
        {
            if (!invincible)
            {
                health--;
                setHealth();
            }
            

            Destroy(other.gameObject);
        }

        if (other.CompareTag("Enemy"))
        {
            if (invincible)
            {
                Destroy(other.gameObject);
            }
            
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
        invincible = true;

        float elapsed = 0f;
        while (elapsed < dashTime)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        rb.useGravity = true;
        invincible = false;
    }

    // Coroutine for stomp logic. Stops momentum, applies downward force, then turns gravity back on after a short time.
    void stomp(Rigidbody rb)
    {
        StartCoroutine(StompCoroutine(rb));
    }

    IEnumerator StompCoroutine(Rigidbody rb)
    {
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = false;
        invincible = true; 

        yield return new WaitForSeconds(0.15f);

        rb.useGravity = true;
        rb.AddForce(Vector3.down * stompForce, ForceMode.Impulse);

        
        yield return new WaitForSeconds(1f);
        invincible = false;
    }


    public void setHealth()
    {
        Health.text = "Health: " + health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void setFragments()
    {
        Fragments.text = "Fragments: " + fragments.ToString();
    }

    public void setStocks()
    {
            Stocks.text = "Jump Stocks: " + jumpStocks.ToString() + "\nDash Stocks: " + dashStocks.ToString() + "\nStomp Stocks: " + stompStocks.ToString();
    }
}
