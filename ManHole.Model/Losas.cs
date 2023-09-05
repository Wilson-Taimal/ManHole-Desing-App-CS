using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHole.Model
{
    public class Losas
    {// -------------------------------------------------------------------------------------------------------------------------------
        // DATOS DE ENTRADA //

        /// <summary>
        /// Ancho de la sección _ [cm]
        /// </summary>
        public double b;

        /// <summary>
        /// Espesor del elemento _ [cm]
        /// </summary>
        public double h;

        /// <summary>
        /// Recubrimiento del refuerzo _ [cm]
        /// </summary>
        public double r;

        /// <summary>
        /// Altura efectiva _ [cm]
        /// </summary>
        public double d;

        /// <summary>
        /// Cuantía mínima de refuerzo en losas
        /// </summary>
        public double pmin;

        /// <summary>
        /// Número de la barra a emplear
        /// </summary>
        public int Nb;

        /// <summary>
        /// Lista o vector de momentos de diseño positivos {Mux(+), Muy(+)} _ [kN.cm]
        /// </summary>
        public double[] LMup;

        /// <summary>
        /// Lista o vector de momentos de diseño negativos {Mux(-), Muy(-)} _ [kN.cm]
        /// </summary>
        public double[] LMun;

        /// <summary>
        /// Lista o vector de fuerzas cortantes {Vux, Vuy} _ [kN]
        /// </summary>
        public double[] LVu;


        // -------------------------------------------------------------------------------------------------------------------------------
        // METODOS //

        public string ChequeoMomentoNominal(double Mu, double Mn)
        {
            string Opcion1 = "Cumple";
            string Opcion2 = "No cumple";

            if (Mu < Mn)
            { return Opcion1; }
            else
            { return Opcion2; }

        }

        public string ChequeoCortante(double Vu, double Vc)
        {
            string Opcion1 = "Cumple";
            string Opcion2 = "No cumple";

            if (Vc > Vu)
            { return Opcion1; }
            else
            { return Opcion2; }

        }
    }
}
