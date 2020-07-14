using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] blocks;
    public Color[] colors;
    public GameObject stone;
    public GameObject cloud;
    // private Vector2 screenBounds;
    private float screenWidth;
    private float[] spawnPos;
    private int[] seqPool;
    private int level;
    public ColorSpawner colorSpawner;
    public ShapeSpawner shapeSpawner;

    // Start is called before the first frame updated
    void Start()
    {
        screenWidth = WindowManager.instance.GetWidth();
        // screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        float spawnPos1 = - 4 * screenWidth / 5;
        float spawnPos2 = - 2 * screenWidth / 5;
        float spawnPos3 = 0;
        float spawnPos4 = 2 * screenWidth / 5;
        float spawnPos5 = 4 * screenWidth / 5;
        spawnPos = new float[5] {spawnPos1, spawnPos2, spawnPos3, spawnPos4, spawnPos5};
        seqPool = new int[5] {0, 1, 2, 3, 4};
        StartCoroutine(LevelZero());
    }
    IEnumerator LevelZero() // Tutorial Level
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 0) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(4.5f);
                break;
            }
            float randomInterval = Random.Range(2.3f, 2.7f); // Interval of every rows of blocks.
            yield return new WaitForSeconds(randomInterval);
            Spawner();
        }
        StartCoroutine(LevelOne());
    }
    IEnumerator LevelOne() // Add meteorite and speed up
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 1) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(3.6f);
                break;
            }
            float randomInterval = Random.Range(2.0f, 2.4f); // Interval of every rows of blocks.
            yield return new WaitForSeconds(randomInterval);
            if (Random.value < 0.5f) // Decide if generate stone.
            {
                StoneGenerator();
            }
            Spawner();
        }
        StartCoroutine(LevelTwo());
    }
    IEnumerator LevelTwo() // Add swticher and speed up
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 2) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(3.5f);
                break;
            }
            bool ifSwitcher = Random.value < 0.5f; // Decide if generate switcher.
            float randomInterval = Random.Range(2.0f, 2.4f); // Interval of every rows of blocks.

            if (ifSwitcher) // Generate switcher.
            {
                randomInterval = Random.Range(2.5f, 2.8f); // If there is a switcher between two rows, widen the distance.
            }
            yield return new WaitForSeconds(randomInterval);
            if (Random.value < 0.4f) // Decide if generate stone.
            {
                StoneGenerator();
            }
            Spawner(ifSwitcher);
        }
        StartCoroutine(LevelThree());
    }
    IEnumerator LevelThree() // Add flashing blocks and moving blocks & speed up
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 3) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(3.4f);
                break;
            }
            bool ifSwitcher = Random.value < 0.5f; // Decide if generate switcher.
            float randomInterval = Random.Range(1.7f, 2.1f); // Interval of every rows of blocks.

            if (ifSwitcher) // Generate switcher.
            {
                randomInterval = Random.Range(2.2f, 2.5f); // If there is a switcher between two rows, widen the distance.
            }

            float moveOrFlash = Random.value;
            bool ifMoveX = moveOrFlash < 0.3f; // Decide if move along X axis.
            bool ifFlash = moveOrFlash > 0.6f; // Decide if flash.

            yield return new WaitForSeconds(randomInterval);

            if (Random.value < 0.3f) // Decide if generate stone.
            {
                StoneGenerator();
            }

            Spawner(ifSwitcher, ifFlash, ifMoveX);
        }
        StartCoroutine(LevelFour());
    }
    IEnumerator LevelFour() // Add cloud cover and speed up
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 4) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(3.2f);
                break;
            }
            bool ifSwitcher = Random.value < 0.5f; // Decide if generate switcher.
            float randomInterval = Random.Range(1.5f, 2.0f); // Interval of every rows of blocks.

            if (ifSwitcher) // Generate switcher.
            {
                randomInterval = Random.Range(2.0f, 2.4f); // If there is a switcher between two rows, widen the distance.
            }

            float moveOrFlash = Random.value;
            bool ifMoveX = moveOrFlash < 0.3f; // Decide if move along X axis.
            bool ifFlash = moveOrFlash > 0.6f; // Decide if flash.

            float stoneOrCloud = Random.value;
            bool ifStone = stoneOrCloud < 0.3f; // Decide if generate stone.
            bool ifCloud = stoneOrCloud > 0.5f; // Decide if generate cloud.

            yield return new WaitForSeconds(randomInterval);

            if (ifStone) // Decide if generate stone.
            {
                StoneGenerator();
            }
            if (ifCloud && GameManager.instance.hasCloudCover == false) // Decide if generate cloud.
            {
                GameManager.instance.hasCloudCover = true;
                CloudGenerator();
            }

            Spawner(ifSwitcher, ifFlash, ifMoveX);
        }
        StartCoroutine(LevelFive());
    }
    IEnumerator LevelFive() // Speed up
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            level = GameManager.instance.GetLevel();
            if (level != 5) // If level up, then wait some sec and call next level's function.
            {
                yield return new WaitForSeconds(3.0f);
                break;
            }
            bool ifSwitcher = Random.value < 0.5f; // Decide if generate switcher.
            float randomInterval = Random.Range(1.4f, 1.9f); // Interval of every rows of blocks.

            if (ifSwitcher) // Generate switcher.
            {
                randomInterval = Random.Range(1.9f, 2.3f); // If there is a switcher between two rows, widen the distance.
            }

            float moveOrFlash = Random.value;
            bool ifMoveX = moveOrFlash < 0.3f; // Decide if move along X axis.
            bool ifFlash = moveOrFlash > 0.6f; // Decide if flash.

            float stoneOrCloud = Random.value;
            bool ifStone = stoneOrCloud < 0.3f; // Decide if generate stone.
            bool ifCloud = stoneOrCloud > 0.8f; // Decide if generate cloud.

            yield return new WaitForSeconds(randomInterval);

            if (ifStone) // Decide if generate stone.
            {
                StoneGenerator();
            }
            if (ifCloud && GameManager.instance.hasCloudCover == false) // Decide if generate cloud.
            {
                GameManager.instance.hasCloudCover = true;
                CloudGenerator();
            }
            Spawner(ifSwitcher, ifFlash, ifMoveX);
        }
        StartCoroutine(LevelSix());
    }
    IEnumerator LevelSix()
    {
        GameManager.instance.ChangeSpeed(); // Change speed before every level.
        GameManager.instance.changeBG();
        while(true)
        {
            // Final Level
            bool ifSwitcher = Random.value < 0.5f; // Decide if generate switcher.
            float randomInterval = Random.Range(1.3f, 1.8f); // Interval of every rows of blocks.

            if (ifSwitcher) // Generate switcher.
            {
                randomInterval = Random.Range(1.8f, 2.2f); // If there is a switcher between two rows, widen the distance.
            }

            float moveOrFlash = Random.value;
            bool ifMoveX = moveOrFlash < 0.3f; // Decide if move along X axis.
            bool ifFlash = moveOrFlash > 0.6f; // Decide if flash.

            float stoneOrCloud = Random.value;
            bool ifStone = stoneOrCloud < 0.3f; // Decide if generate stone.
            bool ifCloud = stoneOrCloud > 0.8f; // Decide if generate cloud.

            yield return new WaitForSeconds(randomInterval);

            if (ifStone) // Decide if generate stone.
            {
                StoneGenerator();
            }
            if (ifCloud && GameManager.instance.hasCloudCover == false) // Decide if generate cloud.
            {
                GameManager.instance.hasCloudCover = true;
                CloudGenerator();
            }
            Spawner(ifSwitcher, ifFlash, ifMoveX);
        }
    }
    void Spawner(bool ifSwitcher=false, bool ifFlash=false, bool ifMoveX=false)
    {
        bool colorPrior = Random.value < 0.5f; //Decide if five colors or five shapes.
        if (colorPrior){
            ColorPrior(ifFlash, ifMoveX);
            if (ifSwitcher) //Generate switcher.
            {
                colorSpawner.SpawnColorSwitcher();
            } 
        }
        else{
            ShapePrior(ifFlash, ifMoveX);
            if (ifSwitcher) //Generate switcher.
            {
                shapeSpawner.SpawnShapeSwitcher();
            }
        }
        ShufflePool();
    }
    void ColorPrior(bool ifFlash=false, bool ifMoveX=false)
    {        
        // This function ensures that five generated blocks has five different colors.
        if (ifMoveX)
        {
            for (int i = 0; i < 3; i++)
            {
                int shapeIndex = Random.Range(0, blocks.Length); //Shape generate randomly.
                GameObject nextBlock = (GameObject)Instantiate(blocks[shapeIndex], new Vector2(spawnPos[i], transform.position.y), Quaternion.identity);
                nextBlock.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[seqPool[i]];
                nextBlock.AddComponent<BlockMove>();
                nextBlock.AddComponent<BlockMoveX>();      
            }
        }
        else
        {
            for (int i = 0; i < spawnPos.Length; i++)
            {
                int shapeIndex = Random.Range(0, blocks.Length); //Shape generate randomly.
                GameObject nextBlock = (GameObject)Instantiate(blocks[shapeIndex], new Vector2(spawnPos[i], transform.position.y), Quaternion.identity);
                nextBlock.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[seqPool[i]];
                nextBlock.AddComponent<BlockMove>();
                if (ifFlash)
                {
                    nextBlock.AddComponent<BlockFlash>();
                }     
            }
        } 
    }
    void ShapePrior(bool ifFlash=false, bool ifMoveX=false)
    {
        // This function ensures that five generated blocks has five different shapes.
        if (ifMoveX)
        {
            for (int i = 0; i < 3; i++)
            {
                int colorIndex = Random.Range(0, colors.Length); //Color generate randomly.
                GameObject nextBlock = (GameObject)Instantiate(blocks[seqPool[i]], new Vector2(spawnPos[i], transform.position.y), Quaternion.identity);
                nextBlock.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[colorIndex];
                nextBlock.AddComponent<BlockMove>();   
                nextBlock.AddComponent<BlockMoveX>();      
            }
        }
        else
        {
            for (int i = 0; i < spawnPos.Length; i++)
            {
                int colorIndex = Random.Range(0, colors.Length); //Color generate randomly.
                GameObject nextBlock = (GameObject)Instantiate(blocks[seqPool[i]], new Vector2(spawnPos[i], transform.position.y), Quaternion.identity);
                nextBlock.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colors[colorIndex];
                nextBlock.AddComponent<BlockMove>();
                if (ifFlash)
                {
                    nextBlock.AddComponent<BlockFlash>();
                }        
            }
        }

    }
    void StoneGenerator()
    {
        int index = Random.Range(0, spawnPos.Length); //Choose a random position.
        GameObject nextStone = (GameObject)Instantiate(stone, new Vector2(spawnPos[index], transform.position.y), Quaternion.identity);
        nextStone.GetComponent<SpriteRenderer>().sortingLayerName = "Bomb";
        nextStone.AddComponent<StoneMove>();        
    }
    void CloudGenerator()
    {
        GameObject nextCloud = (GameObject)Instantiate(cloud, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        nextCloud.AddComponent<CloudMove>();        
    }
    void ShufflePool()
    {
        for (int i = 0; i < seqPool.Length; i++)
        {
            int temp = seqPool[i];
            int rand = Random.Range(0, seqPool.Length);
            seqPool[i] = seqPool[rand];
            seqPool[rand] = temp;
        }
    }
}
