using UnityEngine;
using UnityEngine.UI;
 
public class AnimatorScript : MonoBehaviour
{
    public Animator gearFaultAnim;
    public GameObject gearboxGearAnimationModel;
    public Animator bearingFaultAnim;
    public GameObject gearboxBearingAnimationModel;
    public Text animationTitle;
    public Text animationDetails;
    public Text animationStepDetails;
    public FusionManager fusionManager;
    public Text gearAnimationButtonText;
    public Text bearingAnimationButtonText;

    private string defaultAnimationTitle = "No Animation";
    private float animSpeed = 0.1f;
    private int faultStep = 0; //Offset step by 1
    private int animationModelActivationStatus = 0; // 0 - None, 1 - Gear, 2 - Bearing

    void Start ()
    {
        gearboxBearingAnimationModel.gameObject.SetActive(false);
        gearboxGearAnimationModel.gameObject.SetActive(false);
        gearFaultAnim.enabled = false;
        bearingFaultAnim.enabled = false;
    }

    public void GearMaintenanceButtonPress()
    {
        faultStep = 0;
        fusionManager.toggleCADActivationStatus(false);
        fusionManager.setCADModelInactive();
        activateAnimationModel(1);
        PlayMaintenanceAnimation();
    }

    public void BearingMaintenanceButtonPress()
    {
        faultStep = 0;
        fusionManager.toggleCADActivationStatus(false);
        fusionManager.setCADModelInactive();
        activateAnimationModel(2);
        PlayMaintenanceAnimation();
    }

    public string getStepDetails(int modelType, int stepNumber)
    {
        if (modelType == 1)
        {
            switch (stepNumber)
            {
                case 1:
                    return "Lift up the shaft with the faulty gear";
                case 2:
                    return "Remove the other components to extract the faulty gear";
                case 3:
                    return "Reinsert the components in order with a new gear";
                case 4:
                    return "Place the shaft back in the original position";
                default:
                    break;

            }
        }
        if (modelType == 2)
        {
            switch (stepNumber)
            {
                case 1:
                    return "Lift up the shaft with the faulty bearing";
                case 2:
                    return "Extract the faulty bearing";
                case 3:
                    return "Reinsert a new bearing";
                case 4:
                    return "Place the shaft back in the original position";
                default:
                    break;

            }
        }
        return "";
    }

    public void updateStepDetails()
    {
        switch (animationModelActivationStatus)
        {
            case 1:
                animationTitle.text = "Gear Maintenance Animation";
                animationDetails.text = getStepDetails(1, faultStep + 1);
                animationStepDetails.text = "Step  " + (faultStep+1).ToString() +" / 4";
                break;
            case 2:
                animationTitle.text = "Bearing Maintenance Animation";
                animationDetails.text = getStepDetails(2, faultStep + 1);
                animationStepDetails.text = "Step  " + (faultStep+1).ToString() + " / 4";
                break;
            default:
                animationTitle.text = defaultAnimationTitle;
                animationDetails.text = "";
                animationStepDetails.text = "";
                break;
        }
    }

    private void activateAnimationModel(int key)
    {
        switch (key)
        {
            case 1:
                gearboxGearAnimationModel.gameObject.SetActive(true);
                gearFaultAnim.enabled = true;
                gearboxBearingAnimationModel.gameObject.SetActive(false);
                bearingFaultAnim.enabled = false;
                gearFaultAnim.speed = animSpeed;
                animationModelActivationStatus = 1;
                gearAnimationButtonText.color = Color.red;
                bearingAnimationButtonText.color = Color.black;
                break;
            case 2:
                gearboxBearingAnimationModel.gameObject.SetActive(true);
                bearingFaultAnim.enabled = true;
                gearboxGearAnimationModel.gameObject.SetActive(false);
                gearFaultAnim.enabled = false;
                bearingFaultAnim.speed = animSpeed;
                animationModelActivationStatus = 2;
                gearAnimationButtonText.color = Color.black;
                bearingAnimationButtonText.color = Color.red;
                break;
            default:
                gearboxGearAnimationModel.gameObject.SetActive(false);
                gearFaultAnim.enabled = false;
                gearboxBearingAnimationModel.gameObject.SetActive(false);
                bearingFaultAnim.enabled = false;
                animationModelActivationStatus = 0;
                gearAnimationButtonText.color = Color.black;
                bearingAnimationButtonText.color = Color.black;
                break;
        }
    }

    public void NextStepButtonPress()
    {
        faultStep += 1;
        faultStep %= 4;
        PlayMaintenanceAnimation();
    }

    public void PreviousStepButtonPress()
    {
        if (faultStep != 0)
        {
            faultStep -= 1;
        }        
        PlayMaintenanceAnimation();
    }

    public void maintenanceReturnButtonPress()
    {
        activateAnimationModel(0);
        fusionManager.toggleCADActivationStatus(true);
        animationTitle.text = "Animation Step";
        animationDetails.text = " ";
        animationStepDetails.text = " ";
    }

    private void PlayMaintenanceAnimation()
    {
        Debug.Log("Model: " + animationModelActivationStatus.ToString() + "Step: " + faultStep.ToString());
        updateStepDetails();
        if (animationModelActivationStatus == 1)
        {
            switch (faultStep)
            {
                case 1:
                    gearFaultAnim.Play("Gear Fault Step 2", 0);
                    break;
                case 2:
                    gearFaultAnim.Play("Gear Fault Step 3", 0);
                    break;
                case 3:
                    gearFaultAnim.Play("Gear Fault Step 4", 0);
                    break;
                default:
                    gearFaultAnim.Play("Gear Fault Step 1", 0);
                    break;
            }
        }
        if (animationModelActivationStatus == 2)
        {
            switch (faultStep)
            {
                case 1:
                    bearingFaultAnim.Play("Bearing Fault Step 2", 0);
                    break;
                case 2:
                    bearingFaultAnim.Play("Bearing Fault Step 3", 0);
                    break;
                case 3:
                    bearingFaultAnim.Play("Bearing Fault Step 4", 0);
                    break;
                default:
                    bearingFaultAnim.Play("Bearing Fault Step 1", 0);
                    break;
            }
        }
    }
}
