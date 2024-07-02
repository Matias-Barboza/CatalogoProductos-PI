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
            if (Page is Productos productos)
            {
                if (!IsPostBack) 
                {
                    CargarTiposOrden();
                    return;
                }

                if (HayFiltrosActivos()) 
                {
                    MostrarFiltrosActivos();
                }
            }
        }

        public void MostrarFiltrosActivos()
        { 
            FiltrosActivos = Session["FiltrosActivos"] != null && (bool)Session["FiltrosActivos"];

            FiltroMarcaActivo = Session["FiltroMarca"] != null && (bool)Session["FiltroMarca"];

            FiltroCategoriaActivo = Session["FiltroCategoria"] != null && (bool)Session["FiltroCategoria"];

            FiltroPrecioActivo = Session["FiltroPrecio"] != null && (bool)Session["FiltroPrecio"];
        }

        public bool HayFiltrosActivos()
        {
            return Session["FiltrosActivos"] != null && (bool)Session["FiltrosActivos"];
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

            MostrarFiltrosActivos();
        }

        protected void AplicarFiltrosButton_ServerClick(object sender, EventArgs e)
        {
            Tuple<List<string>, List<string>, string, decimal, string> filtros = ObtenerFiltros();

            ((Productos)Page).AplicarFiltros(filtros.Item1, filtros.Item2, filtros.Item3, filtros.Item4, filtros.Item5);
        }

        public Tuple<List<string>,List<string>,string,decimal,string> ObtenerFiltros() 
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

            return Tuple.Create(marcas, categorias, condicionPrecio, precio, tipoOrden);
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
            ((Productos)Page).CargarProductos();
        }

        protected void AplicarOrdenButton_ServerClick(Object sender, EventArgs e) 
        {
            Tuple<List<string>, List<string>, string, decimal, string> filtroOrden = ObtenerFiltros();

            ((Productos)Page).AplicarFiltros(null,null,"",-1,filtroOrden.Item5);
        }
    }
}