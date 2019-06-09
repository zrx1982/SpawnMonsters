using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Menus;
using System.Collections.Generic;

namespace Spawn_Monsters
{
	class MultiClickableMonsterComponent : ClickableComponent
	{
		private List<ClickableMonsterComponent> monsters;
		private List<ColoredButtonComponent> buttons;
		private int current;

		public MultiClickableMonsterComponent(string[] names, Color[] colors, int xPos, int yPos, int width, int height, object[] arguments, int spriteWidth = 16, int spriteHeight = 24) 
			: base(new Microsoft.Xna.Framework.Rectangle(xPos, yPos, width, height), names[0]) {
			monsters = new List<ClickableMonsterComponent>();
			buttons = new List<ColoredButtonComponent>();
			for (int i = 0; i < names.Length; i++) {
				monsters.Add(new ClickableMonsterComponent(names[i], xPos, yPos, width, height, spriteWidth, spriteHeight) {
					args = (object[])arguments[i]
				});
				int offset = (width - names.Length * 40) / 2 + 20;
				buttons.Add(new ColoredButtonComponent(xPos + i * 40 + offset, yPos + height - 70, 30, 30, colors[i], i));
			}

			current = 0;

		}

		public void performHoverAction(int x, int y) {

			AnimatedSprite sprite = monsters[current].sprite;

			if (containsPoint(x, y)) {
				if (sprite.CurrentAnimation == null) {
					sprite.Animate(Game1.currentGameTime, monsters[current].StartFrame, monsters[current].NumberOfFrames, monsters[current].Interval);
				}
			} else {
				sprite.StopAnimation();
			}
		}

		public void receiveLeftClick(int x, int y) {
			foreach(ColoredButtonComponent c in buttons) {
				if(c.containsPoint(x, y)) {
					current = c.index;
					Game1.playSound("smallSelect");
					return;
				}
			}
			if (containsPoint(x, y)) {
				Game1.activeClickableMenu = new MonsterPlaceMenu(monsters[current].name.Replace("Armored ", "")
				.Replace("Iridium Crab", "Rock Crab")
				.Replace("Iridium Bat", "Bat")
				.Replace("Lava Bat", "Bat")
				.Replace("Frost Bat", "Bat")
				.Replace("Carbon ","")
				.Replace("Wilderness", "Stone"), monsters[current].args);
				
			}
		}


		public void Draw(SpriteBatch b) {
			if (monsters[current].name == "Green Slime" || monsters[current].name == "Fly" || monsters[current].name == "Grub") monsters[current].Draw(b, buttons[current].color);
			else monsters[current].Draw(b);
			Rectangle r = buttons[current].bounds;
			r.Width += 10;
			r.Height += 10;
			r.X -= 5;
			r.Y -= 5;
			b.Draw(Game1.staminaRect, r, Color.IndianRed);
			foreach(ColoredButtonComponent button in buttons) {
				button.draw(b);
			}
		}
	}
}
