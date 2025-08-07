using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace EasyUI.Dialogs {

	public enum DialogButtonColor {
		White,
		Purple,
		Magenta,
		Blue,
		Green,
		Yellow,
		Orange,
		Red
	}

	public class Dialog {
		public string Title = "Title";
		public string Message = "Message goes here.";
		public string ButtonText = "Close";

		public bool eventOnClose = false;
		public float FadeInDuration = .3f;
		public float DelayAfterClose = 0f;
		public DialogButtonColor ButtonColor = DialogButtonColor.White;
		public UnityAction OnClose = null;
	}

	public class DialogUI : MonoBehaviour {
		[SerializeField] GameObject canvas;
		[SerializeField] TMP_Text titleUIText;
		[SerializeField] TMP_Text messageUIText;
		[SerializeField] Button closeUIButton;
		Image closeUIButtonImage;
		TMP_Text closeUIButtonText;
		CanvasGroup canvasGroup;

		[Space ( 20f )]
		[Header ( "Close button colors" )]
		[SerializeField] Color[] buttonColors;

		Queue<Dialog> dialogsQueue = new Queue<Dialog> ( );
		Dialog dialog = new Dialog ( );
		Dialog tempDialog;

		[HideInInspector] public bool IsActive = false;



		void Awake()
		{

			closeUIButtonImage = closeUIButton.GetComponent<Image>();
			closeUIButtonText = closeUIButton.GetComponentInChildren<TMP_Text>();
			canvasGroup = canvas.GetComponent<CanvasGroup>();

			//Add close event listener
			closeUIButton.onClick.RemoveAllListeners();
			closeUIButton.onClick.AddListener(Hide);
			
		}

		
		public DialogUI SetTitle ( string title ) {
			dialog.Title = title;
			return this;
		}

		
		public DialogUI SetMessage ( string message ) {
			dialog.Message = message;
			return this;
		}

		
		public DialogUI SetButtonText ( string text ) {
			dialog.ButtonText = text;
			return this;
		}
		
		public DialogUI SetEventOnClose ( bool value ) {
			dialog.eventOnClose = value;
			return this;
		}

		
		public DialogUI SetButtonColor(DialogButtonColor color)
		{
			dialog.ButtonColor = color;
			return this;
		}
		public DialogUI SetDelayAfterClose(float delay)
		{
    		dialog.DelayAfterClose = delay;
    		return this;
		}

		
		public DialogUI SetFadeInDuration(float duration)
		{
			dialog.FadeInDuration = duration;
			return this;
		}

		
		public DialogUI OnClose ( UnityAction action ) {
			dialog.OnClose = action;
			return this;
		}

		//-------------------------------------
		
		public void Show ( ) {
			dialogsQueue.Enqueue ( dialog );
			//Reset Dialog
			dialog = new Dialog ( );

			if ( !IsActive )
				ShowNextDialog ( );
		}


		void ShowNextDialog ( ) {
			tempDialog = dialogsQueue.Dequeue ( );

			titleUIText.text = tempDialog.Title;
			messageUIText.text = tempDialog.Message;
			closeUIButtonText.text = tempDialog.ButtonText.ToUpper ( );
			closeUIButtonImage.color = buttonColors [ ( int )tempDialog.ButtonColor ];

			canvas.SetActive ( true );
			IsActive = true;
			StartCoroutine ( FadeIn ( tempDialog.FadeInDuration ) );
		}


		// Hide dialog
		public void Hide ( ) {
			canvas.SetActive ( false );
			IsActive = false;

			if (tempDialog.eventOnClose)
			{
				eventDecide();
			}
			// Invoke OnClose Event
			if (tempDialog.OnClose != null)
				tempDialog.OnClose.Invoke();

			StopAllCoroutines ( );

			float delay = tempDialog.DelayAfterClose;
			if (dialogsQueue.Count != 0)
    			StartCoroutine(DelayedShowNextDialog(delay));
		}
		private IEnumerator DelayedShowNextDialog(float delaySeconds)
		{
   			yield return new WaitForSeconds(delaySeconds);
    		ShowNextDialog();
		}

		public void eventDecide()
		{
		}


		//-------------------------------------

		IEnumerator FadeIn ( float duration ) {
			float startTime = Time.time;
			float alpha = 0f;

			while ( alpha < 1f ) {
				alpha = Mathf.Lerp ( 0f, 1f, (Time.time - startTime) / duration );
				canvasGroup.alpha = alpha;

				yield return null;
			}
		}
	}

}