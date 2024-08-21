using CatalogoProductos_dominio;
using CatalogoProductos_negocio;
using CatalogoProductos_Web.ClasesHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class Site : System.Web.UI.MasterPage
    {
        public bool FiltrosActivos { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginHelper.PaginaNecesitaSesionIniciada(Page)) 
            {
                if (!LoginHelper.HaySesionIniciada(Session)) 
                {
                    Response.Redirect("Login.aspx", true);
                }

                if (!LoginHelper.SesionIniciadaEsAdmin(Session) && LoginHelper.DebeSerAdmin(Page)) 
                {
                    Response.Redirect("Error.aspx?tipo=3", true);
                }
            }

            if (Session["EstablecerDatos"] != null && (bool) Session["EstablecerDatos"]) 
            {
                EstablecerFotoPerfil();
                EstablecerNombrePerfil();
            }

            if (Page is Productos)
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

        //-------------------------------------------------------------------- MÉTODOS ------------------------------------------------------------------------
        public void EstablecerFotoPerfil() 
        {
            Usuario usuarioActual = (Usuario)Session["UsuarioSesionActual"];
            
            if (string.IsNullOrEmpty(usuarioActual.UrlImagenPerfil)) 
            {
                PerfilActualImage.ImageUrl = LoginHelper.PLACEHOLDER_IMAGEN_PERFIL;
                return;
            }

            PerfilActualImage.ImageUrl = UsuarioNegocio.ObtenerRutaCompletaImagenPerfil(usuarioActual.UrlImagenPerfil);
        }

        public void EstablecerNombrePerfil()
        {
            Usuario usuarioActual = (Usuario)Session["UsuarioSesionActual"];

            if (string.IsNullOrEmpty(usuarioActual.Nombre) && string.IsNullOrEmpty(usuarioActual.Apellido))
            {
                return;
            }

            NombreUsuarioActualLabel.Text = $"{usuarioActual.Nombre}, {usuarioActual.Apellido}";
        }

        public void MostrarFiltrosActivos()
        { 
            FiltrosActivos = (bool)Session["FiltrosActivos"];

            BuscarButton.Disabled = FiltrosActivos;
        }

        public bool HayFiltrosActivos()
        {
            return Session["FiltrosActivos"] != null && (bool)Session["FiltrosActivos"];
        }

        public void CargarFiltros() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<object> marcas;
            List<object> categorias;

            if (!ControlesEstanVacios()) 
            {
                return;
            }

            if (MarcasCheckBoxList.Items.Count == 0) 
            {
                marcas = marcaNegocio.ObtenerMarcas().ToList<object>();
                VincularElementoADataSource(MarcasCheckBoxList, marcas, "Descripcion", "Id");
            }

            if (CategoriasCheckBoxList.Items.Count == 0) 
            {
                categorias = categoriaNegocio.ObtenerCategorias().ToList<object>();
                VincularElementoADataSource(CategoriasCheckBoxList, categorias, "Descripcion", "Id");
            }


            if(CondicionDropDownList.Items.Count == 0) 
            {
                List<object> condiciones = new List<object> {
                                                                    new ListItem("Seleccione una opción"),
                                                                    new ListItem("Igual a"),
                                                                    new ListItem("Mayor a"),
                                                                    new ListItem("Menor a"),
                                                                    new ListItem("Mayor o igual a"),
                                                                    new ListItem("Menor o igual a")
                                                                   };
                VincularElementoADataSource(CondicionDropDownList, condiciones, "Text", "Value");
            }
        }

        public void VincularElementoADataSource(ListControl listControl, List<object> fuente, string dataText, string dataValue)
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

        public void DesmarcarCheckBoxs(ListControl checkBoxList)
        {
            foreach (ListItem checkBox in checkBoxList.Items)
            {
                if (checkBox.Selected)
                {
                    checkBox.Selected = !checkBox.Selected;
                }
            }
        }

        //-------------------------------------------------------------------- FUNCIONES ----------------------------------------------------------------------
        public bool ControlesEstanVacios() 
        {
            return MarcasCheckBoxList.Items.Count == 0 && CategoriasCheckBoxList.Items.Count == 0 && CondicionDropDownList.Items.Count == 0;
        }

        public Tuple<List<string>, List<string>, string, decimal, string> ObtenerFiltros()
        {
            List<string> marcas = null;
            List<string> categorias = null;
            decimal precio = -1;
            string condicionPrecio = "";

            if (FiltrosActivos)
            {
                marcas = ObtenerCheckBoxsFiltrosSeleccionados(MarcasCheckBoxList);

                categorias = ObtenerCheckBoxsFiltrosSeleccionados(CategoriasCheckBoxList);

                condicionPrecio = ConvertirCondicion(CondicionDropDownList.SelectedItem.Text);

                precio = decimal.TryParse(ValorFiltroTextBox.Text, out precio) ? precio : -1;
            }

            string tipoOrden = ConvertirCondicion(OrdenTipoDropDownList.SelectedItem.Text);

            return Tuple.Create(marcas, categorias, condicionPrecio, precio, tipoOrden);
        }

        public List<string> ObtenerCheckBoxsFiltrosSeleccionados(CheckBoxList checkBoxList)
        {
            List<string> list = null;
            List<string> listItemsSeleccionados = new List<string>();

            foreach (ListItem control in checkBoxList.Items)
            {
                if (control.Selected)
                {
                    listItemsSeleccionados.Add(control.Text);
                }
            }

            return listItemsSeleccionados.Count > 0 ? listItemsSeleccionados : list;
        }

        public string ConvertirCondicion(string condicion)
        {
            string clausula = "";

            switch (condicion)
            {
                case "A-Z":
                    clausula = "ORDER BY a.Nombre";
                    break;
                case "Z-A":
                    clausula = "ORDER BY a.Nombre DESC";
                    break;
                case "Menor a mayor precio":
                    clausula = "ORDER BY a.Precio";
                    break;
                case "Mayor a menor precio":
                    clausula = "ORDER BY a.Precio DESC";
                    break;
                case "Igual a":
                    clausula = "=";
                    break;
                case "Mayor a":
                    clausula = ">";
                    break;
                case "Menor a":
                    clausula = "<";
                    break;
                case "Mayor o igual a":
                    clausula = ">=";
                    break;
                case "Menor o igual a":
                    clausula = "<=";
                    break;
                default:
                    break;
            }

            return clausula;
        }

        //-------------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------
        protected void BuscarButton_ServerClick(object sender, EventArgs e)
        {
            if (Page.IsValid) 
            {
                ((Productos)Page).AplicarFiltros(campoBusqueda: BusquedaTextBox.Text);
                Session.Add("CampoBusqueda", BusquedaTextBox.Text);
            }
        }

        protected void CheckBox_CheckedChanged(object sender, EventArgs e) 
        {
            CheckBox checkBox = (CheckBox) sender;

            FiltrosActivos = checkBox.Checked;
            Session.Add("FiltrosActivos", FiltrosActivos);
            OrdenTipoDropDownList.SelectedIndex = 0;
            BusquedaTextBox.Text = "";

            if (FiltrosActivos) 
            {
                CargarFiltros();
            }

            MostrarFiltrosActivos();
        }

        protected void AplicarFiltrosButton_ServerClick(object sender, EventArgs e)
        {
            if (!Page.IsValid) 
            {
                return;
            }

            Tuple<List<string>, List<string>, string, decimal, string> filtros = ObtenerFiltros();

            ((Productos)Page).AplicarFiltros(filtros.Item1, filtros.Item2, filtros.Item3, filtros.Item4, filtros.Item5);
        }

        
        protected void LimpiarFiltrosButton_ServerClick(object sender, EventArgs e)
        {
            DesmarcarCheckBoxs(MarcasCheckBoxList);
            DesmarcarCheckBoxs(CategoriasCheckBoxList);

            CondicionDropDownList.SelectedIndex = 0;
            ValorFiltroTextBox.Text = "";

            ((Productos)Page).CargarProductos();
        }

        protected void AplicarOrdenButton_ServerClick(Object sender, EventArgs e) 
        {
            string campoBusqueda = "";

            if (Session["CampoBusqueda"] != null && Session["CampoBusqueda"].ToString() == BusquedaTextBox.Text) 
            {
                campoBusqueda = Session["CampoBusqueda"].ToString();
            }

            Tuple<List<string>, List<string>, string, decimal, string> filtroOrden = ObtenerFiltros();

            ((Productos)Page).AplicarFiltros(tipoOrden: filtroOrden.Item5, campoBusqueda: campoBusqueda);
        }

        protected void CerrarSesionButton_ServerClick(object sender, EventArgs e)
        {   
            LoginHelper.EliminarDatosSession(Session);

            Response.Redirect("Default.aspx");
        }

        //-------------------------------------------------------------------- VALIDATORS ---------------------------------------------------------------------
        protected void ValorFiltroCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CondicionPrecioCustomValidator.IsValid = CondicionDropDownList.SelectedIndex != 0;

                if (!CondicionPrecioCustomValidator.IsValid && !decimal.TryParse(ValorFiltroTextBox.Text, out _))
                {
                    args.IsValid = true;
                    CondicionPrecioCustomValidator.IsValid = args.IsValid;
                    return;
                }

                args.IsValid = decimal.TryParse(ValorFiltroTextBox.Text, out _);
                ValorFiltroTextBox.Focus();
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void CondicionPrecioCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                ValorFiltroCustomValidator.IsValid = decimal.TryParse(ValorFiltroTextBox.Text, out _);

                if (!ValorFiltroCustomValidator.IsValid && CondicionDropDownList.SelectedIndex == 0) 
                {
                   args.IsValid = true;
                   ValorFiltroCustomValidator.IsValid = args.IsValid;
                   return;
                }

                args.IsValid = CondicionDropDownList.SelectedIndex != 0;
                CondicionDropDownList.Focus();
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}