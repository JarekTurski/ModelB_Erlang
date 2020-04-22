using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using LiveCharts;

namespace ModelB_Erlang
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Values1 = new ChartValues<ObservablePoint>();
            Values2 = new ChartValues<ObservablePoint>();
            SepX = new LiveCharts.Wpf.Separator();
            SepY = new LiveCharts.Wpf.Separator();
            OsY = new Axis();
            OsX = new Axis();
            Btn2.Visibility = Visibility.Collapsed;
        }
        int N, N2; //liczba serwerow
        double A, A2, P;//srednia wartosc ruchu, prawdopodobienstwo blokady
        

        public ChartValues<ObservablePoint> Values1 { get; set; }
        public ChartValues<ObservablePoint> Values2 { get; set; }
        public LiveCharts.Wpf.Separator SepX { get; set; }
        public LiveCharts.Wpf.Separator SepY { get; set; }
        public Axis OsY { get; set; }
        public Axis OsX { get; set; }

        double CalculateP(double A, int N)
        {
            return Math.Round(Math.Pow(A, N) / (CalculateFactorial(N) *  CalculateSum(N, A)),4);
        }

        int CalculateN(double A, double P)  //////////////////////TO DO
        {
            int result = 1;
            while (P < CalculateP(A, result))
                result++;

            if (P > CalculateP(A, result))
                result--;

            else if (result > 1)
                result--;

            return result;
        }

        double CalculateA(double P, int N)
        {
            double result = 0.0001d;
            while (P > CalculateP(result, N))
                result++;

            while (P < CalculateP(result, N))
                result -= 0.0001d;


            return Math.Round(result, 4);
        }

        public List<double> CalculateAseries(double A1, double A2, double P)
        {
            List<double> result = new List<double>();
            double gap=0;
            result.Add(Math.Round(A1,4));

            if (P < 0.005)
            {
                gap = 0.01;
                while ((result.Last() + gap) <= A2)
                {
                    if (result.Last() > 0.08)
                        gap = 0.2;
                    else if (result.Last() > 0.7)
                        gap = 0.4;
                    else if (result.Last() > 1.8)
                        gap = 0.5;

                    result.Add(Math.Round(result.Last() + gap, 4));
                }
            }
            else if (P < 0.03)
            {
                gap = 0.1;
                while ((result.Last() + gap) <= A2)
                {
                    if (result.Last() > 0.3)
                        gap = 0.3;
                    else if (result.Last() > 1.6)
                        gap = 0.5;

                    result.Add(Math.Round(result.Last() + gap, 4));
                }
            }
            else if (P < 0.1)
            {
                gap = 0.2;
                while ((result.Last() + gap) <= A2)
                {
                    if (result.Last() > 1)
                        gap = 0.5;
                    else if (result.Last() > 40)
                        gap = 1;

                    result.Add(Math.Round(result.Last() + gap, 4));
                }
            }
            else if (P < 0.3)
            {
                gap = 0.3;
                while ((result.Last() + gap) <= A2)
                {
                    if (result.Last() > 2)
                        gap = 0.5;
                    else if (result.Last() > 5)
                        gap = 1;

                    result.Add(Math.Round(result.Last() + gap, 4));
                }
            }
            else if (P >= 0.3)
            {
                gap = 1;
                while (result.Last() + gap <= A2) result.Add(Math.Round(result.Last() + gap, 4));
            }
            if (result.Last() != A2)
                result.Add(A2);
            return result;
        }

        public List<double> CalculateNseriesY(List<double> A, double P)
        {
            int range = A.Count();
            List<double> result = new List<double>();
            for (int i = 0; i < range; i++)
                result.Add(CalculateN(A[i],P));

            return result;
        }

        public List<double> CalculateNseries(int N1, int N2)
        {
            int range = N2 - N1 + 1;
            List<double> result = new List<double>();
            for (int i= 0; i< range; i++)
                result.Add(N1 + i);

            return result;
        }

        public List<double> CalculatePseries(double A, int N1, int N2)
        {
            int range = N2 - N1 + 1;
            List<double> result = new List<double>();
            result.Add(CalculateP(A, N1));
            for (int i = 1; i < range; i++)
                result.Add(Math.Round((A * result[i - 1]) / (N1 + i + A * result[i - 1]),4));

            return result;
        }

        public void DrawChart(int series_index, List<double> xvalues, List<double> yvalues)
        {
            if (series_index == 1)
            {
                for (int i = 0; i < xvalues.Count(); i++)
                    Values1.Add(new ObservablePoint { X = xvalues[i], Y = yvalues[i] });              
            }
            if (series_index == 2)
            {
                for (int i = 0; i < xvalues.Count(); i++)
                    Values2.Add(new ObservablePoint { X = xvalues[i], Y = yvalues[i] });             
            }
        }
            
        public double CalculateFactorial(int number) //silnia
        {
            double result = 1;
            while (number != 1 && number != 0)
            {
                result = result * number;
                number = number - 1;
            }
            return result;
        }

        public double CalculateSum(int N, double A)
        {
            double result=0d;
            for(int i=0;i<=N;i++)
            {
                result = result + (Math.Pow(A, i) / CalculateFactorial(i));
            }
            return result;
        }

        public double CalculateStepXChart2 (double A2)
        {
            if (A2 <= 15)
                return 0.5;
            if (A2 <= 25)
                return 1;
            if (A2 <= 50)
                return 2;
            if (A2 <= 100)
                return 5;
            else
                return 10;
        }

        public int CalculateStepYChart2 (double A2, double P)
        {
            int N = CalculateN(A2, P);
            if (N <= 17)
                return 1;
            if (N <= 36)
                return 2;
            else
                return 5;
        }

        private void tb1_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void tb2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox1_Checked(object sender, RoutedEventArgs e)
        {
            Cbx2.IsEnabled = false;
            Tb5.IsEnabled=true;
            tb3.IsEnabled = false;
            Btn1.IsEnabled = false;
            Btn2.IsEnabled = true;
            Btn2.Visibility = Visibility.Visible;
            Btn1.Visibility = Visibility.Collapsed;
        }

        private void CheckBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            Cbx2.IsEnabled = true;
            Tb5.IsEnabled = false;
            tb3.IsEnabled = true;
            Btn1.IsEnabled = true;
            Btn2.IsEnabled = false;
            Btn1.Visibility = Visibility.Visible;
            Btn2.Visibility = Visibility.Collapsed;
        }

        private void CartesianChart_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox2_Checked(object sender, RoutedEventArgs e)
        {
            cbx1.IsEnabled = false;
            tb6.IsEnabled = true;
            tb1.IsEnabled = false;
            Btn1.IsEnabled = false;
            Btn2.IsEnabled = true;
            Btn2.Visibility = Visibility.Visible;
            Btn1.Visibility = Visibility.Collapsed;
        }

        private void Btn3_Click(object sender, RoutedEventArgs e) //HELP
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void CheckBox2_Unchecked(object sender, RoutedEventArgs e)
        {
            cbx1.IsEnabled = true;
            tb6.IsEnabled = false;
            Btn1.IsEnabled = true;
            tb1.IsEnabled = true;
            Btn2.IsEnabled = false;
            Btn1.Visibility = Visibility.Visible;
            Btn2.Visibility = Visibility.Collapsed;
        }

        private void button2_Click(object sender, RoutedEventArgs e) //wykres
        {
            Values1.Clear();
            Values2.Clear();
            Chart1.Update();
            
            N = Convert.ToInt32(tb1.Text);
            A = Convert.ToDouble(tb2.Text);
            P = Convert.ToDouble(tb3.Text);
            if ((N == 0 && A == 0) || (N == 0 && P == 0) || (A == 0 && P == 0) || (A == 0 && P == 0 && N == 0))
                MessageBox.Show("Wprowadzone dane nie są odpowiedniego formatu.", "Zły format danych wejściowych");

            else if (N != 0 && A != 0 && P != 0)
                MessageBox.Show("Jedno pole powinno zostać puste.", "Zły format danych wejściowych");

            else if (P == 0) //wykres P(A=const,N)
            {
                
                N2 = Convert.ToInt32(Tb5.Text);
                if (N >= N2)
                    MessageBox.Show("Podany przedział liczbowy nie istnieje.", "Zły format danych wejściowych");
                OsY.Title = "prawdopodobieństwo blokady";
                OsX.Title = "liczba serwerów";
                SepY.Step = 0.1;
                SepX.Step = (int)(N2 / 22) + 1;
                DrawChart(1,CalculateNseries(N, N2), CalculatePseries(A, N, N2));
                DataContext = this;
            }

            else if(N==0) // wykres N(P=const, A)
            {
                A2 = Convert.ToDouble(tb6.Text);
                if (A >= A2)
                    MessageBox.Show("Podany przedział liczbowy nie istnieje.", "Zły format danych wejściowych");
                OsY.Title = "liczba serwerów";
                OsX.Title = "średnia wartość ruchu";
                SepY.Step = CalculateStepYChart2(A2,P);
                SepX.Step = CalculateStepXChart2(A2);
                DrawChart(2,CalculateAseries(A,A2,P), CalculateNseriesY(CalculateAseries(A,A2,P),P));
                DataContext = this;

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e) //wyliczanie wartosci
        {
            N = Convert.ToInt32(tb1.Text);
            A = Convert.ToDouble(tb2.Text);
            P = Convert.ToDouble(tb3.Text);
            if ((N == 0 && A == 0) || (N == 0 && P == 0) || (A == 0 && P == 0) || (A == 0 && P == 0 && N == 0))
                MessageBox.Show("Wprowadzone dane nie są odpowiedniego formatu", "Zły format danych wejściowych");

            else if (N != 0 && A != 0 && P != 0)
                MessageBox.Show("Jedno pole powinno zostać puste", "Zły format danych wejściowych");

            else if (P < 0 || P > 1)
                MessageBox.Show("Wartość prawdopodobieństwa musi być w przedziale (0;1> ", "Zły format danych wejściowych");

            else if (P == 0)
                tb3.Text = CalculateP(A, N).ToString();

            else if (N == 0)
                tb1.Text = CalculateN(A, P).ToString();

            else if (A == 0)
                tb2.Text = CalculateA(P, N).ToString();

        }
    }
}
