using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryController : MonoBehaviour
{
    public GameObject canvas, mainCamera;
    GraphicRaycaster m_Raycaster;
    public ItemData[] StartItems;
    public Transform slots;
    public Image ItemImg,ItemNameBack,ItemDescriptionBack;
    public GameObject ItemVisual;
    public Text ItemName;
    public Text ItemDescription;
    public Image ItemDrag;
    private InputController inputController;

    void Start()
    {
        foreach (ItemData item in StartItems)
        {
            ItemRecieved(item);
        }
        m_Raycaster = canvas.GetComponent<GraphicRaycaster>();
        inputController = mainCamera.GetComponent<InputController>();
    }

    public void ShowItemInfo(ItemData item)
    {
        ItemName.enabled = item != null;
        ItemDescription.enabled = item != null;
        ItemImg.enabled = item != null;
        if (item)
        {
            ItemName.text = item.Title;
            ItemDescription.text = item.Description;
            ItemImg.sprite = item.Icon;
            if (ItemNameBack != null && ItemDescriptionBack != null)
            {
                ItemNameBack.enabled = true;
                ItemDescriptionBack.enabled = true;
            }
        }
        else
            if (ItemNameBack != null && ItemDescriptionBack != null)
        {
            ItemNameBack.enabled = false;
            ItemDescriptionBack.enabled = false;
        }
    }

    public void DevClick(ItemData item)
    {
        Debug.Log(item.Title);
        
    }

    public void DragStart(ItemData item)
    {
        ItemDrag.enabled = item != null;
        if (item)
        {
            ItemDrag.sprite = item.Icon;
            ItemDrag.transform.position = Input.mousePosition;
        }
    }

    public void DragStop(ItemData item, PointerEventData eventData, Transform startSlot)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(eventData, results);
        foreach (RaycastResult result in results)
        {
            switch(result.gameObject.tag)
            {
                case "InventorySlot":
                {
                    Debug.Log("Slot");
                    Transform t = slots.Find(result.gameObject.name);
                    if (t.childCount == 0)
                    {
                        GameObject itemVisual = Instantiate(ItemVisual, t.transform.position, Quaternion.identity, t);
                        itemVisual.GetComponent<InventoryItem>().Init(item);
                        Destroy(startSlot.gameObject);
                    }
                    else
                    {
                        Debug.Log("Slot is full!");
                    }
                    break;
                }

                case "DropScrew":
                {
                    inputController.DisableClouds();
                    if (item.ID == 1)
                    {
                        inputController.spriteRenderer = inputController.wire.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.screwAudio);
                        inputController.pos.enabled = true;
                        inputController.repaired = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Wire is reaired");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropTube":
                {
                    inputController.DisableClouds();
                    if (item.ID == 4)
                    {
                        inputController.spriteRenderer = inputController.battery.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.tubeAudio);
                        inputController.pos.enabled = true;
                        inputController.powered = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Powered!");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropGear":
                {
                    inputController.DisableClouds();
                    if(inputController.openedCompDoor)
                    {
                        if (item.ID == 0)
                        {
                            inputController.audioSource.PlayOneShot(inputController.gearAudio);
                            inputController.pos.enabled = true;
                            inputController.gearRepaired = true;
                            Destroy(startSlot.gameObject);
                            Debug.Log("Gear is in position!");
                        }
                        else
                        {
                            inputController.neg.enabled = true;
                            Debug.Log("Wrong");
                        }
                    }
                    else
                    {
                        inputController.neutral.enabled = true;
                        Debug.Log("Open Door!");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropCard":
                {
                    inputController.DisableClouds();
                    if (item.ID == 3)
                    {
                        inputController.spriteRenderer = inputController.door.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.openedDoor = true;
                        inputController.audioSource.PlayOneShot(inputController.cardAudio);
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Door Opened");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropCrystal":
                {
                    inputController.DisableClouds();
                    if (item.ID == 6)
                    {
                        inputController.spriteRenderer = inputController.crystalImg.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.crystalAudio);
                        inputController.crystalInserted = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Crystal Inserted");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropShard":
                {
                    inputController.DisableClouds();
                    if (item.ID == 7)
                    {
                        inputController.spriteRenderer = inputController.shardImg.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.crystalAudio);
                        inputController.shardInserted = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Shard Inserted");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropKey":
                {
                    inputController.DisableClouds();
                    if (item.ID == 5)
                    {
                        inputController.spriteRenderer = inputController.keyImg.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        BoxCollider2D box = inputController.keyImg.GetComponent<BoxCollider2D>();
                        box.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.keyAudio);
                        inputController.keyInserted = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Key Inserted");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropTubeLvL2":
                {
                    inputController.DisableClouds();
                    if (item.ID == 4)
                    {
                        inputController.spriteRenderer = inputController.liquidImg.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.tubeAudio);
                        inputController.liquidBool = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Liquid in Position, sir!");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropWirecutter":
                {
                    inputController.DisableClouds();
                    if (item.ID == 10)
                    {
                        inputController.spriteRenderer = inputController.wireLvl3.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.wirecutAudio);
                        inputController.wireBroken = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Wire cuted");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropCrystalLvl3":
                {
                    inputController.DisableClouds();
                    if (item.ID == 9)
                    {
                        inputController.spriteRenderer = inputController.crystalLvl3Img.GetComponent<SpriteRenderer>();
                        inputController.spriteRenderer.enabled = true;
                        inputController.audioSource.PlayOneShot(inputController.crystalAudio);
                        inputController.crystalInsertedLvl3 = true;
                        inputController.pos.enabled = true;
                        Destroy(startSlot.gameObject);
                        Debug.Log("Crystal inserted");
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }

                case "DropScrewLvl3":
                {
                    inputController.DisableClouds();
                    if (item.ID == 1)
                    {
                        if (inputController.wireBroken)
                        {
                            inputController.spriteRenderer = inputController.repairedImg.GetComponent<SpriteRenderer>();
                            inputController.spriteRenderer.enabled = true;
                            inputController.audioSource.PlayOneShot(inputController.screwAudio);
                            inputController.repairedLvl3 = true;
                            inputController.pos.enabled = true;
                            Destroy(startSlot.gameObject);
                            Debug.Log("Wires repaired");
                        }
                        else
                        {
                            inputController.neutral.enabled = true;
                            Debug.Log("Wrong");
                        }
                    }
                    else
                    {
                        inputController.neg.enabled = true;
                        Debug.Log("Wrong");
                    }
                    inputController.closed = false;
                    inputController.close = Time.time + 3f;
                    break;
                }
            }
        }
        ItemDrag.enabled = false;
    }

    public void ItemRecieved(ItemData item)
    {
        Transform freeslot = GetFreeSlot();
        GameObject itemVisual = Instantiate(ItemVisual, freeslot.transform.position, Quaternion.identity, freeslot);
        itemVisual.GetComponent<InventoryItem>().Init(item);
    }

    private Transform GetFreeSlot()
    {
        foreach (Transform t in slots)
        {
            if (t.childCount == 0) { return t; }
        }

        return null;
    }
}
