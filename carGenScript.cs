using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carGenScript : MonoBehaviour {

    public List<GameObject> Cabs;
    public List<GameObject> sedanRears;
    public List<GameObject> Fronts;
    public List<GameObject> Rears;
    public List<GameObject> Roofs;
    public List<GameObject> FrontBumpers;
    //public List<GameObject> RearBumpers;
    public List<GameObject> SteeringWheels;

    // Accessories
    public List<GameObject> Cargo;
    public List<GameObject> Lights;
    public List<GameObject> RoofRacks;

   // public List<GameObject> generatedCar = new List<GameObject>();

    //public GameObject 

    //chosen parts
    public GameObject instCab;
    public GameObject instFront;
    public GameObject instRear;
    public GameObject instFBumper;
    public GameObject instRBumper;
    public GameObject instSteeringWheel;
    public GameObject instRoof;

    public GameObject instCargo;
    public GameObject instLights;
    public GameObject instRoofrack;
    public GameObject instTLights;

    private GameObject randomSedanRear;
    private GameObject randomRear;

    public float [] lightPos = new float[3];

    public Vector3 spawnPos;
    public Vector3 point;
    private Color chosenAccessoryColour; //change to private once confirmed working
    private Color chosenBottomColour;
    private Color chosenFenderColour;


    // Start is called before the first frame update
    void Start()
    {
        generateCar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            point = spawnPos;
            generateCar();
        }
    }

    public void generateCar() {

        if (instCab !=null) {
            Destroy(instCab);
        }
  
        //-----Generate random Cab-----------------
        GameObject randomCab = GetRandomPart(Cabs);
        
        instCab = Instantiate(randomCab, point, Quaternion.identity);
        midPartConnector cSocket = instCab.GetComponent<midPartConnector>(); //connects steering wheel, rear and front sections to car

        //-----Generate random Steering Wheel-----------------
        GameObject randomSteeringWheel = GetRandomPart(SteeringWheels);
        instSteeringWheel = Instantiate(randomSteeringWheel, cSocket.steeringSocket.transform.position, Quaternion.identity);
        instSteeringWheel.transform.parent = instCab.transform;

        //-----Generate random Front-----------------
        GameObject randomFront = GetRandomPart(Fronts);
        instFront = Instantiate(randomFront, cSocket.frontSocket.transform.position, Quaternion.identity);
        instFront.transform.parent = instCab.transform;

        //-----Generate random Roof-----------------
        //roof spawning and roof rules
        //if cab is sedan cab, spawn sedan rears
        if (randomCab == Cabs[0]) {
                randomSedanRear = GetRandomPart(sedanRears);
                instRear = Instantiate(randomSedanRear, cSocket.rearSocket.transform.position, Quaternion.identity);
                instRear.transform.parent = instCab.transform;
        }

        //if cab is racing cab, spawn any rear
        if (randomCab == Cabs[1]) {
                randomRear = GetRandomPart(Rears);
                instRear = Instantiate(randomRear, cSocket.rearSocket.transform.position, Quaternion.identity);
                instRear.transform.parent = instCab.transform;
        }

        //Spawn no roof if cab is sedan cab or racing cab
        if ((randomCab == Cabs[0]) || (randomCab == Cabs[1])) {
                GameObject randomRoof = Roofs[7]; //no roof
                instRoof = Instantiate(randomRoof, cSocket.roofSocket.transform.position, Quaternion.identity);
                instRoof.transform.parent = instCab.transform;
            
        //If cab is neither of those, spawn any roof and rear
        } else if ((randomCab != Cabs[0]) && (randomCab!= Cabs[1])){
                randomRear = GetRandomPart(Rears);
                instRear = Instantiate(randomRear, cSocket.rearSocket.transform.position, Quaternion.identity);
                instRear.transform.parent = instCab.transform;

                GameObject randomRoof = GetRandomPart(Roofs); 
                instRoof = Instantiate(randomRoof, cSocket.roofSocket.transform.position, Quaternion.identity);
                instRoof.transform.parent = instCab.transform;
        }

        //-----Generate random Rear Bumper-----------------
        //literal asset flip of front bumper, change headlight mat to red in generate color section
        rearPartConnector rSocket = instRear.GetComponent<rearPartConnector>(); //connects rear bumper to rear
        GameObject randomRearBumper = GetRandomPart(FrontBumpers);
        
        //Make sure all rear beds spawn with rear lights
        if ((randomRear = Rears[0]) || (randomRear = Rears[1]) || (randomRear = Rears[5]) || (randomRear = Rears[6]) || (randomRear = Rears[13])) {
            instRBumper = Instantiate(FrontBumpers[Random.Range(0, 4)], rSocket.rearBumperSocket.transform.position, Quaternion.AngleAxis(180, Vector3.up));
        } else {
            instRBumper = Instantiate(randomRearBumper, rSocket.rearBumperSocket.transform.position, Quaternion.AngleAxis(180, Vector3.up));
        }
        instRBumper.transform.parent = instRear.transform;

        //-----Generate random Front Bumper-----------------
        frontPartConnector fSocket = instFront.GetComponent<frontPartConnector>(); //connects front bumper to front
        GameObject randomFrontBumper = GetRandomPart(FrontBumpers);
        instFBumper = Instantiate(randomFrontBumper, fSocket.bullbarSocket.transform.position, Quaternion.identity);
        instFBumper.transform.parent = instFront.transform;

        //-----Generate random spotlights on front bumper-----------------
        spotlightConnector spotlightSocket = instFBumper.GetComponent<spotlightConnector>(); //connects lights to front bumper
        if (instFBumper.GetComponent<spotlightConnector>() != null) {
            float randValue = Random.value;
            if (randValue >= 0.9f) {
                GameObject randomLights = GetRandomPart(Lights);
                instLights = Instantiate(randomLights, spotlightSocket.lightSocket.transform.position, Quaternion.identity);
                instLights.transform.parent = instFBumper.transform;
            } else if (randValue <= 0.9f) {
                instLights = null;
            }
        } else {
            spotlightSocket = null;
        }

        //-----Generate random spotlights on rear bumper-----------------
        spotlightConnector taillightSocket = instRBumper.GetComponent<spotlightConnector>(); //connects lights to front bumper
        if (instRBumper.GetComponent<spotlightConnector>() != null) {
            float randValue = Random.value;
            if (randValue >= 0.9f) {
                GameObject randomLights = GetRandomPart(Lights);
                //flip lights around like front bumper flipped to rear bumper, adjust position and make inline with rear bumper

                if ((randomLights != Lights[4]) || (randomLights != Lights[5])) { //if lights aren't single, position in centre
                instTLights = Instantiate(randomLights, (taillightSocket.lightSocket.transform.position + new Vector3(0,0.05f,0.02f)), Quaternion.AngleAxis(180, Vector3.right));  //this flips it upside down to align with bumper
                } else {
                    //if lights are single choose random offset (left, right, center bumper)
                    instTLights = Instantiate(randomLights, (taillightSocket.lightSocket.transform.position + new Vector3((lightPos[Random.Range(0, 4)]),0.05f,0.02f)), Quaternion.AngleAxis(180, Vector3.right));  //this flips it upside down to align with bumper
                }
                instTLights.transform.parent = instRBumper.transform;
            } else if (randValue <= 0.9f) {
                instTLights = null;
            }
        } else {
            spotlightSocket = null;
        }

        //-----Generate random roof rack for roof and sedan rear-----------------
        roofrackConnector rrSocket = instRoof.GetComponent<roofrackConnector>(); //reference script that connects roofrack to roof
        if (instRoof.GetComponent<roofrackConnector>() != null) {
            float randValue = Random.value;
            if (randValue >= 0.9f) { // make roofrack spawn rare
                GameObject randomRoofrack = GetRandomPart(RoofRacks);
                randomRoofrack.transform.localScale = new Vector3(1f,1f,1f);
                instRoofrack = Instantiate(randomRoofrack, rrSocket.roofrackSocket.transform.position, Quaternion.identity);
                instRoofrack.transform.parent = instRoof.transform;
            } else if (randValue <= 0.9f) {
                instRoofrack = null;
            }
        } else {
            rrSocket = null;
        }

        // Random roof rack for sedan rear
        roofrackConnector rrSedanSocket = instRear.GetComponent<roofrackConnector>(); //connects roofrack to sedan roof
        if (instRear.GetComponent<roofrackConnector>() != null) {
            GameObject randomRoofrack = GetRandomPart(RoofRacks);
            
            /*foreach (GameObject sedanRear in sedanRears)
            {
                if (sedanRear == sedanRears[Random.Range(0, 3)]) {
                    randomRoofrack.transform.localScale = new Vector3(1f,1f,1.1f);
                } else if (sedanRear != sedanRears[Random.Range(0, 3)]) {
                    randomRoofrack.transform.localScale = new Vector3(1f,1f,2.3f);
                //randomRoofrack.transform.localScale = new Vector3(1f,1f,1.1f);
            }
            }*/

            //I know I Should use foreach loop here (above attempt) but can't seem to specify array range, this bad solution works for now

            //hatchbacks should spawn with small roofrack
            if ((randomSedanRear == sedanRears[0]) || (randomSedanRear == sedanRears[1]) || (randomSedanRear == sedanRears[2]) || (randomSedanRear == sedanRears[3])  || (randomSedanRear == sedanRears[4]) || (randomSedanRear == sedanRears[5]) || (randomSedanRear == sedanRears[6])  || (randomSedanRear == sedanRears[7])) { 
                randomRoofrack.transform.localScale = new Vector3(1f,1f,1.1f);
            } else { //any other sedan rears should spawn with scaled up roof rack
                randomRoofrack.transform.localScale = new Vector3(1f,1f,2.3f);
            }
            instRoofrack = Instantiate(randomRoofrack, rrSedanSocket.roofrackSocket.transform.position, Quaternion.identity);
            instRoofrack.transform.parent = instRear.transform;
        } else {
            rrSedanSocket = null;
        }

        //-----Generate random cargo on rear bed area-----------------
        /*cargoConnector cgoSocket = instRear.GetComponent<cargoConnector>(); //connects cargo to rear
        if (instRear.GetComponent<cargoConnector>() != null) { // checks if cargo script exists on rear
            GameObject randomCargo = GetRandomPart(Cargo);

            //Not working at the moment, keeps spawning wrong cargo on long flatbed
            if (randomRear != Rears[4]) { // if not long flatbed
                randomCargo = Cargo[1];
            } else {
                randomCargo = Cargo[3];
                //Generate anything but dissassembled bike for long flatbed
            }
            instCargo = Instantiate(randomCargo, cgoSocket.cargoSocket.transform.position, Quaternion.identity);
            instCargo.transform.parent = instRear.transform;
            
            
        } else if (instRear.GetComponent<cargoConnector>() == null) { // if no cargo script detected, cargo script doesn't exist
            cSocket = null;
            //instCargo = null;
        }*/

        //-----Generate random Part function-----------------
        GameObject GetRandomPart(List<GameObject> partList) {
        int randomNumb = Random.Range(0, partList.Count - 1);
        return partList[randomNumb];
    }
        generateColour();
    }

    void generateColour() {
        GameObject colourManager = GameObject.Find("colourManager");
        colourManagerScript colour = colourManager.GetComponent<colourManagerScript>();

        Color topBodyColour = colour.carColours[Random.Range(0, colour.carColours.Length)];
        Color bottomBodyColour = colour.carColours[Random.Range(0, colour.carColours.Length)];
        Color windowColour = colour.windowColours[Random.Range(0, colour.windowColours.Length)];
        Color coverColour = colour.coverColours[Random.Range(0, colour.coverColours.Length)];

           foreach (Material mat in instCab.GetComponent<Renderer>().materials){
            if (mat.name == "Firstbodycolour (Instance)") {
                mat.color = topBodyColour;
            }

            if (mat.name == "Secondbodycolour (Instance)") {
                 float randValue = Random.value;
                if (randValue >= 0.6f) {
                    chosenBottomColour = mat.color = bottomBodyColour;
                } else {
                   chosenBottomColour = mat.color = topBodyColour;
                }
            }

            if (mat.name == "Glass (Instance)") {
                mat.color = windowColour;
        }

            if (mat.name == "Accents (Instance)") {
                mat.color = coverColour;
        }

            if (mat.name == "Cover (Instance)") {
                mat.color = colour.coverColours[Random.Range(0, colour.coverColours.Length)];
        }

    }

    foreach (Material mat in instSteeringWheel.GetComponent<Renderer>().materials){
            if (mat.name == "Secondbodycolour (Instance)") {
                mat.color = coverColour;
            }

            if (mat.name == "Cover (Instance)") {
                mat.color = topBodyColour;
            }

    }

    foreach (Material mat in instRoof.GetComponent<Renderer>().materials){
            if (mat.name == "Firstbodycolour (Instance)") {
                mat.color = topBodyColour;
                }

            if (mat.name == "Glass (Instance)") {
                mat.color = windowColour;
        }
            if (mat.name == "Cover (Instance)") {
                mat.color = coverColour;
        }
    }

        foreach (Material mat in instFront.GetComponent<Renderer>().materials){
            if (mat.name == "Firstbodycolour (Instance)") {
                mat.color = topBodyColour;
            }

            if (mat.name == "Secondbodycolour (Instance)") {
                 float randValue = Random.value;
                    mat.color = chosenBottomColour;
            }

             if (mat.name == "FenderMat (Instance)") {
                float randValue = Random.value;
                if (randValue >= 0.6f) {
                    chosenFenderColour = mat.color = coverColour;
                } else {
                   chosenFenderColour = mat.color = chosenBottomColour;
                }
            }
    }

    foreach (Material mat in instRear.GetComponent<Renderer>().materials) {
            if (mat.name == "Firstbodycolour (Instance)") {
                mat.color = topBodyColour;
        }
            if (mat.name == "Secondbodycolour (Instance)") {
                    mat.color = chosenBottomColour;
            }

             if (mat.name == "Cover (Instance)") {
                mat.color = colour.coverColours[Random.Range(0, colour.coverColours.Length)];
                } 

            if (mat.name == "FenderMat (Instance)") {
                   mat.color = chosenFenderColour;
             }

              if (mat.name == "Glass (Instance)") {
                mat.color = windowColour;
        }
    }

    foreach (Material mat in instRBumper.GetComponent<Renderer>().materials) {
            if (mat.name == "Headlights (Instance)") {
                mat.SetColor("_EmissionColor", Color.red);
        }
    }

    if (instTLights != null) {
    foreach (Material mat in instTLights.GetComponent<Renderer>().materials) {
            if (mat.name == "Headlights (Instance)") {
                mat.SetColor("_EmissionColor", Color.red);
        }
    }
    }

    if (instCargo != null) {
    foreach (Material mat in instCargo.GetComponent<Renderer>().materials) {
            if (mat.name == "Firstbodycolour (Instance)") {
                mat.color = colour.carColours[Random.Range(0, colour.carColours.Length)];
            }

            if (mat.name == "Secondbodycolour (Instance)") {
                 float randValue = Random.value;
                if (randValue >= 0.6f) {
                    mat.color = colour.coverColours[Random.Range(0, colour.coverColours.Length)];
                } else {
                   mat.color = colour.carColours[Random.Range(0, colour.carColours.Length)];
                }
            }

            if (mat.name == "Cover (Instance)") {
                mat.color = colour.coverColours[Random.Range(0, colour.coverColours.Length)];
            }

            if (mat.name == "Chassis (Instance)") {
                mat.color = colour.coverColours[Random.Range(0, colour.coverColours.Length)];
            }
        }
    }
}
}
