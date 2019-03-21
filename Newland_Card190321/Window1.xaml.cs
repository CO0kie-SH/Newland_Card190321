using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Newland_Card190321
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource t_UserInfoViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("t_UserInfoViewSource")));
            // 通过设置 CollectionViewSource.Source 属性加载数据: 
            t_UserInfoViewSource.Source = new Model1().t_UserInfo.ToList();
        }
    }
}
