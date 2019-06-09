using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Menus;

namespace Spawn_Monsters
{
	class ColoredButtonComponent : ClickableComponent
	{

		public int xPos, yPos, width, height;
		public Color color;
		public int index;

		public ColoredButtonComponent(int xPos, int yPos, int width, int height, Color color, int index)
			: base(new Rectangle(xPos, yPos, width, height), "name") {
			this.xPos = xPos;
			this.yPos = yPos;
			this.width = width;
			this.height = height;
			this.color = color;
			this.index = index;
		}
		public void draw(SpriteBatch b) {
			b.Draw(StardewValley.Game1.staminaRect, new Rectangle(xPos, yPos, width, height), color);
		}
	}
}
