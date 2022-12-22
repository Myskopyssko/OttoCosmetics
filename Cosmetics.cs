using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Cosmetics : MonoBehaviourPun
{
    [Header("Cosmetics")]
    public GameObject[] hats;
    [Space(5)]
    public GameObject[] shirts;
    [Space(5)]
    public GameObject[] shoes;

    [Space(15)]
    public int selectedHat;
    public int selectedShirt;
    public int selectedShoe;

    [Header("Cosmetics UI")]
    [Space(20)]
    public GameObject cosmeticsUI;
    public GameObject cosmeticsModel;
    public bool canActivate = false;
    private GameObject selectedHatObject;
    private GameObject selectedShirtObject;
    private GameObject selectedShoeObject;

    public PhotonView view;
    public bool isModel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(!isModel) {
            if(!view.IsMine)
                return;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            if(!isModel){
            if(canActivate == true)
            {
                cosmeticsUI.SetActive(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                cosmeticsModel.SetActive(true);
            }
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            cosmeticsUI.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cosmeticsModel.SetActive(false);
        }

        foreach(GameObject sirt in shirts)
        {
            if(selectedShirt == 0)
            {
                sirt.SetActive(false);
            }

            if (sirt != shirts[selectedShirt])
            {
                sirt.SetActive(false);
            }
            else if(sirt == shirts[selectedShirt])
            {
                if(isModel == true)
                {
                    sirt.SetActive(true);
                }

                sirt.SetActive(true);
                this.photonView.RPC("SetShirtActive_RPC", RpcTarget.AllBuffered);
            }
        }

        foreach (GameObject hat in hats)
        {
            if(selectedHat == 0)
            {
                hat.SetActive(false);
            }
            
            if (hat != hats[selectedHat])
            {
                hat.SetActive(false);
            }
            else if(hat == hats[selectedHat])
            {
                if(isModel == true)
                {
                    hat.SetActive(true);
                }

                hat.SetActive(true);
                this.photonView.RPC("SetHatActive_RPC", RpcTarget.AllBuffered);
            }
        }

        foreach(GameObject shos in shoes)
        {
            if(selectedShoe == 0)
            {
                shos.SetActive(false);
            }
            
            if (shos != shoes[selectedShoe])
            {
                shos.SetActive(false);
            }
            else if(shos == shoes[selectedShoe])
            {
                if(isModel == true)
                {
                    shos.SetActive(true);
                }
                
                shos.SetActive(true);
                this.photonView.RPC("SetShoeActive_RPC", RpcTarget.AllBuffered);
            }
        }
    }

    public void AddSelectedHat()
    {
        this.photonView.RPC("AddSelectedHat_RPC", RpcTarget.AllBuffered);
    }

    public void AddSelectedShoe()
    {
        this.photonView.RPC("AddSelectedShoe_RPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AddSelectedShoe_RPC()
    {
        if(selectedShoe != shoes.Length)
        {
            selectedShoe += 1;
        }
        else if(selectedShoe == shoes.Length)
        {
            selectedShoe = 0;
        }
    }

    public void AddSelectedHatModel()
    {
        if(selectedHat != hats.Length)
        {
            selectedHat += 1;
        }
        else if(selectedHat == hats.Length)
        {
            selectedHat = -1;
        }
    }

    public void AddSelectedShoeModel()
    {
        if(selectedShoe != shoes.Length)
        {
            selectedShoe += 1;
        }
        else if(selectedShoe == shoes.Length)
        {
            selectedShoe = -1;
        }
    }

    public void AddSelecteShirtModel()
    {
        if(selectedShirt != shirts.Length)
        {
            selectedShirt += 1;
        }
        else if(selectedShirt == shirts.Length)
        {
            selectedShirt = -1;
        }
    }

    public void RemoveSelecteShirtModel()
    {
        if(selectedShirt == -1)
            return;

        if(selectedShirt != -1)
        {
            selectedShirt -= 1;
        }
    }

    public void RemoveSelectedHatModel()
    {
        if(selectedHat == -1)
            return;

        if(selectedHat != -1)
        {
            selectedHat -= 1;
        }
    }

    [PunRPC]
    private void SetHatActive_RPC()
    {
        if(isModel)
            return;

        if(selectedHat != 0)
            selectedHatObject = hats[selectedHat];

        foreach(GameObject hatObj in hats)
        {
            if(hatObj != selectedHatObject)
            {
                hatObj.SetActive(false);
            }
        }

        if(selectedHat != 0)
            selectedHatObject.SetActive(true);
    }

    [PunRPC]
    private void SetShirtActive_RPC()
    {
        if(isModel)
            return;

        selectedShirtObject = shirts[selectedShirt];

        foreach(GameObject shirtObj in shirts)
        {
            if(shirtObj != selectedShirtObject)
            {
                shirtObj.SetActive(false);
            }
        }
        selectedShirtObject.SetActive(true);
    }

    [PunRPC]
    private void SetShoeActive_RPC()
    {
        if(isModel)
            return;

        selectedShoeObject = shoes[selectedShoe];

        foreach(GameObject shoeObj in shoes)
        {
            if(shoeObj != selectedShoeObject)
            {
                shoeObj.SetActive(false);
            }
        }
        selectedShoeObject.SetActive(true);
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("dor"))
        {
            canActivate = true;
        }
        else
        {
            
        }
    }

    [PunRPC]
    public void AddSelectedHat_RPC()
    {
        if(selectedHat != hats.Length)
        {
            selectedHat++;
        }
        else if (selectedHat == hats.Length)
        {
            selectedHat = 0;
        }
    }

    public void RemoveSelectedHat()
    {
        if(isModel){
            selectedHat -= 1;
            return;
    }
        this.photonView.RPC("RemoveSelectedHat_RPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void RemoveSelectedHat_RPC()
    {
        if(isModel)
            return;

        if(selectedHat != 0)
        {
            selectedHat -= 1;
        }
    }

    [PunRPC]
    public void RemoveSelectedShirt_RPC()
    {
        if(isModel)
            return;
        
        if(selectedShirt != 0)
        {
            selectedShirt -= 1;
        }
    }

    public void AddSelectedShirt()
    {
        this.photonView.RPC("AddSelectedShirt_RPC", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void AddSelectedShirt_RPC()
    {
        if(selectedShirt != shirts.Length)
        {
            selectedShirt++;
        }
        else if(selectedShirt == hats.Length)
        {
            selectedShirt = 0;
        }
    }

    public void RemoveSelectedShirt()
    {
        if(isModel)
            selectedHat -= 0;
            return;
        
        this.photonView.RPC("RemoveSelectedShirt_RPC", RpcTarget.AllBuffered);
    }

    public void RemoveSelectedShoe()
    {
        if(selectedShoe != -1)
        {
            selectedShoe -= 1;
        }
    }
}