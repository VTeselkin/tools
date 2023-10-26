using GameSoft.Tools.ZenjectExtensions;
using UnityEngine;
using Zenject;
namespace GameSoft.Tools
{
	public class ApplicationPauseExecute : MonoBehaviour
	{
		[Inject] private readonly SignalBus _signalBus;

		private void OnApplicationPause(bool pauseStatus)
		{
			switch (pauseStatus)
			{
				case true:
					_signalBus.Fire<ApplicationPause>();
					break;
				default:
					_signalBus.Fire<ApplicationUnpause>();
					break;
			}
		}

		private void OnApplicationFocus(bool hasFocus)
		{
			_signalBus.Fire(new ApplicationFocus(hasFocus));
		}

#if UNITY_EDITOR
		public void Update()
		{
			if (Input.GetKeyDown(KeyCode.U) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				OnApplicationPause(false);
			}
        
			if (Input.GetKeyDown(KeyCode.P) && (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)))
			{
				OnApplicationPause(true);
			}
		}
#endif
	}
}