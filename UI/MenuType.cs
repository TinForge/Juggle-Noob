using UnityEngine;

/// <summary>
/// Subscribes to State.OnMenuCalled and displays corresponding menu
/// </summary>
public class MenuType : UIElementBase
{
	[SerializeField] private Menu menuType;

	protected virtual void Awake ()
	{
		base.Awake ();

		State.OnMenuCalled += MenuCall;
	}

	private void MenuCall (Menu state)
	{
		if (menuType == state)
			Enable ();
		else
			Disable ();
	}

}
