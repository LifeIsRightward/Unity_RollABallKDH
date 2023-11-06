using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    public Text countText;
    public Text CheckTime;
    public Text BestScore;
    public GameObject winTextObject;
    public FixedJoystick joystick;
    public AudioClip SoundTrack;

    Rigidbody rb;

    int count;
    float timecount = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        
        SetCountText();
        winTextObject.SetActive(false);
        //PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;
        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
        timecount += Time.deltaTime;
        CheckTime.text = "Time: " + ((int)timecount).ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            SetGetScoreAudio();
        }
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        Debug.Log(timecount);
        if(count >= 12)
        {
            if (!PlayerPrefs.HasKey("BestScore"))
            {
                PlayerPrefs.SetFloat("BestScore", timecount); // ���� �ȵǰ� ũ�� �ʱ�ȭ��Ű�� ���� �ʱⰪ�� �־���
            }
            float bestTime = PlayerPrefs.GetFloat("BestScore");
            Debug.Log(bestTime);

            if(timecount < bestTime)
            {
                bestTime = timecount;
                PlayerPrefs.SetFloat("BestScore", bestTime);
            }
            BestScore.text = "Best Time: " + (int)bestTime;
            Debug.Log("Branch4");
            winTextObject.SetActive(true);
        } 
    }

    void SetGetScoreAudio()
    {
        GetComponent<AudioSource>().PlayOneShot(SoundTrack);//�̷��� �ؾ��� ����� + ȿ������ ���ÿ� ��µǴµ� ��.
    }

    public void OnClick()
    {
        SceneManager.LoadScene("MiniGame");
    }
    public void OnApplicationQuit()
    {
       Application.Quit();
    }


}
