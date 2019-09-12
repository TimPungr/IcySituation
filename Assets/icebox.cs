using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class icebox : MonoBehaviour
{

    private float TIME_TO_FREEZE = 3f;

    [SerializeField] private SpriteRenderer thisSprite;
    [SerializeField] private Rigidbody2D thisBody;
    [SerializeField] private float horizontalSpeed = 100f;

    protected float changePerSecond;
    protected GameManager manager;
    protected bool touching;
    private ContactPoint2D[] contacts;

    protected ContactPoint2D[] Contacts
    {
        get
        {
            return contacts;
        }

        set
        {
            contacts = value;
        }
    }

    private void Start()
    {
        changePerSecond = (80f / 255f) / TIME_TO_FREEZE;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void SetFreezeTime(float newTime)
    {
        TIME_TO_FREEZE = newTime;
        changePerSecond = (80f / 255f) / TIME_TO_FREEZE;
    }

    protected virtual void OnFreeze()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (thisSprite.color.g <= 70f / 255)
        {
            thisSprite.color = new Color(thisSprite.color.r, 70f / 255, thisSprite.color.b, thisSprite.color.a);           
            manager.IncreaseScore(transform.position.y);
            OnFreeze();
            Destroy(thisBody);
            Destroy(this);
        }

        if (!touching)
        {
            thisSprite.color = new Color(thisSprite.color.r, Mathf.Min(thisSprite.color.g + (changePerSecond * Time.deltaTime), 150f / 255), thisSprite.color.b, thisSprite.color.a);
        }

        float move = Input.acceleration.x * horizontalSpeed;
        move *= Time.deltaTime;
        transform.Translate(move, 0, 0, Space.World);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "KillTrigger")
        {
            manager.DecreaseLives();
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        touching = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        touching = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Contacts = new ContactPoint2D[collision.contactCount];
        if (transform.position.y > collision.transform.position.y + 1.7f)
        {
            collision.GetContacts(Contacts);
            float collLength = Vector2.Distance(Contacts[0].point, Contacts[collision.contactCount - 1].point);
            thisSprite.color = new Color(thisSprite.color.r, thisSprite.color.g - (changePerSecond * Time.deltaTime * Mathf.Max(1,collLength)), thisSprite.color.b, thisSprite.color.a);            
        }
    }
}
