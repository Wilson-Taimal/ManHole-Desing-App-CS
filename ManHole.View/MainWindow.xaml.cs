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
using ManHole.Core;
using ManHole.Model;

namespace ManHole.View
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Prueba();
        }

        public void Prueba()
        {
            Desarrollo desarrollo = new Desarrollo();

            // -------------------------------------------------------------------------------------------------------------------------------
            // GEOMETRÍA //
            GeomEstructura GeomEst = new GeomEstructura();
            GeomEst.D1 = 1.20;
            GeomEst.D3 = 0.60;
            GeomEst.E1 = 0.20;
            GeomEst.E2 = 0.20;
            GeomEst.E3 = 0.20;
            GeomEst.E4 = 0.20;
            GeomEst.E5 = 0.08;
            GeomEst.Ec = 0.10;
            GeomEst.x = 0.10;
            GeomEst.h = 0.70;
            GeomEst.hu = 1.90;

            GeomEst.D2 = GeomEst.D1 + (2*GeomEst.E2) + (2*GeomEst.x);
            GeomEst.E6 = (GeomEst.E2 - GeomEst.E5)/2;
            GeomEst.HT = GeomEst.E1 + GeomEst.E3 + GeomEst.h + GeomEst.hu;


            // -------------------------------------------------------------------------------------------------------------------------------
            // MATERIALES //
            Materiales materiales = new Materiales();
            materiales.fc = 28;
            materiales.fy = 420;
            materiales.fiv = 0.75;
            materiales.rc = 24;
            materiales.Ecuacion = 0;
            materiales.Ec = materiales.ElasticidadConcreto(materiales.Ecuacion, materiales.fc);


            // -------------------------------------------------------------------------------------------------------------------------------
            // SUELO //
            Suelo suelo = new Suelo();
            suelo.Qadm = 70;
            suelo.rs = 18;
            suelo.fis = 25;
            suelo.rsat = 19;
            suelo.rw = 10;
            suelo.H1 = 0.90;


            // -------------------------------------------------------------------------------------------------------------------------------
            // CARGAS //
            Cargas cargas = new Cargas();
            cargas.DC = 60.02;
            cargas.DT = 1.0;
            cargas.LLc = 160;
            cargas.LLt = 125;
            cargas.SentidoMuro = 0;
            List<ResultCargas> resulCargas = desarrollo.CalculoEmpujes(GeomEst, suelo, cargas);


            // -------------------------------------------------------------------------------------------------------------------------------
            // ESTABILIDAD //
            Estabilidad estabilidad = new Estabilidad();
            estabilidad.Qs = 63;
            estabilidad.Si = 2.24;
            estabilidad.Sadm = 2.54;
            List<ResultEstabilidad> resultEstabilidad = desarrollo.CalculoEstabiliada(estabilidad, suelo);


            // -------------------------------------------------------------------------------------------------------------------------------
            // DISEÑO LOSA //
            DiseñoElementos diseñoElementos = new DiseñoElementos();
            Refuerzo refuerzo = new Refuerzo();
            Losas losas = new Losas();
            losas.b = 100;
            losas.r = 7.5;
            losas.pmin = 0.0018;
            losas.Nb = 3;
            losas.h = GeomEst.E1 * 100;
            losas.d = losas.h - losas.r;

            // Solicitaciones sobre la losa
            double[] Losa_Mun = { 950, 1000 };
            double[] Losa_Mup = { 1520, 300 };
            double[] Losa_Vu = { 68, 35 };
            losas.LMun = Losa_Mun;
            losas.LMup = Losa_Mup;
            losas.LVu = Losa_Vu;
            List<ResultLosas> resultLosas = desarrollo.CalculoLosas(materiales, losas, diseñoElementos, refuerzo);


            // -------------------------------------------------------------------------------------------------------------------------------
            // DISEÑO CILINDRO //           
            Muros muros = new Muros();
            muros.b = 100;
            muros.r = 7.5;
            muros.pmin = 0.0021;
            muros.Nb = 3;
            muros.h = GeomEst.E2 * 100;
            muros.d = muros.h - muros.r;

            // Solicitaciones sobre el muro
            double[] Muro_Muv = { 250, 700 };
            double[] Muro_Muh = { 60, 200 };
            muros.Vu = 35;
            muros.LMuy = Muro_Muv;
            muros.LMux = Muro_Muh;
            
            List<ResultMuros> resultMuros = desarrollo.CaculoMuros(materiales, muros, diseñoElementos, refuerzo);


            // -------------------------------------------------------------------------------------------------------------------------------
            // DISEÑO CONO //           
            Cono cono = new Cono();
            cono.b = 100;
            cono.r = 5;
            cono.pmin = 0.0021;
            cono.Nb = 3;
            cono.h = GeomEst.Ec * 100;
            cono.d = cono.h - cono.r;

            // Solicitaciones sobre el muro
            double[] Cono_Muv = { 100, 150 };
            double[] Cono_Muh = { 27, 27 };
            cono.Vu = 10;
            cono.LMuy = Cono_Muv;
            cono.LMux = Cono_Muh;

            List<ResultCono> resultCono = desarrollo.CaculoCono(materiales, cono, diseñoElementos, refuerzo);

        }
    }
}
