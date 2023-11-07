using Balances.Model;
using System.Data;
using System.Text;
using WebService;

namespace BuscarIGJ
{
    public class BusquedaEntidadService
    {
        public static async Task<WebService.Entidad> BusquedaEntidadByCorrelativo(int correlativo)
        {
           var entidad = new WebService.Entidad();


            GJ_GenericSoapClient ws = new GJ_GenericSoapClient(GJ_GenericSoapClient.EndpointConfiguration.IGJ_GenericSoap);

            var entidadPorCorrelativo = await ws.GetSociedadAsync(correlativo);

            //CARGO EL MODELO CON LA RESPUESTA

            DataSet ds = ToDataSet(entidadPorCorrelativo);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];

                entidad.NroCorrelativo = Convert.ToInt32(dr[0]);
                entidad.RazonSocial = dr[1].ToString();
                entidad.TipoSoc = dr[2].ToString();


            }

            return entidad;
        }


        public static async Task<WebService.Entidad> GetByCuit(long Cuit)
        {

            WebService.Entidad entidad = new WebService.Entidad();

            GJ_GenericSoapClient wsIGJ = new GJ_GenericSoapClient(GJ_GenericSoapClient.EndpointConfiguration.IGJ_GenericSoap);

            entidad = await wsIGJ.GetSociedadbyCUITAsync(Convert.ToInt64(Cuit));




            return entidad;
        }


        public static DataSet ToDataSet(ArrayOfXElement arrayOfXElement)
        {
            var strSchema = arrayOfXElement.Nodes[0].ToString();
            var strData = arrayOfXElement.Nodes[1].ToString();
            var strXml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\n\t<DataSet>";
            strXml += strSchema + strData;
            strXml += "</DataSet>";

            DataSet ds = new DataSet("TestDataSet");
            ds.ReadXml(new MemoryStream(Encoding.UTF8.GetBytes(strXml)));

            return ds;
        }
    }
}
