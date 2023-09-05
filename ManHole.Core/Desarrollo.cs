using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ManHole.Model;

namespace ManHole.Core
{
    public class Desarrollo
    {
        // CÁLCULO DE CARGAS Y EMPUJES //
        public List<ResultCargas> CalculoEmpujes(GeomEstructura GeomEst, Suelo suelo, Cargas cargas)
        {
            List<ResultCargas> listaresultado = new List<ResultCargas>();
            ResultCargas resulCargas = new ResultCargas();

            if (suelo.H1 == 0)
            {
                resulCargas.EH = cargas.EmpujeHorizontal(suelo.fis, suelo.rs, GeomEst.HT);
            }
            else
            {
                resulCargas.WA0 = cargas.PresionAgua0(suelo.fis, suelo.rs);
                resulCargas.WA1 = cargas.PresionAgua1(suelo.fis, suelo.rs, suelo.H1);
                resulCargas.WA2 = cargas.PresionAgua2(GeomEst.HT, suelo.H1, suelo.fis, suelo.rs, suelo.rsat, suelo.rw);
            }

            if (cargas.SentidoMuro == 0)
            {
                resulCargas.LSs_per = cargas.SobrecargaVivaS_per(suelo.fis, suelo.rs);
                resulCargas.LSi_per = cargas.SobrecargaVivaI_per(GeomEst.HT, suelo.fis, suelo.rs);
            }
            else
            {
                resulCargas.LSs_par = cargas.SobrecargaVivaS_par(suelo.fis, suelo.rs);
                resulCargas.LSi_par = cargas.SobrecargaVivaI_par(GeomEst.HT, suelo.fis, suelo.rs);
            }

            listaresultado.Add(resulCargas);
            return listaresultado;
        }
        // ------------------------------------------------------------------------------------------------------------------------------- //



        // ===== ANALISIS DE ESTABILIDAD ===== //
        public List<ResultEstabilidad> CalculoEstabiliada(Estabilidad estabilidad, Suelo suelo)
        {
            List<ResultEstabilidad> listaresultado = new List<ResultEstabilidad>();
            ResultEstabilidad resultEstabilidad = new ResultEstabilidad();
            resultEstabilidad.Esfuerzo = estabilidad.Esfuerzo(estabilidad.Qs, suelo.Qadm);
            resultEstabilidad.Asentamiento = estabilidad.Asentamiento(estabilidad.Si, estabilidad.Sadm);

            listaresultado.Add(resultEstabilidad);
            return listaresultado;
        }
        // ------------------------------------------------------------------------------------------------------------------------------- //



        //  ===== DISEÑO LOSAS =====  //
        public List<ResultLosas> CalculoLosas(Materiales materiales, Losas losas, DiseñoElementos diseñoElementos, Refuerzo refuerzo)
        {
            List<ResultLosas> listaresultado = new List<ResultLosas>();

            // Diseño para momentos negativos
            for (int i = 0; i < losas.LMun.Length; i++)
            {
                ResultLosas resultLosas = new ResultLosas();
                resultLosas.Mun = losas.LMun[i];
                resultLosas.Asmin = diseñoElementos.AceroMinimo(losas.pmin, losas.b, losas.h);
                resultLosas.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, losas.LMun[i], losas.b, losas.d);
                resultLosas.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, losas.LMun[i], losas.b, losas.d);
                resultLosas.As = diseñoElementos.Acero(resultLosas.Asreq, resultLosas.Asmin);
                resultLosas.Asb = refuerzo.AreaBarra(losas.Nb);
                resultLosas.Cant = diseñoElementos.CantBarras(resultLosas.As, resultLosas.Asb);
                resultLosas.Sep = diseñoElementos.SepBarras(losas.b, resultLosas.As, resultLosas.Asb);

                // Chequeos
                resultLosas.Ascol = diseñoElementos.AceroColocado(resultLosas.Cant, resultLosas.Asb);
                resultLosas.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultLosas.Ascol, losas.b, losas.d);
                resultLosas.ChequeoMomentoNominal = losas.ChequeoMomentoNominal(resultLosas.Mun, resultLosas.Mn);

                listaresultado.Add(resultLosas);
            }

            // Diseño para momentos positivos
            for (int i = 0; i < losas.LMup.Length; i++)
            {
                ResultLosas resultLosas = new ResultLosas();
                resultLosas.Mup = losas.LMup[i];
                resultLosas.Asmin = diseñoElementos.AceroMinimo(losas.pmin, losas.b, losas.h);
                resultLosas.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, losas.LMup[i], losas.b, losas.d);
                resultLosas.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, losas.LMup[i], losas.b, losas.d);
                resultLosas.As = diseñoElementos.Acero(resultLosas.Asreq, resultLosas.Asmin);
                resultLosas.Asb = refuerzo.AreaBarra(losas.Nb);
                resultLosas.Cant = diseñoElementos.CantBarras(resultLosas.As, resultLosas.Asb);
                resultLosas.Sep = diseñoElementos.SepBarras(losas.b, resultLosas.As, resultLosas.Asb);

                // Chequeos
                resultLosas.Ascol = diseñoElementos.AceroColocado(resultLosas.Cant, resultLosas.Asb);
                resultLosas.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultLosas.Ascol, losas.b, losas.d);
                resultLosas.ChequeoMomentoNominal = losas.ChequeoMomentoNominal(resultLosas.Mup, resultLosas.Mn);

                listaresultado.Add(resultLosas);
            }

            // Chequeo Cortante
            for (int i = 0; i < losas.LVu.Length; i++)
            {
                ResultLosas resultLosas = new ResultLosas();
                resultLosas.Vu = losas.LVu[i];
                resultLosas.Vc = diseñoElementos.CortConcreto(materiales.fiv, materiales.fc, losas.b, losas.d);
                resultLosas.ChequeoCortante = losas.ChequeoCortante(resultLosas.Vu, resultLosas.Vc);

                listaresultado.Add(resultLosas);
            }

            return listaresultado;
        }
        // ------------------------------------------------------------------------------------------------------------------------------- //
        //  ===== DISEÑO MUROS =====  //

        public List<ResultMuros> CaculoMuros(Materiales materiales, Muros muros, DiseñoElementos diseñoElementos, Refuerzo refuerzo)
        {
            List<ResultMuros> listaresultado = new List<ResultMuros>();

            // Refuerzo Vertical
            for (int i = 0; i < muros.LMuy.Length; i++)
            {
                ResultMuros resultMuros = new ResultMuros();
                resultMuros.Muy = muros.LMuy[i];
                resultMuros.Asmin = diseñoElementos.AceroMinimo(muros.pmin, muros.b, muros.h);
                resultMuros.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, muros.LMuy[i], muros.b, muros.d);
                resultMuros.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, muros.LMuy[i], muros.b, muros.d);
                resultMuros.As = diseñoElementos.Acero(resultMuros.Asreq, resultMuros.Asmin);
                resultMuros.Asb = refuerzo.AreaBarra(muros.Nb);
                resultMuros.Cant = diseñoElementos.CantBarras(resultMuros.As, resultMuros.Asb);
                resultMuros.Sep = diseñoElementos.SepBarras(muros.b, resultMuros.As, resultMuros.Asb);
               
                // Chequeos
                resultMuros.Ascol = diseñoElementos.AceroColocado(resultMuros.Cant, resultMuros.Asb);
                resultMuros.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultMuros.Ascol, muros.b, muros.d);
                resultMuros.ChequeoMomentoNominal = muros.ChequeoMomentoNominal(resultMuros.Muy, resultMuros.Mn);

                listaresultado.Add(resultMuros);
            }

            // Refuerzo Horizontal
            for (int i = 0; i < muros.LMux.Length; i++)
            {
                ResultMuros resultMuros = new ResultMuros();
                resultMuros.Mux = muros.LMux[i];
                resultMuros.Asmin = diseñoElementos.AceroMinimo(muros.pmin, muros.b, muros.h);
                resultMuros.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, muros.LMux[i], muros.b, muros.d);
                resultMuros.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, muros.LMux[i], muros.b, muros.d);
                resultMuros.As = diseñoElementos.Acero(resultMuros.Asreq, resultMuros.Asmin);
                resultMuros.Asb = refuerzo.AreaBarra(muros.Nb);
                resultMuros.Cant = diseñoElementos.CantBarras(resultMuros.As, resultMuros.Asb);
                resultMuros.Sep = diseñoElementos.SepBarras(muros.b, resultMuros.As, resultMuros.Asb);                

                // Chequeos
                resultMuros.Ascol = diseñoElementos.AceroColocado(resultMuros.Cant, resultMuros.Asb);
                resultMuros.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultMuros.Ascol, muros.b, muros.d);
                resultMuros.ChequeoMomentoNominal = muros.ChequeoMomentoNominal(resultMuros.Mux, resultMuros.Mn);

                listaresultado.Add(resultMuros);
            }

            // Chequeo Cortante
            {
                ResultMuros resultMuros = new ResultMuros();
                resultMuros.Vu = muros.Vu;
                resultMuros.Vc = diseñoElementos.CortConcreto(materiales.fiv, materiales.fc, muros.b, muros.d);
                resultMuros.ChequeoCortante = muros.ChequeoCortante(resultMuros.Vu, resultMuros.Vc);

                listaresultado.Add(resultMuros);
            }
            
            return listaresultado;
        }


        // ------------------------------------------------------------------------------------------------------------------------------- //
        //  ===== DISEÑO CONO =====  //
        public List<ResultCono> CaculoCono(Materiales materiales, Cono cono, DiseñoElementos diseñoElementos, Refuerzo refuerzo)
        {
            List<ResultCono> listaresultado = new List<ResultCono>();

            // Refuerzo Vertical
            for (int i = 0; i < cono.LMuy.Length; i++)
            {
                ResultCono resultCono = new ResultCono();
                resultCono.Muy = cono.LMuy[i];
                resultCono.Asmin = diseñoElementos.AceroMinimo(cono.pmin, cono.b, cono.h);
                resultCono.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, cono.LMuy[i], cono.b, cono.d);
                resultCono.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, cono.LMuy[i], cono.b, cono.d);
                resultCono.As = diseñoElementos.Acero(resultCono.Asreq, resultCono.Asmin);
                resultCono.Asb = refuerzo.AreaBarra(cono.Nb);
                resultCono.Cant = diseñoElementos.CantBarras(resultCono.As, resultCono.Asb);
                resultCono.Sep = diseñoElementos.SepBarras(cono.b, resultCono.As, resultCono.Asb);

                // Chequeos
                resultCono.Ascol = diseñoElementos.AceroColocado(resultCono.Cant, resultCono.Asb);
                resultCono.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultCono.Ascol, cono.b, cono.d);
                resultCono.ChequeoMomentoNominal = cono.ChequeoMomentoNominal(resultCono.Muy, resultCono.Mn);

                listaresultado.Add(resultCono);
            }

            // Refuerzo Horizontal
            for (int i = 0; i < cono.LMux.Length; i++)
            {
                ResultCono resultCono = new ResultCono();
                resultCono.Mux = cono.LMux[i];
                resultCono.Asmin = diseñoElementos.AceroMinimo(cono.pmin, cono.b, cono.h);
                resultCono.preq = diseñoElementos.Cuantia(materiales.fc, materiales.fy, cono.LMux[i], cono.b, cono.d);
                resultCono.Asreq = diseñoElementos.AceroRequerido(materiales.fc, materiales.fy, cono.LMux[i], cono.b, cono.d);
                resultCono.As = diseñoElementos.Acero(resultCono.Asreq, resultCono.Asmin);
                resultCono.Asb = refuerzo.AreaBarra(cono.Nb);
                resultCono.Cant = diseñoElementos.CantBarras(resultCono.As, resultCono.Asb);
                resultCono.Sep = diseñoElementos.SepBarras(cono.b, resultCono.As, resultCono.Asb);

                // Chequeos
                resultCono.Ascol = diseñoElementos.AceroColocado(resultCono.Cant, resultCono.Asb);
                resultCono.Mn = diseñoElementos.MomNominal(materiales.fc, materiales.fy, resultCono.Ascol, cono.b, cono.d);
                resultCono.ChequeoMomentoNominal = cono.ChequeoMomentoNominal(resultCono.Mux, resultCono.Mn);

                listaresultado.Add(resultCono);
            }

            // Chequeo Cortante
            {
                ResultCono resultCono = new ResultCono();
                resultCono.Vu = cono.Vu;
                resultCono.Vc = diseñoElementos.CortConcreto(materiales.fiv, materiales.fc, cono.b, cono.d);
                resultCono.ChequeoCortante = cono.ChequeoCortante(resultCono.Vu, resultCono.Vc);

                listaresultado.Add(resultCono);
            }

            return listaresultado;
        }
    }
}
