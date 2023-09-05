using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHole.Model
{
    public class DiseñoElementos
    {
        // -------------------------------------------------------------------------------------------------------------------------------
        // Diseño a flexión
        public double Cuantia(double fc, double fy, double Mu, double b, double d)
        {
            double k = (Mu / (b * Math.Pow(d, 2)));
            double m = (fy / (0.85 * fc));
            double preq = 1 / m * (1 - Math.Sqrt(1 - (2 * m * k) / (0.90 * fy / 10)));
            return Math.Round(preq, 4);
        }

        public double AceroRequerido(double fc, double fy, double Mu, double b, double d)
        {
            double k = (Mu / (b * Math.Pow(d, 2)));
            double m = (fy / (0.85 * fc));
            double preq = 1 / m * (1 - Math.Sqrt(1 - (2 * m * k) / (0.90 * fy / 10)));
            double Asreq = preq * b * d;
            return Math.Round(Asreq, 2);
        }

        public double AceroMinimo(double pmin, double b, double h)
        {
            double Asmin = pmin * b * h;
            return Math.Round(Asmin, 2);
        }

        public double Acero(double Asreq, double Asmin)
        {
            double As = Math.Max(Asreq, Asmin);
            return Math.Round(As, 2);
        }

        public double CantBarras(double As, double Asb)
        {
            double Cant = Math.Ceiling(As / Asb);
            return Cant;
        }

        public double SepBarras(double b, double As, double Asb)
        {

            double Sep = Math.Round(b / (As / Asb), 2);

            if (Sep <= 30)
            {
                Sep = Math.Round(b / (As / Asb), 2);
                return Sep;
            }
            else
            {
                Sep = 30;
                return Sep;
            }
        }

        public double AceroColocado(double Cant, double Asb)
        {
            double Ascol = Cant * Asb;
            return Math.Round(Ascol, 2);
        }

        public double MomNominal(double fc, double fy, double Asc, double b, double d)
        {
            double Mn = 0.9 * Asc * fy / 10 * (d - (Asc * fy / 10) / (1.7 * fc / 10 * b));
            return Math.Round(Mn, 2);
        }


        // -------------------------------------------------------------------------------------------------------------------------------
        // Chequeo a cortantes losas, muros y vigas

        public double CortConcreto(double fiv, double fc, double b, double d)
        {
            double Vc = (0.17 * fiv * Math.Sqrt(fc) * (b * 10) * (d * 10)) / 1000;
            return Math.Round(Vc, 2);
        }


        public double CortAcero(double fiv, double Av, double fy, double d, double S)
        {
            double Vs = (fiv * 2 * Av * (fy / 10) * d) / S;
            return Math.Round(Vs, 2);
        }

        public double CortNominal(double Vc, double Vs)
        {
            double Vn = Vc + Vs;
            return Math.Round(Vn, 2);
        }


        // -------------------------------------------------------------------------------------------------------------------------------
    }
}
