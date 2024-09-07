/**************************************************
 * - Author: so2u
 * - Create Time: 2024-09-04 06:07:07
 * - Git: https://github.com/svan9
 **************************************************/

using UnityEngine;

namespace Nanpa.Core.NodeEditor {

class Pallete {
	public static Color BackgroundColor   = FromIntAlpha(0x1f1f1fff);
	public static Color LinesColor        = FromIntAlpha(0x171717ff);
	public static Color LinesColor2       = FromIntAlpha(0x1717177d);
	public static Color NodeBackground    = FromIntAlpha(0x333333ff);
	public static Color NodeText          = FromIntAlpha(0x848484ff);
	public static Color NodeDotPoint      = FromIntAlpha(0x77ff77ff);

	public static Color FromInt(uint x) {
		float r = ((x>>16) & 0xff)/255.0f;
		float g = ((x>>8)  & 0xff)/255.0f;
		float b = ((x>>0)  & 0xff)/255.0f;
		return new Color(r,g,b);
	}

	public static Color FromIntAlpha(uint x) {
		float r = ((x>>24) & 0xff)/255.0f;
		float g = ((x>>16) & 0xff)/255.0f;
		float b = ((x>>8)  & 0xff)/255.0f;
		float a = ((x>>0)  & 0xff)/255.0f;
		return new Color(r,g,b,a);
	}
}

}