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

namespace clinical.userControls
{
    /// <summary>
    /// Interaction logic for previousVisitObject.xaml
    /// </summary>
    public partial class previousVisitObject : UserControl
    {
        public previousVisitObject()
        {
            InitializeComponent();
        }
        public string VisitReason
        {
            get { return (string)GetValue(VisitReasonProperty); }
            set { SetValue(VisitReasonProperty, value); }
        }

        public static DependencyProperty VisitReasonProperty = DependencyProperty.Register("VisitReason", typeof(string), typeof(previousVisitObject));


        public string VisitDate
        {
            get { return (string)GetValue(VisitDateProperty); }
            set { SetValue(VisitDateProperty, value); }
        }

        public static readonly DependencyProperty VisitDateProperty = DependencyProperty.Register("VisitDate", typeof(string), typeof(previousVisitObject));

    }
}
