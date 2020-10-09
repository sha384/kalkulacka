using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using org.mariuszgromada.math.mxparser;

namespace Kalkulacka
{
    public partial class MainPage : ContentPage
    {
        private string label_text = "";
        private string label_value = "";

        public MainPage()
        {
            InitializeComponent();
        }

        // obnoví label podle toho, co se do něj napíše
        private void render_label()
        {
            label.Text = label_text;
        }

        // zavolá se po stisknutí čísla nebo znaku
        private void button_click(object sender, EventArgs e)
        {
            string value = (sender as Button).Text;

            // přidání čísla/znaku do labelu
            label_text += value;

            // přidání čísla/znaku do hodnoty labelu (speciální znaky se zamění za * nebo / aby se to dalo vypočítat)
            switch (value)
            {
                case "÷":
                    label_value += "/";
                    break;
                case "×":
                    label_value += "*";
                    break;
                case ",":
                    label_value += ".";
                    break;
                default:
                    label_value += value;
                    break;
            }

            render_label();
        }

        // rovná se
        private void evaluate(object sender, EventArgs e)
        {
            Expression expression = new Expression(label_value);
            label_text = expression.calculate().ToString();
            label_value = label_text;

            render_label();
        }

        // vyčištení labelu
        private void clear(object sender, EventArgs e)
        {
            label_text = "";
            label_value = "";

            render_label();
        }

        // smazání posledního znaku v labelu
        private void delete(object sender, EventArgs e)
        {
            if (label_text.Length != 0)
            {
                label_text = label_text.Remove(label_text.Length - 1);
                label_value = label_value.Remove(label_value.Length - 1);
            }

            render_label();
        }
    }
}
