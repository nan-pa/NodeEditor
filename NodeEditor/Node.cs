using System.Collections;
/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-04 06:19:26
 * - Git: https://github.com/svan9
 **************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using Vector2 = UnityEngine.Vector2;

namespace Nanpa.Core.NodeEditor {

class Node {
	public static string name = "Node";
	public Vector2 pos = Vector2.zero;
	public Vector2 size = Vector2.zero;
	
	public List<Widget> widgets = new();
	public List<OutWidget> outWidgets = new();

	Vector2 min;
	Vector2 max;

	private GUIStyle _M_style = null;

	Context ctx = null;
	
	public Node() {
		_M_style = new GUIStyle();
		widgets.Add(new Widget());
		outWidgets.Add(new OutWidget());
	}

	public void Resize() {
		_M_style = new GUIStyle(GUI.skin.box);
		_M_style.normal.background = Context.MakeTex(size, Pallete.NodeBackground);
		// _M_style.normal.textColor = Color.white;
	}

	public void Move(Vector2 dv) {
		if (ctx == null) { return; }
		pos.x += -dv.x * ctx.zoom;
		pos.y += dv.y * ctx.zoom;
	}

	void Init() {
		if (_M_style == null) {
			Resize();
		}
	}

	public void OnGUI(Rect position, Context ctx) {
		GUI.backgroundColor = Pallete.NodeBackground;
		GUI.skin.box.alignment = TextAnchor.UpperLeft;
		GUI.skin.box.padding = new RectOffset(10,10,10,10);
		var content = new GUIContent("Hellow");
		var __style = new GUIStyle(GUI.skin.box);
		__style.fontSize = (int)GUIContext.LodLevel(18);
		min = position.position;
		max = position.position+position.size;
		GUI.Box(position, content, __style);
		this.ctx ??= ctx;
		// position.y = GUIContext.LodLevel(position.y);
		position.y += GUIContext.LodLevel(Constaints.NODE_PIN_HEIGHT);
		int counter = 0;
		foreach(Widget widget in widgets) {
			position.height = GUIContext.LodLevel(widget.LineHeight);
			widget.OnGUI(position);
			if (outWidgets.Count > counter) {
				outWidgets[counter++].OnGUI(position);
			}
			position.y += GUIContext.LodLevel(widget.LineHeight);
			
		}
	}
	

	public void GenerateSize() {
		float _size_x = 280;
		float _size_y = Constaints.NODE_PIN_HEIGHT;
		foreach(Widget widget in widgets) {
			_size_y += widget.LineHeight;
		}
		size.x = _size_x;
		size.y = _size_y;
		// min = pos;
		// max = pos+size;
		Resize();
	}

	internal bool IsInBox(Vector2 point, Rect rect) {
		return (
			rect.x < point.x 
			&& rect.y < point.y 
			&& rect.x+rect.width > point.x
			&& rect.y+rect.height > point.y
		);
	}

}

}