using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using MySql.Data.MySqlClient;
//using MySql.Data;
using System.Text;
using System.Data.SqlClient;

namespace YMonitor.Clases
{
    public class BDInfo
    {
        public string fillgrid()
        {
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder();
            //        string query = "select distinct o.sPedimento,u.nIdAduSec06,o.dFechaPago from sir.SIR_149_PEDIMENTO o "
            // + "inner join sir.SIR_60_REFERENCIAS u on u.sNumPedimento = o.sPedimento "
            // + "inner join sir.SIR_71_SUCURSAL_PATENTE_ADUANA l on l.nIdAduSec06 = u.nIdAduSec06 "
            // + "inner join sir.SIR_70_PATENTES m on m.nIdPatente70 = l.nIdPatente70 "
            //+ "where dFechaPago between '2018-06-01 00:00:00' and '2018-06-30 00:00:00'and u.nIdAduSec06  like '36' and m.nIdPatente70 like '2'";
            DateTime fechaActual = DateTime.Today;
            string query = "select DISTINCT c.snumpedimento as 'PEDIMENTO',c.sReferencia as 'TRAFICO',p.srazonsocial as 'CLIENTE',b.dFechaEntrada as 'FECHA DE ENTRADA',a.sNombre +' '+ a.sApellidoPaterno as 'DOCUMENTADOR',sClaveAduana + sClaveSeccion 'ADUANA'" +
",sPatente as 'PATENTE',m.sclave as 'TIPO DE PEDIMENTO', b.dFechaPago as 'FECHA DE PAGO' from admin.ADMINC_07_CLIENTES p "
+ "inner join sir.SIR_60_REFERENCIAS c on c.nIdClie07 = p.nIdClie07 "
+ "inner join sir.SIR_149_PEDIMENTO b on b.sPedimento = c.snumpedimento "
+ "inner join admin.ADMINA_02_PERSONAL a on a.nidpers02 = nidejecutivo02 "
+ "inner join  sir.SIR_71_SUCURSAL_PATENTE_ADUANA r on r.nIdSucPatAdu71 = c.nIdSucPatAdu71 "
+ "inner join sir.SIR_70_PATENTES t on t.nidpatente70 = r.nIdPatente70 "
+ "inner join sir.SIR_28_CLAVES_DOCTOS m on m.nIdClaveDocto28 = c.nIdClaveDocto28 "
+ "inner join sir.SIR_06_ADUANA_SEC s on s.nIdAduSec06 = r.nIdAduSec06 "
+ "where sClaveAduana + sClaveSeccion = '810' AND Year(dFechaEntrada) = " + fechaActual.Year + " AND MONTH(dFechaEntrada) = " + fechaActual.Month + " ";
            //csb.DataSource = "localhost";
            csb.DataSource = "192.168.254.5";
            csb.InitialCatalog = "SIR";
            csb.IntegratedSecurity = false;
            csb.ConnectTimeout = 600;
            csb.UserID = "GCIT";
            csb.Password = "@#reporteador20";
            //csb.UserID = "sa";
            //csb.Password = "W1zard05";
            DataTable qResult = new DataTable();
            SqlConnection con = new SqlConnection(csb.ConnectionString);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(query, con);
            adapter.SelectCommand.CommandTimeout = 600;
            qResult.Clear();
            adapter.Fill(qResult);




            StringBuilder sb = new StringBuilder();
            sb.Append("<table id=example2 class=" + '"' + "table table-bordered table-hover" + '"' + ">");
            sb.Append("<thead>");
            sb.Append("<tr>");
            sb.Append("<th>PEDIMENTO</th>");
            sb.Append("<th>TRAFICO</th>");
            sb.Append("<th>CLIENTE</th>");
            sb.Append("<th>FECHA ENTRADA</th>");
            sb.Append("<th>DOCUMENTADOR</th>");
            sb.Append("<th>ADUANA</th>");
            sb.Append("<th>PATENTE</th>");
            sb.Append("<th>TIPO PEDIMENTO</th>");
            sb.Append("<th>FECHA PAGO</th>");
            sb.Append("</tr>");
            sb.Append("</thead>");
            sb.Append("<tbody>");
            foreach (DataRow dr in qResult.Rows)
            {
                string trafico = "null";
                string cliente = "null";
                string month = "null";
                string year = "null";

                foreach (DataColumn dc1 in qResult.Columns)
                {
                    if (dc1.ColumnName == "CLIENTE")
                    {
                        cliente = dr[dc1.ColumnName].ToString();
                    }
                    else
                        if (dc1.ColumnName == "FECHA ENTRADA")
                    {
                        year = dr[dc1.ColumnName].ToString();
                        month = dr[dc1.ColumnName].ToString();
                    }
                    else
                            if (dc1.ColumnName == "TRAFICO")
                    {
                        trafico = dr[dc1.ColumnName].ToString();
                    }
                    else
                                if (trafico != "null" && year != "null" && month != "null" && cliente != "null")

                        goto here1;

                }

            here1:
                sb.Append("<tr>");
                foreach (DataColumn dc in qResult.Columns)
                {
                    if (dc.ColumnName == "TRAFICO")
                    {
                        string q = "\"";
                        sb.Append("<td>");
                        sb.Append("<form action='http://192.168.254.6/getfiles/' method='post'>");
                        sb.Append("<input type ='hidden' name = 'dirurl' value = ' " + cliente + q + year + q + month + q + trafico + " '/>");
                        sb.Append("<input type ='hidden' name = 'secret' value = 'xyz' />");
                        // para la columna de trafico
                        sb.Append("<button type='submit' class='btn btn-link' name='envio_prueba'>" + trafico + "</button>");
                        //sb.Append("<a href='http://192.168.254.6/getfiles/' class='btn btn-link';>" + trafico +"</a>");
                        /* 
                         * "R:\SirRecursos\ControlDocumentos\CLIENTE\AÑO-FECHA ENTRADA\MES-FECHA ENTRADA\TRAFICO"
                          */
                        sb.Append("</form>");
                        sb.Append("</td>");
                    }
                    else
                    {
                        sb.Append("<td>");
                        sb.Append(dr[dc.ColumnName].ToString());
                        sb.Append("</td>");
                    }
                }


                sb.Append("</tr>");
            }
            sb.Append("</tbody>");
            sb.Append("<tfoot>");
            sb.Append("<tr>");
            sb.Append("<th>PEDIMENTO</th>");
            sb.Append("<th>TRAFICO</th>");
            sb.Append("<th>CLIENTE</th>");
            sb.Append("<th>FECHA ENTRADA</th>");
            sb.Append("<th>DOCUMENTADOR</th>");
            sb.Append("<th>ADUANA</th>");
            sb.Append("<th>PATENTE</th>");
            sb.Append("<th>TIPO PEDIMENTO</th>");
            sb.Append("<th>FECHA PAGO</th>");
            sb.Append("</tr>");
            sb.Append("<tfoot>");
            sb.Append("</table>");
            //string Tablaweb = sb.ToString();
            string Tablaweb = "<table><thead><tr><th>Nombre</th></tr></thead><tbody><tr><td>Miguel</td></tr></tbody></table>";
            return Tablaweb;



        }
    }
}