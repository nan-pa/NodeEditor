/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-04 06:03:40
 * - Git: https://github.com/svan9
 **************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Nanpa.Core.NodeEditor {

class Renderer {
	float _Zoom = 5.0f;
	internal List<Node> nodes = new();
	internal GridElement _M_gridElement = new();
	internal NodeEditorBase editor = null;
	internal Context ctx = new();
	Node draggingNode = null;

	Vector2 _M_offset = Vector2.zero;
	Vector2 _M_size = Vector2.zero;

	public float Zoom {
		set {
			_Zoom = value > Constaints.MAX_ZOOM_VALUE 
				? Constaints.MAX_ZOOM_VALUE 
				: value < Constaints.MIN_ZOOM_VALUE 
					? Constaints.MIN_ZOOM_VALUE 
					: value;
			// _Nodes.forEach((_node)=>_node.calc());
		}
		get {
			return _Zoom;
		}
	}

	public event Action<EventType, Vector2, KeyCode> OnKeyEvent;
	Vector3? _LastMousePosition;

	internal void KeysEvents() {
		var curEvent = Event.current;
		if (curEvent == null) {return;} 
		var mousePosition = curEvent.mousePosition;
		switch (curEvent.type) {
			case EventType.MouseDrag:
				if ( curEvent.button == 0 && draggingNode != null )
					DragNode();
				else if ( curEvent.button == 2 ) 
					DragGrid();
				break;
			case EventType.MouseDown:
				// if (curEvent.button == 1)
				break;
			case EventType.MouseUp: 
				_LastMousePosition = null;
				draggingNode = null;
				break;
			case EventType.ScrollWheel:
				OnScroll(curEvent.delta.y);
				break;

		}
		OnKeyEvent?.Invoke(curEvent.type, mousePosition, curEvent.keyCode);
	}

	void DragGrid() {
		var curMousePosition = Event.current.mousePosition;
		if (_LastMousePosition != null) {
			var dv = GUIUtility.GUIToScreenPoint((Vector2)_LastMousePosition)
								- GUIUtility.GUIToScreenPoint(curMousePosition);
			Move(dv);
			Repaint();
		}
		_LastMousePosition = curMousePosition;
	}
	void DragNode() {
		var curMousePosition = Event.current.mousePosition;
		if (_LastMousePosition != null) {
			var dv = GUIUtility.GUIToScreenPoint((Vector2)_LastMousePosition)
								- GUIUtility.GUIToScreenPoint(curMousePosition);
			draggingNode.Move(dv);
			Repaint();
		}
		_LastMousePosition = curMousePosition;
	}

	void OnScroll(float speed) {
		Zoom += speed * Zoom * 0.1f;
		Repaint();
	}

	void Move(Vector2 dv) {
		_M_offset.x += dv.x * _Zoom;
		_M_offset.y += dv.y * _Zoom;
	}


#region Constructor	
	public Renderer() { 
		nodes.Add(new Node());
	}
#endregion

	public void OnGUI(Rect position) {
		ctx.Init(_M_offset, _M_size, position, _Zoom);
		ctx.LodLevelFNa();
		_M_gridElement.OnRender(this);
		var mouse = //ctx.GridToGUI(
			// GUIUtility.GUIToScreenPoint(
			Event.current.mousePosition
		// )
		// )
		;
		foreach(Node node in nodes) {
			if (node.size == Vector2.zero) {
				node.GenerateSize();
			}
			var p_ = ctx.GridToGUI(node.pos);
			
			Rect __bounds = new()
			{
				width = ctx.Level(node.size.x),
				height = ctx.Level(node.size.y),
				x = p_.x,
				y = p_.y
			};
		
			node.OnGUI(__bounds, ctx);
			if (
				draggingNode == null 
				&& node.IsInBox(mouse, __bounds)
			) {
				draggingNode = node;
			}
		}
	}

	internal void Repaint() {
		editor.Repaint();
	}
}
}