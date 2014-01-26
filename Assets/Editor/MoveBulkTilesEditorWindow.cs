using UnityEngine;
using UnityEditor;
using System.Collections;

public class MoveBulkTilesEditorWindow : EditorWindow {

	float amount;

	[MenuItem ("Boombots/Bulk Move Tiles")]
	static void ShowWindow () {
		EditorWindow.GetWindow(typeof(MoveBulkTilesEditorWindow));
	}

	void OnGUI () {

		amount = EditorGUILayout.FloatField("Amount",amount);

		if (GUILayout.Button("Move")) {


			BoxCollider2D[] tiles = GameObject.FindObjectsOfType<BoxCollider2D>();

			foreach (BoxCollider2D tile in tiles) {
				Vector3 pos = tile.gameObject.transform.position;

				Vector3 newPosition = new Vector3(pos.x, pos.y + amount, pos.z);

				tile.transform.position = newPosition;
			}


		}
	}
}
