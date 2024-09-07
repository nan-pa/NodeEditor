/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-04 06:19:26
 * - Git: https://github.com/svan9
 **************************************************/

using System;
using UnityEngine;

namespace Nanpa.Core.NodeEditor {

class Widget<T> {
	public bool hasEntry = true;
	public static string name = "widget";
	GUIStyle _style;
	// public bool hasExit  = true;

	public float LineHeight {
		get {
			return Constaints.NODE_PIN_HEIGHT;
		}
	}
	
	private T _M_data;

	public Widget() {
		_style = new(GUI.skin.box);
	}

	public Widget(bool hasEntry) {
		this.hasEntry = hasEntry;
		// this.hasExit = hasExit;
	}

	public void OnGUI(Rect position) {
		var _pos = new Rect(position);
		_pos.x -= GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS);
		_pos.height = GUIContext.LodLevel(Constaints.NODE_PIN_HEIGHT);
		_pos.width = Constaints.NODE_PIN_RADIUS*2;
		
		if (hasEntry) {
			GUIContext.DrawCircle(_pos, (int)GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS), Pallete.NodeDotPoint);
		}
		_pos.x = position.x + GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS);
		_pos.width = position.width - _pos.width;
		GUIContext.DrawText(_pos, name);
	}

	public void Put(T data) {
		_M_data = data;
	}

	public T Get() {
		return _M_data;
	}
}

class OutWidget<T> {
	public bool hasEntry = true;
	public static string name = "out";
	GUIStyle _style;
	// public bool hasExit  = true;

	public float LineHeight {
		get {
			return Constaints.NODE_PIN_HEIGHT;
		}
	}
	
	private T _M_data;

	public OutWidget() {
		_style = new(GUI.skin.box);
	}

	public OutWidget(bool hasEntry) {
		this.hasEntry = hasEntry;
		// this.hasExit = hasExit;
	}

	public void OnGUI(Rect position) {
		var _pos = new Rect(position);
		_pos.x += _pos.width - GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS);
		_pos.height = GUIContext.LodLevel(Constaints.NODE_PIN_HEIGHT);
		_pos.width = Constaints.NODE_PIN_RADIUS*2;
		
		if (hasEntry) {
			GUIContext.DrawCircle(_pos, (int)GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS), Pallete.NodeDotPoint);
		}
		_pos.x = position.x + GUIContext.LodLevel(Constaints.NODE_PIN_RADIUS);
		_pos.width = position.width - _pos.width;
		GUIContext.DrawText(_pos, name, TextAnchor.MiddleRight);
	}

	public void Put(T data) {
		_M_data = data;
	}

	public T Get() {
		return _M_data;
	}
}

class Widget: Widget<object> {}
class OutWidget: OutWidget<object> {}

}