using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Label
{
    public string Text { get; set; }
    public GUISkin Skin { get; set; }
    public float Lifetime { get; set; }
    public Vector2 Size { get; set; }
    public Vector2 Position { get; set; }
    public Vector2 Velocity { get; set; }

    // constructor
    public Label(string text, GUISkin skin, float lifetime, Vector2 size, Vector2 position, Vector2 velocity)
	{
        this.Text = text;
        this.Skin = skin;
        this.Size = size;
        this.Position = position;
        this.Lifetime = lifetime;
        this.Velocity = velocity;
	}
}
