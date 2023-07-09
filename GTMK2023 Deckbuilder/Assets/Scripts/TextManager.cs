using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private List<Label> _labels = new List<Label>();
	public GUISkin defaultSkin;

	// -- METHODS --
	// import styles
	private void Start()
	{
		defaultSkin = (GUISkin)UnityEditor.AssetDatabase.LoadMainAssetAtPath("Assets/Styles/Default.GUISkin");
		GUI.skin = defaultSkin;
	}

	// update label position and lifetime
	private void Update()
	{
		var labelsToDelete = new List<Label>();

		for (var i = 0; i < _labels.Count; i++)
		{
			var lbl = _labels[i];
			lbl.Position += lbl.Velocity * Time.deltaTime; // move label
			lbl.Lifetime -= Time.deltaTime;
			if (lbl.Lifetime <= 0) // assign for deletion
			{
				labelsToDelete.Add(lbl);
			}
		}

		foreach (var del in labelsToDelete)
		{
			_labels.Remove(del); // removing from list is effectively same as deletion
		}
	}

	// draw all labels
	private void OnGUI()
	{
		foreach (Label lbl in _labels)
		{
			GUI.Label(new Rect(lbl.Position, lbl.Size), lbl.Text);
		}
	}

	// create new custom label
	public void NewCustomLabel(string text, GUISkin skin, float lifetime, Vector2 size, Vector2 position, Vector2 velocity)
	{
		var lbl = new Label(text, skin, lifetime, size, position, velocity);
	}
}
