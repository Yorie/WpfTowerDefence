using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfTowerDefence
{
    /// <summary>
    /// Логика взаимодействия для AddTower.xaml
    /// </summary>
    public partial class AddTower : Window
    {
        Cell cell;
        Canvas canvasMap;
        TowerManager towerManager;
        Player player;
        Game game;

        internal AddTower(Cell cell, TowerManager towerManager, Canvas canvasMap, Player player, Game game)
        {
            this.cell = cell;
            this.canvasMap = canvasMap;
            this.towerManager = towerManager;
            this.player = player;
            this.game = game;
            InitializeComponent();
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            AddNewTower();
            if (errorTextBox.Text.Equals(""))
            {
                this.Close();
            }
        }

        private void AddNewTower()
        {
            Tower towerToAdd = null;
            if (towerType_listBox.SelectedItem == null)
            {
                return;
            }

            var selectedTowerType = towerType_listBox.SelectedItem.ToString();
            if (!string.IsNullOrEmpty(selectedTowerType) && towerManager != null && cell.isGround)
            {
                switch (selectedTowerType)
                {
                    case "ArcherTower":
                        {
                            /*
                              TODO
                              Would be better:
                              
                              towerToAdd = new ArcherTower(cell, canvasMap, player);
                              
                              and below outside of switch:
                              if (towerToAdd.Price < player.Money) 
                              {
                                // Not enough money warning.
                              }
                              
                              By that way we move out price check logic at exactly one place and avoid making complex conditions in each case lable of the switch.
                             */
                            towerToAdd = (player.Money >= ArcherTower.price) ? new ArcherTower(cell, canvasMap, player) : null;
                            break;
                        }
                    case "CatapultTower":
                        {
                            towerToAdd = (player.Money >= CatapultTower.price) ? new CatapultTower(cell, canvasMap, player) : null;
                            break;
                        }
                    case "TrebuchetTower":
                        {
                            towerToAdd = (player.Money >= TrebuchetTower.price) ? new TrebuchetTower(cell, canvasMap, player) : null;
                            break;
                        }
                }

                if (towerToAdd == null)
                {
                    errorTextBox.Text = "Недостаточно денег для покупки этой башни.";
                }
                else
                {
                    errorTextBox.Text = "";
                    player.Money -= towerToAdd.Price;
                    towerToAdd.onCount += KillCost_onCount;
                    Application.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        towerManager.AddTower(towerToAdd);
                    }));
                    cell.state = 2;
                }
            }
        }

        private void KillCost_onCount(int killCost)
        {
            player.Money += killCost;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            game.isSingleAddTower = false;
        }
    }
}
