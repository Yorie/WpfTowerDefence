using System.Windows.Controls;
using System.Windows.Media;

namespace WpfTowerDefence
{
    class ArcherTower : Tower
    {
        static SolidColorBrush colorNorm = Brushes.SkyBlue;
        public static int price = 10; // TODO: Not a constant value, could be changed from outside, but will not take effect on created entities. Make it const or private, or better pass as entity configuration.
        public ArcherTower(Cell location, Canvas canvasMap, Player player) : base(location, canvasMap, colorNorm, price, player)
        {
            this.range = 3;
            this.power = 1;
        }
    }
}
