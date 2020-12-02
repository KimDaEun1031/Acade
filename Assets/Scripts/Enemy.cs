using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public string enemyname;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDelay;
    public float curShotDelay;

    public GameObject EnemyBullettype1; //적 기본탄
    public GameObject player;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Fire(); // 탄 발사
        Reload();
    }


    void ReturnSprite()
    {
        spriteRenderer.sprite = sprites[0];
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BorderBullet")
            Destroy(gameObject);
        else if (collision.gameObject.tag == "PlayerBullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.dmg);

            Destroy(collision.gameObject);

        }

    }

    void Fire()
    {

        if (curShotDelay < maxShotDelay)
            return;

        if (enemyname == "Enemy")
        {
            GameObject bullet = Instantiate(EnemyBullettype1, transform.position + Vector3.right * 0.3f, transform.rotation);          
            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();       
            Vector3 dirVec = player.transform.position - transform.position + Vector3.right * 0.3f;           
            rigid.AddForce(dirVec * 10, ForceMode2D.Impulse);
  
        }

        curShotDelay = 0;
    }
    void Reload()
    {
        curShotDelay += Time.deltaTime;
    }
    void OnHit(int dmg)
    {
        health -= dmg;
        spriteRenderer.sprite = sprites[1];
        Invoke("ReturnSprite", 0.1f);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}



