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
    /// Interaction logic for medicalRecordObject.xaml
    /// </summary>
    public partial class medicalRecordObject : UserControl
    {
        public medicalRecordObject()
        {
            InitializeComponent();
        }

        public string RecordType
        {
            get { return (string)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }

        public static DependencyProperty TypeProperty = DependencyProperty.Register("RecordType", typeof(string), typeof(medicalRecordObject));


        public string RecordDate
        {
            get { return (string)GetValue(RecordDateProperty); }
            set { SetValue(RecordDateProperty, value); }
        }

        public static readonly DependencyProperty RecordDateProperty = DependencyProperty.Register("RecordDate", typeof(string), typeof(medicalRecordObject));

    }
}
