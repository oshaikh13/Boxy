﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AutomationClient : NetworkBehaviour {

	private SteamVR_TrackedController rightController;
	private SteamVR_TrackedController leftController;

	[SyncVar (hook="PlayerPressedChanged")]
	private string playerPressed = "";

	private void Start() {
		// controller = GetComponent<SteamVR_TrackedController>();
		rightController = GameObject.Find("[CameraRig]").transform.GetChild(0).GetComponent<SteamVR_TrackedController>();
		leftController = GameObject.Find("[CameraRig]").transform.GetChild(1).GetComponent<SteamVR_TrackedController>();

		rightController.TriggerClicked += HandleTriggerClicked;
		leftController.TriggerClicked += HandleTriggerClicked;

	}

	void HandleTriggerClicked (object sender, ClickedEventArgs e) {

		if (!isLocalPlayer)
			return;
		
		CmdPlayerPressed (EnvVariables.DisplayName);

	}

	void PlayerPressedChanged(string readyPlayer) {
		GameObject.Find("Automator").gameObject.GetComponent<AutomationController>().addPlayersReady (readyPlayer, isServer);
	}


	[Command]
	void CmdPlayerPressed(string displayName) {
		playerPressed = displayName;
	}

}
