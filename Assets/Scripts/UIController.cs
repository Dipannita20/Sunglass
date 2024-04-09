using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController instance;
    public RectTransform QuestionPanel;
    public RectTransform FoundationQuestion;
    public RectTransform SkinQuestion;
    public RectTransform GiftQuestion;
    public RectTransform BasedOnInformation;
    public RectTransform WelcomeFirstMessage;
    public RectTransform WelcomeSecondMessage;
    public RectTransform WelcomeOnTheChair;

    public RectTransform RefPanel;

    private Vector2 QuestionPanelOnScreenPosition;
    private Vector2 QuestionPanelResetPosition;

    private RectTransform activePanel;

    public GameObject RecomendedProduct;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //QuestionPanelResetPosition = FoundationQuestion.
        // QuestionPanelOnScreenPosition = new Vector3(-338, 63, 0);*/

        QuestionPanelResetPosition = QuestionPanel.anchoredPosition;
        QuestionPanelOnScreenPosition = RefPanel.anchoredPosition;
     
    }

    public IEnumerator StartQuestion()
    {
        yield return new WaitForSeconds(1f);
        QuestionPanel1stOption(QuestionPanel);
    }

    public void QuestionPanel1stOption(RectTransform panel)
    {
        if (activePanel)
            activePanel.anchoredPosition = QuestionPanelResetPosition;

        if (panel.name != "BasedOnInformation")
        {
            panel.anchoredPosition = QuestionPanelOnScreenPosition;
            activePanel = panel;
        }

        switch (panel.name)
        {

            case "QuestionPanel":
                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip1, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio3, 0.5f));
                break;

            case "FoundationQuestion":
                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip4, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio4, 0.5f));
                break;

            case "SkinQuestion":
                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip5, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio5, 0.5f));
                break;

            case "GiftQuestion":
                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip6, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio6, 0.5f));
                break;

            case "BasedOnInformation":
                StartCoroutine(AnimationControl.instance.PlayAnimationClip(AnimationControl.instance.lip7, AnimationControl.instance.idle1, AnimationControl.instance.lipaudio7, 0.5f));
                InputHandler.instance.ShowProductDetails(RecomendedProduct.transform);
                //Reset();
                break;
        }

        Debug.Log(panel.name);

        //

    }

    public void Reset()
    {
        if(activePanel)
           activePanel.anchoredPosition = QuestionPanelResetPosition;

        activePanel = null;
    }

}


