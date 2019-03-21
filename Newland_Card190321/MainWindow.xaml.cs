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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MWRDemoDll;

namespace Newland_Card190321
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        MWRDemoDll.MifareRFEYE ic = MWRDemoDll.MifareRFEYE.Instance;
        public MainWindow()
        {
            InitializeComponent();
            sc(ic.ConnDevice(), "连接设备");
            sc(ic.Search(), "寻卡");
        }

        private void sc(ResultMessage resultMessage, string v)
        {
            
            bool succ= resultMessage.Result == Result.Success;
            if (succ)
            {
                if (v == "寻卡")
                {
                    lbkh.Content = resultMessage.Model.ToString();
                }
            }
            else
            {
                MessageBox.Show(v + "失败！");
            }
        }

        private void btnDQ_Click(object sender, RoutedEventArgs e)
        {
            sc(ic.Search(), "寻卡");
            sc(ic.AuthCardPwd(null, CardDataKind.Data2), "密码验证");
            string s1 = du(1), s2 = du(2);
            if (s1 == "" || s2 == "") return;
            textBox.Text = s1;
            comboBox.SelectedIndex = s2 == "" ? -1 : s2 == "读者" ? 0 : 1;
        }

        private string du(int v)
        {
            string s = "";
            s = ic.ReadString(CardDataKind.Data2, v, Encoding.UTF8).Trim();
            return s;
        }

        private void btnZC_Click(object sender, RoutedEventArgs e)
        {
            string kh = lbkh.Content.ToString(), name = textBox.Text.Trim(),
                ty = comboBox.SelectedIndex == -1 ? "" : comboBox.SelectedIndex == 0 ? "读者" : "员工";
            DateTime dt = DateTime.Now;
            if(kh=="" || name=="" || ty == "")
            {
                MessageBox.Show("请输入正确的信息");
                return;
            }
            btnZC.IsEnabled = false;
            label1.Visibility = Visibility.Visible;

            ic.WriteString(CardDataKind.Data2, name, 1, Encoding.UTF8);
            ic.WriteString(CardDataKind.Data2, ty, 2, Encoding.UTF8);
            label1.Content = $"卡号:{kh}\n姓名：{name}\n人员类型：{ty}\n注册时间：{dt.ToString()}\n\n\n\n.";

            try
            {
                Model1 m = new Model1();
                m.t_UserInfo.Add(new t_UserInfo { Id = kh, Name = name, Type = ty, Date = dt });
                m.SaveChanges();
            }
            catch (Exception)
            {
            }

            PrintDialog dy = new PrintDialog();
            if (dy.ShowDialog().Value)
            {
                dy.PrintVisual(label1, "打印小票");
            }

            label1.Visibility = Visibility.Hidden;
            textBox.Text = "";
            comboBox.SelectedIndex = -1;
            btnZC.IsEnabled = true;
        }

        public static bool show = false;
        private void btnCX_Click(object sender, RoutedEventArgs e)
        {
            if (!show)
            {
                Window1 w = new Window1();
                w.Show();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
