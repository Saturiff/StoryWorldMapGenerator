using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace StoryWorldMap
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // CompositionTarget.Rendering += new EventHandler(CompositionTarget_Rendering);

            // Polygon p = new Polygon();
            // 
            // Point[] ps = new Point[] { new Point(10, 110), new Point(60, 10), new Point(110, 110) };
            // p.Points = new PointCollection(ps);
            // p.Fill = new SolidColorBrush(Color.FromArgb(0xff,0x00,0x00,0xff));
            // GGG.Children.Clear();
            // GGG.Children.Add(p);
        }

        // 創建模式：
        // 滑鼠在特定區域內點擊時可以儲存每個相對位置，並且產生小點
        // 直到按下創建完成，以先前儲存的各個點實例化多邊形
        private enum Status
        {
            Normal, Create, Edit
        }

        private void NormalMode()
        {
            ToggleClickEnableStatus(Status.Normal);
        }

        private void CreateMode()
        {
            // 清空暫存座標
            Reset();
            // 啟用在範圍內點擊的紀錄
            ToggleClickEnableStatus(Status.Create);
        }

        private List<Point> coordinates = new List<Point>();

        Random r = new Random();

        private void Build()
        {
            Polygon p = new Polygon()
            {
                Points = new PointCollection(coordinates),
                Fill = new SolidColorBrush(RandomColor)
            };
            p.MouseDown += P_MouseDown;

            C_DrawArea.Children.Add(p);
            Reset();
        }

        private Color RandomColor => Color.FromArgb(0xff, (byte)r.Next(0, 0xff), (byte)r.Next(0, 0xff), (byte)r.Next(0, 0xff));

        private void P_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var p = (Polygon)sender;
            if (e.ChangedButton == MouseButton.Left)
            {
                // 顯示給個角落的點
                // 互動：拖曳
            }
            else if(e.ChangedButton == MouseButton.Right)
            {
                // 右鍵選單
                // 重新命名、刪除
            }
        }

        private void Reset()
        {
            coordinates.Clear();
        }

        private void ToggleClickEnableStatus(Status newStatus)
        {
            B_MapArea.IsEnabled = true;
            // TODO: 變成右鍵選單
            if (newStatus == Status.Normal)
            {

            }
            else if (newStatus == Status.Create)
            {

            }
            else if (newStatus == Status.Edit)
            {

            }

        }

        // 編輯模式：
        // 對目標多邊形設定名稱、顏色
        // <?> 調整區域範圍(新增節點、修改節點、刪除節點)
        // 判斷滑鼠是否在多邊形內、若重疊則找最上層的polygon
        private void EditMode()
        {
            ToggleClickEnableStatus(Status.Edit);
        }

        private void Delete()
        {

        }

        // 每次結束動作(創建後、編輯後)，進行世界儲存至Settings或txt file
        // 
        private void Save()
        {

        }

        private void AddPointFromClick(object targetObject)
        {
            Point p = Mouse.GetPosition((IInputElement)targetObject);
            coordinates.Add(p);
            CreateClickPoint(p);
        }

        private void CreateClickPoint(Point p)
        {
            Button b = new Button();
            C_DrawArea.Children.Add(b);
            Canvas.SetLeft(b, p.X);
            Canvas.SetTop(b, p.Y);
            b.Height = b.Width = 10;
            b.Background = new SolidColorBrush(RandomColor);
        }

        private void B_Normal_Click(object sender, RoutedEventArgs e) => NormalMode();

        private void B_Create_Click(object sender, RoutedEventArgs e) => CreateMode();

        private void B_Build_Click(object sender, RoutedEventArgs e) => Build();

        private void B_Clear_Click(object sender, RoutedEventArgs e) => Reset();

        private void B_Edit_Click(object sender, RoutedEventArgs e) => EditMode();

        private void B_Delete_Click(object sender, RoutedEventArgs e) => Delete();

        private void B_Save_Click(object sender, RoutedEventArgs e) => Save();

        private void B_MapArea_Click(object sender, RoutedEventArgs e) => AddPointFromClick(sender);
    }
}
