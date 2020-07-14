using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Collider : MonoBehaviour
{
    public Sprite[] shapeSprites;
    public Sprite[] placeHolder;
    // private GameObject shield;
    private bool ifBonus;
    private bool ifFlash;
    private AudioSource[] impactAudios;
    // private Animator wing_left;
    // private Animator wing_right;
    private Animator cloudAnimator;
    private GameObject rainbow;
    private GameObject cloudCover;
    // Start is called before the first frame update
    void Start()
    {
        // shield = GameObject.Find("Shield");
        // shield.SetActive(false);
        ifBonus = false;
        ifFlash = false;
        impactAudios = GetComponents<AudioSource>();
        rainbow = GameObject.Find("Rainbow");
        rainbow.SetActive(false);
        cloudCover = GameObject.Find("Cloud Cover");
        cloudCover.SetActive(false);
        cloudAnimator = this.gameObject.transform.GetChild(4).GetComponent<Animator>();
        // wing_left = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        // wing_right = this.gameObject.transform.GetChild(2).GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D obstacle)
    {
        // Check which kind of collider is.
        if (obstacle.gameObject.tag == "ColorSwitcher")
        {
            CollideColorSw(obstacle);
        }
        else if (obstacle.gameObject.tag == "ShapeSwitcher")
        {
            CollideShapeSw(obstacle);
        }
        else if (obstacle.gameObject.tag == "Enemy")
        {
            CollideEnemy(obstacle);
        }
        else if (obstacle.gameObject.tag == "Stone")
        {
            CollideStone(obstacle);
        }
        else if (obstacle.gameObject.tag == "Cloud")
        {
            StartCoroutine(CollideCloud(obstacle));
        }
    }
    void CollideColorSw(Collider2D switcher)
    {
        SpriteRenderer player = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer colorSwitcher = switcher.GetComponent<SpriteRenderer>();
        impactAudios[3].Play();
        player.color = colorSwitcher.color;
        Destroy(switcher.gameObject);

    }
    void CollideShapeSw(Collider2D switcher)
    {
        SpriteRenderer player = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        GameObject shapeSwitcher = switcher.gameObject;
        SpriteRenderer shapeChild = shapeSwitcher.transform.GetChild(0).GetComponent<SpriteRenderer>();
        string shapeName = shapeChild.sprite.name;
        switch(shapeName)
        {
            case "sw_circle":
                player.sprite = shapeSprites[0];
                break;
            case "sw_triangle":
                player.sprite = shapeSprites[1];
                break;
            case "sw_square":
                player.sprite = shapeSprites[2];
                break;
            case "sw_hexagon":
                player.sprite = shapeSprites[3];
                break;
            case "sw_star":
                player.sprite = shapeSprites[4];
                break;
            default:
                Debug.Log("Can't find corresponding shape!");
                break;
        }
        impactAudios[3].Play();
        Destroy(switcher.gameObject);
    }
    void CollideEnemy(Collider2D enemyBlock)
    {
        if (ifBonus)
        {
            impactAudios[0].Play();
            GameManager.instance.AddScore();
            Destroy(enemyBlock.gameObject);
            return;
        }
        SpriteRenderer player = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        SpriteRenderer enemy = enemyBlock.transform.GetChild(0).GetComponent<SpriteRenderer>();

        if (player.color == enemy.color)
        {
            if (player.sprite == enemy.sprite) //Same color and same shape.
            {
                impactAudios[1].Play();
                GameManager.instance.AddScore();
                rainbow.SetActive(true);
                StartCoroutine(BonusTrigger());
            }
            else //Same color and different shape.
            {
                impactAudios[0].Play();
                GameManager.instance.AddScore();
            }   
        }
        else
        {
            if (player.sprite == enemy.sprite) //Different color and same shape.
            {
                impactAudios[0].Play();
                GameManager.instance.AddScore();
            }
            else //Different color and different shape.
            {
                impactAudios[2].Play();
                GameManager.instance.LoseHealth();
            }
        }
        player.color = enemy.color;
        player.sprite = enemy.sprite;
        Destroy(enemyBlock.gameObject);
    }
    void CollideStone(Collider2D obstacle)
    {
        impactAudios[4].Play();
        GameManager.instance.EndGame(); // Game Over!
    }
    IEnumerator CollideCloud(Collider2D obstacle)
    {
        impactAudios[3].Play();
        Destroy(obstacle.gameObject);
        cloudCover.SetActive(true);
        yield return new WaitForSeconds(8.5f);
        cloudAnimator.Play("cloud_gone");
        yield return new WaitForSeconds(1.1f);
        cloudCover.SetActive(false);
        GameManager.instance.hasCloudCover = false;
    }
    IEnumerator BonusTrigger()
    {
        ifBonus = true;
        ifFlash = true;
        SpriteRenderer playerHolder = GetComponent<SpriteRenderer>();
        playerHolder.sprite = placeHolder[1];
        StartCoroutine(BonusFlash());
        yield return new WaitForSeconds(5.0f);
        ifFlash = false;
        StartCoroutine(BonusFade());
        yield return new WaitForSeconds(1.0f);
        playerHolder.sprite = placeHolder[0];
        ifBonus = false;
    }
    IEnumerator BonusFlash()
    {
        int colorCount = 0;
        while (ifFlash)
        {
            Color nextColor = ColorSpawner.colors[colorCount];
            SpriteRenderer player = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
            player.color = nextColor;
            colorCount ++;
            if (colorCount == 5){
                colorCount = 0;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator BonusFade()
    {
        SpriteRenderer playerHolder = GetComponent<SpriteRenderer>();
        while (ifBonus)
        {
            playerHolder.enabled = false;
            yield return new WaitForSeconds(0.08f);
            playerHolder.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        rainbow.SetActive(false);
    }
}
