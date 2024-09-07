/**************************************************
 * - Author: so2u
 * - Create Time: 2024-08-27 05:13:19
 * - Git: https://github.com/svan9
 **************************************************/

using UnityEngine;
using UnityEditor;

namespace Nanpa.Core.NodeEditor {

public class NodeEditorBase: EditorWindow {
	public Rect bounds;
	private Renderer _M_renderer;
	
	[MenuItem("Nanpa/NodeEditor")]
	[System.Obsolete]
	public static void ShowEditorWindow() {
		var window = GetWindow<NodeEditorBase>();
		window.title = "Node Editor";
		window._M_renderer = new()
		{
			editor = window
		};
	}

	public void OnGUI() {
		_M_renderer ??= new()
			{
				editor = this
			};
		EditorGUI.DrawRect(position, Pallete.BackgroundColor);
		_M_renderer.OnGUI(position);
		_M_renderer.KeysEvents();
	}

}

}