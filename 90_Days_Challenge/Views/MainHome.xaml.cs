using _90_Days_Challenge.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace _90_Days_Challenge.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    
    public sealed partial class MainHome : Page
    {

        public MainHome()
        {
            this.InitializeComponent();


    }

        private void CalculateBmi_Click(object sender, RoutedEventArgs e)
        {
            float result=0;
            try
            {
                result = (float)(float.Parse(WeightValue.Text) / (float.Parse(HeightValue.Text) * float.Parse(HeightValue.Text)))*100 * 100;
                ResultTextBlock.Text = "Your BMI : " +result.ToString("0.00");
                if(result >= 30)
                {
                    resultImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/vbad.png"));
                    resultdescriptionTextBlock.Text = "Individuals with a BMI of 30-34.99 are in a physically unhealthy condition, which puts them at risk for serious ilnesses such as heart disease, diabetes, high blood pressure, gall bladder disease, and some cancers. This holds especially true if you have a larger than recommended Waist Size. These people would benefit greatly by modifying their lifestyle. Ideally, see your doctor and consider reducing your weight by 5-10 percent. Such a weight reduction will result in considerable health improvements.";
                }
                else if (result > 25)
                {
                    resultImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/bad.png"));
                    resultdescriptionTextBlock.Text = "People falling in this BMI range are considered overweight and would benefit from finding healthy ways to lower their weight, such as diet and exercise. Individuals who fall in this range are at increased risk for a variety of ilnesses. If your BMI is 27-29.99 your risk of health problems becomes higher. In a recent study an increased rate of blood pressure, diabetes and heart disease was recorded at 27.3 for women and 27.8 for men. It may be a good idea to check your Waist Circumference and compare it with the recommended limits.";
                }
                else if (result < 25 && result > 18.5)
                {
                    resultImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/normal.png"));
                    resultdescriptionTextBlock.Text = "People whose BMI is within 18.5 to 24.9 possess the ideal amount of body weight, associated with living longest, the lowest incidence of serious ilness, as well as being perceived as more physically attractive than people with BMI in higher or lower ranges. However, it may be a good idea to check your Waist Circumference and keep it within the recommended limits.";
                }
                else
                {
                    resultImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/vbad.png"));
                    resultdescriptionTextBlock.Text = "A lean BMI can indicate that your weight maybe too low. You should consult your physician to determine if you should gain weight, as low body mass can decrease your body's immune system, which could lead to ilness such as disappearance of periods (women), bone loss, malnutrition and other conditions. The lower your BMI the greater these risks become.";
                }
                BmiHistory bm = new BmiHistory();
                bm.Bmi_Date = DateTime.Now.Year+"-"+DateTime.Now.Month+"-"+ DateTime.Now.Day;
                bm.Bmi_Result = result;
                App.db.Insert(bm);
            }
            catch
            {
                ResultTextBlock.Text = "Error Input Data";
            }
        }

        private void ProgramButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Home));
        }
    }
}
