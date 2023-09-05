﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManHole.Model
{
    public class ResultLosas
    {
        /// <summary>
        /// preq: Cuantía de refuerzo requerida para un momento ultimo de diseño dado
        /// </summary>
        public double preq { get; set; }

        /// <summary>
        /// Asreq: Área de acero requerido para una cuantia requerida _ [cm²]
        /// </summary>        
        public double Asreq { get; set; }

        /// <summary>
        /// Asmin: Área de acero mínimo de la sección _ [cm²]
        /// </summary>
        public double Asmin { get; set; }

        /// <summary>
        /// As: Área de acero obtenido por diseño igual al maximo entre Asreq y Asmin _ [cm²]
        /// </summary>
        public double As { get; set; }

        /// <summary>
        /// Ascol: Área de acero colocado que depende de la distribucion final de las barras de refuerzo _ [cm²]
        /// </summary>
        public double Ascol { get; set; }

        /// <summary>
        /// Asb: Área de la sección transversal de la barra selecionada _ [cm²]
        /// </summary>
        public double Asb { get; set; }

        /// <summary>
        /// sep: Separación entre barras de refuerzo en un ancho "b" _ [cm]
        /// </summary>        
        public double Sep { get; set; }

        /// <summary>
        /// Cat: Cantidad de barras empleadas en un ancho "b".
        /// </summary>        
        public double Cant { get; set; }

        /// <summary>
        /// Mup: Momento de diseño positivo de la sección _ [kN.cm]
        /// </summary>        
        public double Mup { get; set; }

        /// <summary>
        /// Mun: Momento de diseño negativo de la sección _ [kN.cm]
        /// </summary>        
        public double Mun { get; set; }

        /// <summary>
        /// Vu: Fuerza cortante de diseño _ [kN]
        /// </summary>        
        public double Vu { get; set; }

        /// <summary>
        /// Mn: Momento nominal de la sección _ [kN.cm]
        /// </summary>        
        public double Mn { get; set; }

        /// <summary>
        /// Vc: Resistencia del concreto al corte _ [kN]
        /// </summary>        
        public double Vc { get; set; }

        /// <summary>
        /// Chequeo de momento nominal de la sección (Mu < øMn)
        /// </summary>
        public string ChequeoMomentoNominal { get; set; }

        /// <summary>
        /// Chequeo resistencia del concreto al cortante (øVc > Vu)
        /// </summary>
        public string ChequeoCortante { get; set; }

    }
}
