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
using System.Reflection;
using System.IO;

namespace TEST02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            void Func(string path)
            {
                if (ListBox1.Items.IsEmpty == false)
                {
                    ListBox1.Items.Clear();

                }
                System.IO.Path.GetFullPath(path);
                if (Directory.Exists(path))
                {


                    string[] fullpathname = Directory.GetFiles(path, "?*.dll");



                    foreach (string dll in fullpathname)
                    {
                        try
                        {
                            Assembly dllfile = (Assembly.LoadFrom(System.IO.Path.GetFullPath(dll)));


                            /* Хотел исключить методы родительского класса Object. Стоит ли?
                            Type ObjectT = (typeof(object));
                            MethodInfo[] ObjectMethods = ObjectT.GetMethods();
                            */

                            foreach (Type Parent in dllfile.GetTypes())
                            {


                                ListBox1.Items.Add(Parent.Name);



                                foreach (MethodInfo method in Parent.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
                                {
                                    if ((method.IsPrivate == false && method.IsFamily == true) || method.IsPublic == true) {
                                        ListBox1.Items.Add("- " + method.Name);
                                    }
                                }



                            }
                        }
                        catch
                        { //MessageBox.Show($"Ошибка! Данная DLL {dll} не является сборкой .NET"); закомментил на случай если в директории будет много сборок
                        }
                    }
                }




                else
                {
                    MessageBox.Show("Ошибка! Данной директории не существует!");

                }
            }

            Func(TextBox1.Text.Replace('"',' ').Trim());


        }

    }
}
