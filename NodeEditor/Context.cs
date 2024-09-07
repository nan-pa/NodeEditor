using System.Collections;
/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-04 06:09:25
 * - Git: https://github.com/svan9
 **************************************************/

using System;
using System.Numerics;
using UnityEditor;
using UnityEngine;

namespace Nanpa.Core.NodeEditor {

class GUIContext {
	public static GUIContext instance;

	private Texture2D _M_circle_texture; 
	public Texture2D emptyTex;
	internal float lodLevel = 0;

	public static float LodLevel(float n) {
		return n/GetInstance().lodLevel;
	}

	public GUIContext() { 
		_M_circle_texture = Context.MakeTex(100, 100, new Color(1.0f, 1.0f, 1.0f, 0.0f));
		emptyTex = Context.MakeTex(100, 100, new Color(1.0f, 1.0f, 1.0f, 0.0f));
	}

	void _DrawCircle(Rect position, Color color) {
		var _texture = Context.MakeTexCircle((int)position.width, (int)position.height, color);
		GUI.DrawTexture(position, _texture);
	}
	void _DrawCircle(Rect position, Color color, int radius) {
		var _texture = Context.MakeTexCircle((int)position.width, (int)position.height, color, radius);
		GUI.DrawTexture(position, _texture, ScaleMode.ScaleAndCrop);
	}

	void _DrawText(Rect position, GUIContent content) {
		var __style = new GUIStyle(GUI.skin.box);
		__style.alignment = TextAnchor.MiddleLeft;
		__style.normal.background = emptyTex;
		__style.fontSize = (int)(15/lodLevel);
		GUI.Box(position, content, __style);
	}

	void _DrawText(Rect position, GUIContent content, TextAnchor anchor) {
		var __style = new GUIStyle(GUI.skin.box);
		__style.alignment = anchor;
		__style.normal.background = emptyTex;
		__style.fontSize = (int)(15/lodLevel);
		GUI.Box(position, content, __style);
	}

	public static void DrawCircle(Rect position, Color color) {
		GetInstance()._DrawCircle(position, color);
	}
	public static void DrawCircle(Rect position, int radius, Color color) {
		GetInstance()._DrawCircle(position, color, radius);
	}
	public static void DrawText(Rect position, string text) {
		var __content = new GUIContent(text); 
		GetInstance()._DrawText(position, __content);
	}
	public static void DrawText(Rect position, string text, TextAnchor anchor) {
		var __content = new GUIContent(text); 
		GetInstance()._DrawText(position, __content, anchor);
	}

	public static GUIContext GetInstance() {
		return instance ??= new();
	}
}

class Context {
	public Color color;
	internal float zoom;
	internal UnityEngine.Vector2 size;
	internal UnityEngine.Vector2 offset;
	internal Rect bounds;

	float lodLevel = 0;

	internal void Init(UnityEngine.Vector2 _offset, UnityEngine.Vector2 _size, Rect _bounds, float _zoom) {
		offset = _offset;
		zoom = _zoom;
		bounds = _bounds;
		size = _size;
	}

	internal Rect position;
	internal void Arc(float x, float y, float r, float startAngle, float endAngle) {}

	internal void Fill(Color color) {
		var step0 = (int)Mathf.Pow(10, lodLevel);
		int halfCount = (int)(step0 * Constaints.CELLS_IN_LINE_COUNT / 2 * 10);
		var length = halfCount * Constaints.DEFAULT_CELL_SIZE;
		Handles.color = color;
		Handles.DrawAAConvexPolygon(
			GridToGUI(new UnityEngine.Vector2(-length+bounds.x, -length+bounds.y)),
			GridToGUI(new UnityEngine.Vector2(-length+bounds.x, length+bounds.y-bounds.height)),
			GridToGUI(new UnityEngine.Vector2(length-bounds.width, length-bounds.height)),
			GridToGUI(new UnityEngine.Vector2(length-bounds.width, -length+bounds.y))
		);
	}

	internal void FillRect(Rect rect, Color color) {
		// var step0 = (int)Mathf.Pow(10, lodLevel);
		// int halfCount = (int)(step0 * Constaints.CELLS_IN_LINE_COUNT / 2 * 10);
		// var length = halfCount * Constaints.DEFAULT_CELL_SIZE;
		Handles.color = color;
		Handles.DrawAAConvexPolygon(
			GridToGUI(new UnityEngine.Vector2(rect.x, rect.y)),
			GridToGUI(new UnityEngine.Vector2(rect.x, rect.y-rect.height)),
			GridToGUI(new UnityEngine.Vector2(rect.width, rect.height)),
			GridToGUI(new UnityEngine.Vector2(rect.width, rect.y))
		);
	}
	
	internal void DrawLine(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2) {
		Handles.color = color;
		Handles.DrawLine(p1, p2);
	}
	
	internal float Level(float n) {
		return n/LodLevelFNa();
	}

	internal int LodLevel() {
		if (lodLevel == 0) {
			lodLevel = MathF.Log(zoom) / 1.5f;
			lodLevel = lodLevel > 0 ? lodLevel : 0;
			// GUIContext.GetInstance().lodLevel = lodLevel;
		}
		return (int)lodLevel;
	}

	internal float LodLevelFNa() {
		lodLevel = MathF.Log(zoom) / 1.5f;
		GUIContext.GetInstance().lodLevel = lodLevel;
		return lodLevel;
	}

	internal float LodLevelF() {
		if (lodLevel == 0) {
			lodLevel = MathF.Log(zoom) / 1.5f;
			lodLevel = lodLevel > 0 ? lodLevel : 0;
			// GUIContext.GetInstance().lodLevel = lodLevel;
		}
		return lodLevel;
	}
	
	internal UnityEngine.Vector2 GridToGUI(UnityEngine.Vector2 vec) {
		UnityEngine.Vector2 newVector;

		newVector.x = ((vec.x - offset.x) / zoom)	+	(size.x / 2.0f);
		newVector.y = ((-vec.y - offset.y) / zoom) +	(size.y / 2.0f);

		return newVector;
	}

	public static Texture2D MakeTex(int width, int height, Color col) {
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new Texture2D( width, height );
		result.SetPixels( pix );
		result.Apply();
		return result;
	}

	public static Texture2D MakeTexCircle(int width, int height, Color color) {
		Texture2D tex = new(width, height);
		int x = height/2;
		int y = height/2;
		int radius = height/2;
		float rSquared = radius * radius;
    for (int u = x - radius; u < x + radius + 1; u++)
        for (int v = y - radius; v < y + radius + 1; v++)
            if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                tex.SetPixel(u, v, color);
		tex.Apply();
		return tex;
	}

	public static Texture2D MakeTexCircle(int width, int height, Color color, int radius) {
		var bg = new Color(1.0f, 1.0f, 1.0f, 0.0f);
		Color[] pix = new Color[width * height];
		for( int i = 0; i < pix.Length; ++i ) {
			pix[ i ] = bg;
		}
		Texture2D tex = new(width, height);
		tex.SetPixels(pix);
		int x = width/2;
		int y = height/2;
		
		float rSquared = radius * radius;
    for (int u = x - radius; u < x + radius + 1; u++)
        for (int v = y - radius; v < y + radius + 1; v++)
            if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                tex.SetPixel(u, v, color);
		tex.Apply();
		return tex;
	}

	public static Texture2D DrawCircle(ref Texture2D tex, Color color, int x, int y, int radius = 3) {
    float rSquared = radius * radius;
    for (int u = x - radius; u < x + radius + 1; u++)
        for (int v = y - radius; v < y + radius + 1; v++)
            if ((x - u) * (x - u) + (y - v) * (y - v) < rSquared)
                tex.SetPixel(u, v, color);
    return tex;
	}

	public static Texture2D MakeTex(UnityEngine.Vector2 size, Color col) {
		Color[] pix = new Color[(int)(size.x * size.y)];
		for( int i = 0; i < pix.Length; ++i )
		{
			pix[ i ] = col;
		}
		Texture2D result = new((int)size.x, (int)size.y);
		result.SetPixels(pix);
		result.Apply();
		return result;
	}
	
}

}