using System.Collections;
/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-05 08:29:29
 * - Git: https://github.com/svan9
 **************************************************/

using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

namespace Nanpa.Core.NodeEditor {
	
public class GridElement : VisualElement {
	Context ctx;
	
	internal void OnRender(Renderer renderer) {
		ctx = renderer.ctx;
		Repaint();
	}

	void Repaint() {
		ctx.Fill(Pallete.BackgroundColor);
		DrawLines();
	}

	void DrawLines() {
		int lodLevel = ctx.LodLevel();
		DrawLODLines(lodLevel);
	}

	void DrawLODLines(int level) {
		const float DEFAULT_CELL_SIZE = Constaints.DEFAULT_CELL_SIZE;
		int step0 = (int)MathF.Pow(10, level);
		int halfCount = (int)(step0 * Constaints.CELLS_IN_LINE_COUNT / 2 * 10);
		var length = halfCount * DEFAULT_CELL_SIZE;
		var offsetX = ctx.offset.x / DEFAULT_CELL_SIZE / (step0 * step0) * step0;
		var offsetY = ctx.offset.y / DEFAULT_CELL_SIZE / (step0 * step0) * step0;
		ctx.color = Pallete.LinesColor2;
		for (int i = -halfCount; i <= halfCount; i += step0) {
			ctx.DrawLine(
				ctx.GridToGUI(new Vector2(-length + offsetX * DEFAULT_CELL_SIZE, (i + offsetY) * DEFAULT_CELL_SIZE)),
				ctx.GridToGUI(new Vector2(length + offsetX * DEFAULT_CELL_SIZE, (i + offsetY) * DEFAULT_CELL_SIZE))
			);
			ctx.DrawLine(
				ctx.GridToGUI(new Vector2((i + offsetX) * DEFAULT_CELL_SIZE, -length + offsetY * DEFAULT_CELL_SIZE)),
				ctx.GridToGUI(new Vector2((i + offsetX) * DEFAULT_CELL_SIZE, length + offsetY * DEFAULT_CELL_SIZE))
			);
		}
		offsetX = offsetX / (10 * step0) * 10 * step0;
		offsetY = offsetY / (10 * step0) * 10 * step0;
		ctx.color = Pallete.LinesColor;
		for (int i = -halfCount; i <= halfCount; i += step0 * 10) {
			ctx.DrawLine(
				ctx.GridToGUI(new Vector2(-length + offsetX * DEFAULT_CELL_SIZE, (i + offsetY) * DEFAULT_CELL_SIZE)),
				ctx.GridToGUI(new Vector2(length + offsetX * DEFAULT_CELL_SIZE, (i + offsetY) * DEFAULT_CELL_SIZE))
			);
			ctx.DrawLine(
				ctx.GridToGUI(new Vector2((i + offsetX) * DEFAULT_CELL_SIZE, -length + offsetY * DEFAULT_CELL_SIZE)),
				ctx.GridToGUI(new Vector2((i + offsetX) * DEFAULT_CELL_SIZE, length + offsetY * DEFAULT_CELL_SIZE))
			);
		}
	}

	
}
}