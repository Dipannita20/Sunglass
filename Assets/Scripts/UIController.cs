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
    public RectTransform Welcome;

    private Vector2 QuestionPanelOnScreenPosition;
    private Vector2 QuestionPanelResetPosition;

    private Vector2 FoundationQuestionPanelOnScreenPosition;
    private Vector2 FoundationQuestionPanelResetPosition;

    private Vector2 SkinQuestionOnScreenPosition;
    private Vector2 SkinQuestionResetPosition;

    private Vector2 GiftQuestionOnScreenPosition;
    private Vector2 GiftQuestionResetPosition;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //QuestionPanelResetPosition = FoundationQuestion.
        // QuestionPanelOnScreenPosition = new Vector3(-338, 63, 0);*/

        //FoundationQuestion.anchoredPosition = new Vector2(340, 63);

        QuestionPanelOnScreenPosition = new Vector2(60, -177);
        QuestionPanelResetPosition = QuestionPanel.anchoredPosition;
        FoundationQuestionPanelResetPosition = FoundationQuestion.anchoredPosition;
        SkinQuestionResetPosition = SkinQuestion.anchoredPosition;
        GiftQuestionResetPosition = GiftQuestion.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator StartQuestion()
    {
        yield return new WaitForSeconds(1f);
        QuestionPanel.anchoredPosition = QuestionPanelOnScreenPosition;
    }

    public void QuestionPanel1stOption()
    {
        Debug.Log("1st");
    }
    public void QuestionPanel2ndOption()
    {
        Debug.Log("2nd");
    }
    public void QuestionPanel3rdOption()
    {
        Debug.Log("3rd");
    }
}
