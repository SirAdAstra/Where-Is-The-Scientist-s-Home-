using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputController : MonoBehaviour
{
    public Image neutral, neg, pos, broken;
    // Level 1
    public GameObject note, card, testTube, screw, gear, inventory, door, computerDoor, wire, battery;
    public ItemData noteData, cardData, tubeData, screwData, gearData;

    // Level 2
    public GameObject crystal, shard, key, poster, crystalImg, shardImg, keyImg, liquidImg;
    public ItemData crystalData, shardData, keyData, posterData;

    // Level 3
    public GameObject crystalLvl3, wirecutter, posterLvl3, crystalLvl3Img, wireLvl3, switchLvl3, repairedImg;
    public ItemData crystalLvl3Data, wirecutterData, posterLvl3Data;

    public AudioClip noteAudio, cardAudio, tubeAudio, screwAudio, gearAudio, keyAudio, crystalAudio, openDoorAudio, switchAudio, wirecutAudio;
    private InventoryController inventoryController;
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;
    public bool powered, closed, repaired, openedDoor, openedCompDoor, gearRepaired, crystalInserted, shardInserted, keyInserted, liquidBool, wireBroken, crystalInsertedLvl3, repairedLvl3;
    public float close;
    
    // Start is called before the first frame update
    void Start()
    {
        inventoryController = inventory.GetComponent<InventoryController>();
        audioSource = GetComponent<AudioSource>();
        powered = false;
        closed = true;
        repaired = false;
        openedCompDoor = false;
        openedDoor = false;
        gearRepaired = false;
        crystalInserted = false;
        shardInserted = false;
        keyInserted = false;
        liquidBool= false;
        wireBroken = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero);
            if (rayHit.transform != null)
            {
                DisableClouds();
                switch(rayHit.transform.tag)
                {
           

                    case "Card":
                    {
                        if (powered && repaired && gearRepaired)
                        {
                            pos.enabled = true;
                            inventoryController.ItemRecieved(cardData);
                            Destroy(card);
                        }
                        else
                            broken.enabled = true;
                        close = Time.time + 3f;
                        break;
                    }

                    case "TestTube":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(tubeData);
                        Destroy(testTube);
                        close = Time.time + 3f;
                        break;
                    }

                    case "Screwdriver":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(screwData);
                        Destroy(screw);
                        close = Time.time + 3f;
                        break;
                    }

                    case "Gear":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(gearData);
                        Destroy(gear);
                        close = Time.time + 3f;
                        break;
                    }

                    case "ComputerDoor":
                    {
                        spriteRenderer = computerDoor.GetComponent<SpriteRenderer>();
                        spriteRenderer.enabled = !spriteRenderer.enabled;
                        openedCompDoor = !openedCompDoor;
                        break;
                    }

                    case "Door":
                    {
                        if(openedDoor)
                        {
                            Debug.Log("Finish!");
                            SceneManager.LoadScene(6);
                        }
                        else
                            Debug.Log("Open door fist");
                        break;
                    }

                    case "Crystal":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(crystalData);
                        Destroy(crystal);
                        close = Time.time + 3f;
                        break;
                    }

                    case "Shard":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(shardData);
                        Destroy(shard);
                        close = Time.time + 3f;
                        break;
                    }

                    case "Key":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(keyData);
                        Destroy(key);
                        close = Time.time + 3f;
                        break;
                    }

                   

                    case "InsertedKey":
                    {
                        if(crystalInserted && shardInserted && liquidBool)
                        {
                            audioSource.PlayOneShot(openDoorAudio);
                            SceneManager.LoadScene(7);
                            Debug.Log("Finish!");
                        }
                        else
                        {
                            broken.enabled = true;
                            close = Time.time + 3f;
                        }
                        break;
                    }

                   

                    case "WireCutter":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(wirecutterData);
                        Destroy(wirecutter);
                        close = Time.time + 3f;
                        break;
                    }

                    case "CrystalLvl3":
                    {
                        pos.enabled = true;
                        inventoryController.ItemRecieved(crystalLvl3Data);
                        Destroy(crystalLvl3);
                        close = Time.time + 3f;
                        break;
                    }

                    case "Switch":
                    {
                        spriteRenderer = switchLvl3.GetComponent<SpriteRenderer>();
                        spriteRenderer.enabled = true;
                        audioSource.PlayOneShot(switchAudio);
                        if (crystalInsertedLvl3 && wireBroken && repairedLvl3)
                        {
                            Debug.Log("Finish!");

                            SceneManager.LoadScene(8);
                        }
                        else
                        {
                            broken.enabled = true;
                            Invoke("DisableSwitch", 1f);
                            close = Time.time + 3f;
                        }
                        break;
                    }
                }
                closed = false;
            } 
            else
            Debug.Log("Nothing.");
        }
        if(!closed && Time.time > close)
            DisableClouds();
    }

    public void DisableClouds()
    {
        neutral.enabled = false;
        neg.enabled = false;
        pos.enabled = false;
        broken.enabled = false;
        closed = true;
    }

    private void DisableSwitch()
    {
        spriteRenderer = switchLvl3.GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
        audioSource.PlayOneShot(switchAudio);
    }
}
