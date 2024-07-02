using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public bool FiltroMarcaActivo { get; set; }
        public bool FiltroCategoriaActivo { get; set; }
        public bool FiltroPrecioActivo { get; set; }
        public bool FiltrosActivos { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page is Productos)
            {
                CargarTiposOrden();
                MostrarFiltrosActivos();
            }
        }

        public void MostrarFiltrosActivos() 
        {
            // Sirve para mantener dibujado la sección de los filtros pertinentes en los PostBack

            if (!HayFiltrosActivos())
            {
                return;
            }

            FiltrosActivos = Session["FiltrosActivos"] != null;
            
            FiltroMarcaActivo = Session["FiltroMarca"] != null;
            
            FiltroCategoriaActivo = Session["FiltroCategoria"] != null;
            
            FiltroPrecioActivo = Session["FiltroPrecio"] != null;
        }

        public bool HayFiltrosActivos() 
        {
            return Session["FiltroMarca"] != null || Session["FiltroCategoria"] != null || Session["FiltroPrecio"] != null;
        }

        public void CargarFiltros() 
        {

            if (FiltroMarcaActivo) 
            {
                MarcaNegocio marcaNegocio = new MarcaNegocio();
                List<object> marcas = marcaNegocio.ObtenerMarcas().ToList<object>();

                VincularADataSource(MarcasCheckBoxList, marcas, "Descripcion", "Id");
            }

            if (FiltroCategoriaActivo) 
            {
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                List<object> categorias = categoriaNegocio.ObtenerCategorias().ToList<object>();

                VincularADataSource(CategoriasCheckBoxList, categorias, "Descripcion", "Id");
            }

            if (FiltroPrecioActivo) 
            {
                CondicionDropDownList.Items.AddRange(new ListItem[] {
                                                                        new ListItem("Seleccione una opción", ""),
                                                                        new ListItem("Igual a", "="),
                                                                        new ListItem("Mayor a", ">"),
                                                                        new ListItem("Menor a", "<"),
                                                                        new ListItem("Mayor o igual a", ">="),
                                                                        new ListItem("Menor o igual a", "<=")});
            }
        }

        public void VincularADataSource(ListControl listControl, List<object> fuente, string dataText, string dataValue) 
        {
            listControl.DataSource = fuente;
            listControl.DataTextField = dataText;
            listControl.DataValueField = dataValue;
            listControl.DataBind();
        }

        public void CargarTiposOrden()
        {
            OrdenTipoDropDownList.Items.AddRange(new ListItem[] {
                                                                    new ListItem("Seleccione una opción"),
                                                                    new ListItem("A-Z"),
                                                                    new ListItem("Z-A"),
                                                                    new ListItem("Menor a mayor precio"),
                                                                    new ListItem("Mayor a menor precio")});
        }

        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            
        }

        protected void CheckBox_CheckedChanged(object sender, EventArgs e) 
        {
            CheckBox checkBox = (CheckBox) sender;

            if (checkBox.ID == "MarcaCheckBox") 
            {
                FiltroMarcaActivo = checkBox.Checked;
                Session.Add("FiltroMarca", FiltroMarcaActivo);
            }

            if (checkBox.ID == "CategoriaCheckBox") 
            {
                FiltroCategoriaActivo = checkBox.Checked;
                Session.Add("FiltroCategoria", FiltroCategoriaActivo);
            }

            if (checkBox.ID == "PrecioCheckBox") 
            {
                FiltroPrecioActivo = checkBox.Checked;
                Session.Add("FiltroPrecio", FiltroPrecioActivo);
            }

            FiltrosActivos = FiltroMarcaActivo || FiltroCategoriaActivo || FiltroPrecioActivo;
            Session.Add("FiltrosActivos", FiltrosActivos);

            if (FiltrosActivos) 
            {
                CargarFiltros();
            }
        }

        protected void AplicarFiltrosButton_ServerClick(object sender, EventArgs e)
        {
           List<string> marcasAux = new List<string>();
           List<string> marcas = null;
           List<string> categoriasAux = new List<string>();
           List<string> categorias = null;

            foreach (ListItem control in MarcasCheckBoxList.Items)
            {
                if (control.Selected)
                {
                    marcasAux.Add(control.Text);
                }
            }

            if (marcasAux.Count > 0) 
            {
                marcas = marcasAux;
            }

            foreach (ListItem control in CategoriasCheckBoxList.Items)
            {
                if (control.Selected) 
                {
                    categoriasAux.Add(control.Text);
                }
            }

            if (categoriasAux.Count > 0) 
            {
                categorias = categoriasAux;
            }

            string condicionPrecio = CondicionDropDownList.SelectedValue;

            decimal.TryParse(ValorFiltroTextBox.Text, out decimal precio);

            string tipoOrden = ConvertirTipoOrden(OrdenTipoDropDownList.SelectedItem.Text);

            ((Productos)Page).AplicarFiltros(marcas, categorias, condicionPrecio, precio, tipoOrden);
        }

        public string ConvertirTipoOrden(string tipoOrden) 
        {
            string clausula = "";

            switch (tipoOrden)
            {
                case "A-Z": clausula = "ORDER BY a.Nombre";
                    break;
                case "Z-A": clausula = "ORDER BY a.Nombre DESC";
                    break;
                case "Menor a mayor precio": clausula = "ORDER BY a.Precio";
                    break;
                case "Mayor a menor precio": clausula = "ORDER BY a.Precio DESC";
                    break;
                default:
                    break;
            }

            return clausula;
        }

        protected void LimpiarFiltrosButton_ServerClick(object sender, EventArgs e)
        {

        }
    }
}