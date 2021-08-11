using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class carPartSelector : MonoBehaviour
{

    public GameObject customiserMain;
    public GameObject paintMenu;
    public GameObject paintMenuButton;
    public GameObject backTolevelMenuButton;
    public GameObject backButton;
    public GameObject viewCarButton;
    //public GameObject levelSelect;

    public Text rearText;
    public Text sedanText;


    //car Section Arrays
    public GameObject[] Cabs;
    public GameObject[] Fronts;
    public GameObject[] Rears;
    public GameObject[] SedanCabRears;
    public GameObject[] SteeringWheels;
    //public GameObject[] FrontBumpers;
    //public GameObject[] RearBumpers;
    public GameObject[] Roofs;

    //Car Section Lists made from arrays
    public List<GameObject> frontList;
    public List<GameObject> cabList;
    public List<GameObject> rearList;
    public List<GameObject> sedanCabRearList;
    public List<GameObject> steeringwheelList;
    //public List<GameObject> frontBumperList;
    //public List<GameObject> rearBumperList;
    public List<GameObject> roofList;
    
    private int currentCab = 0;
    private int currentFront = 0;
    private int currentRear = 0;
    private int currentSteeringWheel = 0;
    //public int currentFrontBumper = 0;
    //public int currentRearBumper = 0;
    private int currentRoof = 0;
    private int currentSedanCabRear = 0;


    //Spawn area for mid section to spawn
    public Transform spawnArea;

    //Chosen objects to spawn
    private GameObject front;
    private GameObject cab;
    private GameObject rear;
    //public GameObject frontBumper;
    //public GameObject rearBumper;
    private GameObject roof;
    private GameObject sedanCabRear;
    private GameObject steeringwheel;

    // Start is called before the first frame update
    void Start() {
        // Set menus up
        customiserMain.SetActive(true);
        paintMenu.SetActive(false);
        paintMenuButton.SetActive(true);
        backTolevelMenuButton.SetActive(true);
        backButton.SetActive(false);
        viewCarButton.SetActive(true);
        sedanText.enabled = false;
        rearText.enabled = false;

        // Set up each section in all parts lists
        foreach (var midPart in Cabs) {
            cab = Instantiate (midPart, spawnArea.position, Quaternion.identity);
            cab.transform.SetParent(spawnArea);
            cab.SetActive(false);
            cabList.Add(cab);
        }

        foreach (var frontPart in Fronts) {
            midPartConnector socketFind = cab.GetComponent<midPartConnector>();
            front = Instantiate (frontPart, socketFind.frontSocket.transform.position, Quaternion.identity);
            front.SetActive(false);
            frontList.Add(front);
        }

        foreach (var rearPart in Rears) {
            midPartConnector socketFind = cab.GetComponent<midPartConnector>(); 
            rear = Instantiate (rearPart, socketFind.rearSocket.transform.position, Quaternion.identity);
            rear.SetActive(false);
            rearList.Add(rear);
        }

        foreach (var sedanCabRearPart in SedanCabRears) {
            midPartConnector socketFind = cab.GetComponent<midPartConnector>();
            sedanCabRear = Instantiate (sedanCabRearPart, socketFind.rearSocket.transform.position, Quaternion.identity);
            sedanCabRear.SetActive(false);
            sedanCabRearList.Add(sedanCabRear);
        }

        foreach (var roofPart in Roofs) {
            midPartConnector socketFind = cab.GetComponent<midPartConnector>();
            roof = Instantiate (roofPart, socketFind.roofSocket.transform.position, Quaternion.identity);
            roof.SetActive(false);
            roofList.Add(roof);
        }

        foreach (var steeringWheelPart in SteeringWheels) {
            midPartConnector socketFind = cab.GetComponent<midPartConnector>();
            steeringwheel = Instantiate (steeringWheelPart, socketFind.steeringSocket.transform.position, Quaternion.identity);
            steeringwheel.SetActive(false);
            steeringwheelList.Add(steeringwheel);
        }


        /*foreach (var rearBumperPart in RearBumpers) {
            rearPartConnector socketFindRear = rear.GetComponent<rearPartConnector>();
            rearBumper = Instantiate (rearBumperPart, socketFindRear.rearBumperSocket.transform.position, Quaternion.AngleAxis(180, Vector3.up));
            rearBumper.SetActive(false);
            rearBumperList.Add(rearBumper);
        }
        
        foreach (var frontBumperPart in FrontBumpers) {
            frontPartConnector socketFindFront = front.GetComponent<frontPartConnector>();
            frontBumper = Instantiate (frontBumperPart, socketFindFront.bullbarSocket.transform.position, Quaternion.identity);
            frontBumper.SetActive(false);
            frontBumperList.Add(frontBumper);
        }
        */

        showPartFromList();
    }

    void showPartFromList() {
        cabList[currentCab].SetActive(true);
        frontList[currentFront].SetActive(true);
        rearList[currentRear].SetActive(true);
        sedanCabRearList[currentSedanCabRear].SetActive(false);
        steeringwheelList[currentSteeringWheel].SetActive(true);
        //frontBumperList[currentFrontBumper].SetActive(false);
        //rearBumperList[currentRearBumper].SetActive(false);
        roofList[currentRoof].SetActive(true);
    }

   public void NextCab() {
        cabList[currentCab].SetActive(false);
    //Mid part next in series
    if (currentCab < cabList.Count - 1) {
            ++currentCab;
            cabList[currentCab].SetActive(true);
         } else {
                currentCab = 0;
                cabList[currentCab].SetActive(true);
                //showPartFromList();
    }
   }

    public void NextFront() {
        frontList[currentFront].SetActive(false);
    //Front part next in series 
    if (currentFront < frontList.Count - 1) {
            ++currentFront;
            frontList[currentFront].SetActive(true);
         } else {
                currentFront = 0;
                frontList[currentFront].SetActive(true);
                //showPartFromList();
    }
    }

    public void NextRear() {
    //Rear part next in series
    sedanText.enabled = false;
    rearText.enabled = false;
        
    if (currentCab==4 && currentRoof==0) { //sedan cab and no roof selected
        sedanText.enabled = true;
        rearList[currentRear].SetActive(false);
        sedanCabRearList[currentSedanCabRear].SetActive(false);
        if (currentSedanCabRear < sedanCabRearList.Count - 1) {
            ++currentSedanCabRear;
            sedanCabRearList[currentSedanCabRear].SetActive(true);
         } else {
                currentSedanCabRear = 0;
                sedanCabRearList[currentSedanCabRear].SetActive(true);
    }
    } else if (currentCab!=4) {
        sedanText.enabled = false;
        rearText.enabled = true;
        rearList[currentRear].SetActive(false);
        sedanCabRearList[currentSedanCabRear].SetActive(false);
        if (currentRear < rearList.Count - 1) {
            ++currentRear;
            rearList[currentRear].SetActive(true);
         } else {
                currentRear = 0;
                rearList[currentRear].SetActive(true);
    }
   }
    }

   /*public void NextFrontBumper() {
        frontBumperList[currentFrontBumper].SetActive(false);
    //Front part next in series 
    if (currentFrontBumper < frontBumperList.Count - 1) {
            ++currentFrontBumper;
            frontBumperList[currentFrontBumper].SetActive(true);
         } else {
                currentFrontBumper = 0;
                frontBumperList[currentFrontBumper].SetActive(true);
    }
    }

    public void NextRearBumper() {
        //rearBumperList[currentRearBumper].SetActive(false);
    //Front part next in series 
    if (currentRearBumper < rearBumperList.Count - 1) {
            ++currentRearBumper;
            rearBumperList[currentRearBumper].SetActive(true);
         } else {
                currentRearBumper = 0;
                rearBumperList[currentRearBumper].SetActive(true);
    }
    }*/

     public void NextRoof() {
        roofList[currentRoof].SetActive(false);
        if (currentCab !=4) {
    //roof next in series 
    if (currentRoof < roofList.Count - 1) {
            ++currentRoof;
            roofList[currentRoof].SetActive(true);
         } else {
                currentRoof = 0;
                roofList[currentRoof].SetActive(true);
    }
    } else {
        currentRoof = 0;
        roofList[currentRoof].SetActive(true);
    }
     }

    public void NextSteeringWheel() {
        steeringwheelList[currentSteeringWheel].SetActive(false);
    //Front part next in series 
    if (currentSteeringWheel < steeringwheelList.Count - 1) {
            ++currentSteeringWheel;
            steeringwheelList[currentSteeringWheel].SetActive(true);
         } else {
                currentSteeringWheel = 0;
                steeringwheelList[currentSteeringWheel].SetActive(true);
    }
    }

   public void PreviousCab() {
        cabList[currentCab].SetActive(false);
        if (currentCab ==0) {
            currentCab = cabList.Count - 1;
            cabList[currentCab].SetActive(true);
        } else {
        --currentCab;
        cabList[currentCab].SetActive(true);
    }
   }

   public void PreviousFront() {
        frontList[currentFront].SetActive(false);
        if (currentFront ==0) {
            currentFront = frontList.Count - 1;
            frontList[currentFront].SetActive(true);
        } else {
        --currentFront;
        frontList[currentFront].SetActive(true);
    }
   }

   public void PreviousRear() {
   //Rear part previous in series
    if (currentCab==4 && currentRoof==0) { //sedan cab and no roof selected
        rearList[currentRear].SetActive(false);
        sedanCabRearList[currentSedanCabRear].SetActive(false);
        if (currentSedanCabRear ==0) {
            currentSedanCabRear = sedanCabRearList.Count -1;
            sedanCabRearList[currentSedanCabRear].SetActive(true);
         } else {
            --currentSedanCabRear;
            sedanCabRearList[currentSedanCabRear].SetActive(true);
    }
    } else if (currentCab!=4) {
        rearList[currentRear].SetActive(false);
        sedanCabRearList[currentSedanCabRear].SetActive(false);
       if (currentRear ==0) {
            currentRear = rearList.Count - 1;
            rearList[currentRear].SetActive(true);
        } else {
        --currentRear;
        rearList[currentRear].SetActive(true);
    }
   }
    }

   /*public void PreviousFrontBumper() {
        frontBumperList[currentFrontBumper].SetActive(false);
        if (currentFrontBumper ==0) {
            currentFrontBumper = frontBumperList.Count - 1;
            frontBumperList[currentFrontBumper].SetActive(true);
        } else {
        --currentFrontBumper;
        frontBumperList[currentFrontBumper].SetActive(true);
    }
   }

   public void PreviousRearBumper() {
        rearBumperList[currentRearBumper].SetActive(false);
        if (currentRearBumper ==0) {
            currentRearBumper = rearBumperList.Count - 1;
            rearBumperList[currentRearBumper].SetActive(true);
        } else {
        --currentRearBumper;
        rearBumperList[currentRearBumper].SetActive(true);
    }
   }*/

   public void PreviousRoof() {
        roofList[currentRoof].SetActive(false);
        if (currentCab !=4) {
        if (currentRoof ==0) {
            currentRoof = roofList.Count - 1;
            roofList[currentRoof].SetActive(true);
        } else {
        --currentRoof;
        roofList[currentRoof].SetActive(true);
    }
   } else {
       currentRoof = 0;
        roofList[currentRoof].SetActive(true);
   }
   }

   public void PreviousSteeringWheel() {
        steeringwheelList[currentSteeringWheel].SetActive(false);
    if (currentSteeringWheel ==0) {
            currentSteeringWheel = steeringwheelList.Count- 1;
            steeringwheelList[currentSteeringWheel].SetActive(true);
         } else {
                --currentSteeringWheel;
                steeringwheelList[currentSteeringWheel].SetActive(true);
    }
    }

    //Menu button actions
    public void openPaintMenu() {
        paintMenu.SetActive(true);
        /*paintMenuButton.SetActive(false);
        backTolevelMenuButton.SetActive(false);
        backButton.SetActive(false);
        viewCarButton.SetActive(false);
        customiserMain.SetActive(true);*/
   }

    public void exitToMain() {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    public void viewCar() {
        customiserMain.SetActive(false);
        paintMenu.SetActive(false);
        paintMenuButton.SetActive(false);
        backTolevelMenuButton.SetActive(false);
        backButton.SetActive(true);
        viewCarButton.SetActive(false);
    }

   public void back() {
        customiserMain.SetActive(true);
        paintMenu.SetActive(false);
        paintMenuButton.SetActive(true);
        backTolevelMenuButton.SetActive(true);
        backButton.SetActive(false);
        viewCarButton.SetActive(true);
    }
}                                                        
