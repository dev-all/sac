using Datos.Interfaces;
using Datos.ModeloDeDatos;
using Negocio.Interfaces;
using Negocio.Modelos;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Negocio.Helpers;
using Entidad.Modelos;
using Datos.Repositorios;

namespace Negocio.Servicios
{
   public class ServicioFacturaVentaItems : ServicioBase
    {
        private FacturaVentasRepositorio oFacturaVenta;
        private ItemImprRepositorio oItemImpr;

        public Action<string, string> _mensaje;

        public ServicioFacturaVentaItems()
        {
            oFacturaVenta = kernel.Get<FacturaVentasRepositorio>();
            oItemImpr = kernel.Get<ItemImprRepositorio>();
        }


        public FacturaVentaItemsModel ObtenerDatosFacturaItems(int idCliente, int nroFactura, int idComprobante)
        {
            FacturaVentaItemsModel facturaItem = new FacturaVentaItemsModel();
            try
            {
                FacturaVentaModel factura = Mapper.Map <FactVenta, FacturaVentaModel>(oFacturaVenta.GetFacturaVentaPorNumero(nroFactura, idComprobante.ToString()));
                List<ItemImprModel> listaItems = Mapper.Map<List<ItemImpre>, List<ItemImprModel>>(oItemImpr.GetAllItemImpreNroFactura(nroFactura, idComprobante));

                facturaItem.factura = factura;
                facturaItem.items = listaItems;
            
                return facturaItem;
            }
            catch(Exception ex)
            {
                return null;
            }


        }


    }
}
