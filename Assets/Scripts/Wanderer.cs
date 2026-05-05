using UnityEngine;

public class Wanderer : MonoBehaviour
{   PlayerLogic playerLogic;
    public GameObject shopCanvas;
    public GameObject storePanel;
    public GameObject dialoguePanel;
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    public float followSpeed = 3f;
    public float stopDist = 4f;
    public float resumeDist = 6f;
    private bool isFollowing = true;
    void Start()
    {
        playerLogic = GameObject.FindWithTag("Player").GetComponent<PlayerLogic>();
        shopCanvas.SetActive(false);
        storePanel.SetActive(false);
        dialoguePanel.SetActive(false);
        answer1.SetActive(false);
        answer2.SetActive(false);
        answer3.SetActive(false);
    }
    void Update()
    {
        if (!shopCanvas.activeSelf)
        {

            float distanceToPlayer = Vector3.Distance(transform.position,
                                  playerLogic.transform.position);

            if (distanceToPlayer > resumeDist)
                isFollowing = true;

            
            if (distanceToPlayer < stopDist)
                isFollowing = false;

            if (isFollowing)
            {
                Vector3 direction = (playerLogic.transform.position
                                    - transform.position).normalized;
                transform.position += direction * followSpeed * Time.deltaTime;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OpenMenu();
    }
    void OpenMenu()
    {
        shopCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseMenu()
    {
        Debug.Log("CloseMenu called");
        shopCanvas.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenShop()
    {
        shopCanvas.SetActive(false);
        storePanel.SetActive(true);
    }
    public void Closeshop()
    {
        storePanel.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void BuyJump()
    {
        if (playerLogic.fragments >= 1)
        {
            playerLogic.fragments--;
            playerLogic.jumpStocks++;
            playerLogic.setFragments();
            playerLogic.setStocks();
            
        }
    }

    public void BuyDash()
    {
        if (playerLogic.fragments >= 1)
        {
            playerLogic.fragments--;
            playerLogic.dashStocks++;
            playerLogic.setFragments();
            playerLogic.setStocks();

        }
    }

    public void BuyStomp()
    {
        if (playerLogic.fragments >= 1)
        {
            playerLogic.fragments--;
            playerLogic.stompStocks++;
            playerLogic.setFragments();
            playerLogic.setStocks();

        }
    }
    public void OpenDialogue()
    {
        shopCanvas.SetActive(false);
        dialoguePanel.SetActive(true);
    }
     public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        shopCanvas.SetActive(true);
    }

    public void Answer1()
    {
        dialoguePanel.SetActive(false);
        answer1.SetActive(true);
    }

    public void Answer2()
    {
        dialoguePanel.SetActive(false);
        answer2.SetActive(true);
    }
    
    public void Answer3()
    {
        dialoguePanel.SetActive(false);
        answer3.SetActive(true);
    }
    public void GoBack()
    {
        if (answer1.activeSelf)
        {
            answer1.SetActive(false);
            dialoguePanel.SetActive(true);
        }
        else if (answer2.activeSelf)
        {
            answer2.SetActive(false);
            dialoguePanel.SetActive(true);
        }
        else if (answer3.activeSelf)
        {
            answer3.SetActive(false);
            dialoguePanel.SetActive(true);
        }
    }
}
