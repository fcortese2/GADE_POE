using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControlledSpawning : MonoBehaviour
{
    public GameObject[] arrSpawnableObjects;
    private int selectionIndex = 0;

    public Text display;

    public Image panel;
    public Text panelText;

    void Start()
    {
        panel.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            {
                if (hit.transform.tag == "Ground1")
                {
                    if (CanSpawn())
                    {
                        GameObject.Find("GM").GetComponent<ResourceStats>().Team1Steel -= 84;
                        GameObject.Find("GM").GetComponent<ResourceStats>().Team1Wood -= 84;
                        Object.Instantiate(arrSpawnableObjects[selectionIndex], hit.point, Quaternion.identity);
                    }
                }

                if (hit.transform.tag == "Team1" || hit.transform.tag == "Team2")
                {
                    GameObject _hit = GameObject.Find(hit.transform.name);
                    if (_hit.GetComponent<RangedUnit>())
                    {
                        panel.gameObject.SetActive(true);
                        panelText.text = "Health: " + _hit.GetComponent<RangedUnit>().CurrentHealth + "\n" +
                                         "Name: " + _hit.GetComponent<RangedUnit>().UNIT.name + "\n" +
                                         "Range: " + _hit.GetComponent<RangedUnit>().UNIT.range + "\n" +
                                         "Speed: " + _hit.GetComponent<RangedUnit>().UNIT.speed;
                    }
                }

            }

        }

        
    }

    bool CanSpawn()
    {
        if (GameObject.Find("GM").GetComponent<ResourceStats>().Team1Wood - 84 < 0 || GameObject.Find("GM").GetComponent<ResourceStats>().Team1Steel - 84 < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Select0()
    {
        selectionIndex = 0;
        DisplayUpdate("Archer");
    }

    public void Select1()
    {
        selectionIndex = 1;

        DisplayUpdate("Sniper Archer");
    }

    public void Select2()
    {
        selectionIndex = 2;

        DisplayUpdate("Knight");
    }

    void DisplayUpdate(string Name)
    {
        display.text = "Left click to place: " + Name;
    }
}
