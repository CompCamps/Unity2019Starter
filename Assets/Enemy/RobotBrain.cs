using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotBrain : MonoBehaviour
{

    Animator anim;

    public float health = 10;
    public float maxHealth = 10;

    [Header("Sounds")]
    public AudioClip blip;

    [Header("UI")]
    public Image healthBar;

    float last_seen = 0;

    [HideInInspector]
    public Vector3 last_known = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("since_seen", 15f + Time.time - last_seen);
        healthBar.fillAmount = health / maxHealth;
        transform.Find("scanner").GetComponent<Light>().intensity = (health / maxHealth) * 20;

        if (health <= 0)
        {
            anim.SetTrigger("die");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Check(other);
    }

    void OnTriggerStay(Collider other)
    {
        Check(other);
    }

    void Check(Collider other)
    {
        if (other.tag == "Player")
        {
            RaycastHit hit;
            LayerMask mask = LayerMask.NameToLayer("Enemy");
            Vector3 target = other.transform.position;
            target.y += 1;
            if (Physics.Linecast(transform.position, target, out hit, mask.value))
            {
                if (hit.transform.tag == "Player")
                {
                    if (anim.GetFloat("since_seen") > 1f)
                    {
                        transform.GetComponent<AudioSource>().PlayOneShot(blip);
                    }
                    last_seen = Time.time + 15f;
                    last_known = other.transform.position;
                }
            }
        }
    }
}
