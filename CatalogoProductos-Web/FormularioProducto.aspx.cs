using CatalogoProductos_negocio;
using CatalogoProductos_dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CatalogoProductos_Web
{
    public partial class FormularioProducto : System.Web.UI.Page
    {
        public bool EsEdicion { get; set; }
        public bool DatosArticuloCargados { get; set; }
        public bool ImagenPorArchivo { get; set; }
        public bool DebeConfirmarEliminacion { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Si viene vacio debe navegar a pagina de error
            if (!IsPostBack) 
            {
                UrlRadioButton.Checked = true;
                CargarDesplegables();
                EstablecerPlaceHolderImagenes();
                Session.Add("DatosArticuloCargados", false);
            }

            if (Session["DatosArticuloCargados"] != null) 
            {
                DatosArticuloCargados = (bool)Session["DatosArticuloCargados"];
            }

            if (Request.QueryString["id"] != null) 
            {
                if (!int.TryParse(Request.QueryString["id"], out int id))
                {
                    Response.Redirect("Error.aspx?tipo=4", true);
                }

                EsEdicion = true;

                if (!DatosArticuloCargados)
                {
                    CargarDatosDelProducto(id);
                    DatosArticuloCargados = true;
                    Session.Add("DatosArticuloCargados", DatosArticuloCargados);
                }
            }

            if (Session["ImagenPorArchivo"] != null) 
            {
                ImagenPorArchivo = (bool)Session["ImagenPorArchivo"];
            }

            if (Session["DebeConfirmarEliminacion"] != null) 
            {
                DebeConfirmarEliminacion = (bool)Session["DebeConfirmarEliminacion"];
            }
        }


        //----------------------------------------------------------------- MÉTODOS ---------------------------------------------------------------------------
        public void CargarDesplegables() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Marca> fuenteMarcas = marcaNegocio.ObtenerMarcas();
            List<Categoria> fuenteCategorias = categoriaNegocio.ObtenerCategorias();

            VincularADataSource(MarcasDropDownList, fuenteMarcas.ToList<object>());
            VincularADataSource(CategoriasDropDownList, fuenteCategorias.ToList<object>());
        }

        public void EstablecerPlaceHolderImagenes() 
        {
            ActualImagen.ImageUrl = ArticuloNegocio.PLACEHOLDER_IMAGEN;
            NuevaImagen.ImageUrl = ArticuloNegocio.PLACEHOLDER_IMAGEN;
        }

        private void VincularADataSource(DropDownList dropDownList, List<object> fuente) 
        {
            dropDownList.DataSource = fuente;
            dropDownList.DataTextField = "Descripcion";
            dropDownList.DataValueField = "Id";
            dropDownList.DataBind();
            dropDownList.Items.Insert(0, "Seleccione una opción");
        }

        public void CargarDatosDelProducto(int id) 
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articuloACargar = articuloNegocio.ObtenerArticuloPorId(id);

            CodigoArticuloTextBox.Text = articuloACargar.CodigoArticulo;
            NombreArticuloTextBox.Text = articuloACargar.Nombre;
            DescripcionArticuloTextBox.Text = articuloACargar.Descripcion;
            MarcasDropDownList.Items.FindByText(articuloACargar.Marca.Descripcion).Selected = true;
            CategoriasDropDownList.Items.FindByText(articuloACargar.Categoria.Descripcion).Selected = true;
            PrecioArticuloTextBox.Text = ArticuloNegocio.PrecioFormateado(articuloACargar.Precio).Substring(2);
            UrlImagenTextBox.Text = SetearRutaImagenActual(articuloACargar.ImagenUrl);

            ActualImagen.ImageUrl = ArticuloNegocio.ConfigurarRutaImagen(articuloACargar.ImagenUrl);
        }

        //------------------------------------------------------------- FUNCIONES -----------------------------------------------------------------------------
        public Articulo VincularArticuloADatos(Articulo articulo)
        {
            articulo.CodigoArticulo = CodigoArticuloTextBox.Text;
            articulo.Nombre = NombreArticuloTextBox.Text;
            articulo.Descripcion = DescripcionArticuloTextBox.Text;
            articulo.Marca.Id = Convert.ToInt32(MarcasDropDownList.SelectedValue);
            articulo.Categoria.Id = Convert.ToInt32(CategoriasDropDownList.SelectedValue);
            articulo.Precio = Convert.ToDecimal(PrecioArticuloTextBox.Text);

            return articulo;
        }

        public string ObtenerImagenUrl(int idArticulo)
        {
            string rutaAGuardar = null;

            if (ImagenPorArchivo)
            {
                string rutaACarpetaImagenes = Server.MapPath("./Imagenes/");
                string rutaCompleta;

                rutaAGuardar = $"imagenArticulo{idArticulo}.jpg";
                rutaCompleta = rutaACarpetaImagenes + rutaAGuardar;

                ImagenLocalInput.PostedFile.SaveAs(rutaCompleta);
            }
            else
            {
                rutaAGuardar = UrlImagenTextBox.Text;
            }

            return rutaAGuardar;
        }

        public string SetearRutaImagenActual(string rutaImagenArticulo)
        {
            if (rutaImagenArticulo.Contains("https://") || rutaImagenArticulo.Contains("http://"))
            {
                return rutaImagenArticulo;
            }

            return null;
        }

        //-------------------------------------------------------------- EVENTOS ------------------------------------------------------------------------------
        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            if (checkBox.ID == "UrlRadioButton") 
            {
                ImagenPorArchivo = false;
            }

            if (checkBox.ID == "SubirArchivoRadioButton") 
            {
                ImagenPorArchivo = true;
            }

            Session.Add("ImagenPorArchivo", ImagenPorArchivo);
        }

        protected void AñadirArticuloButton_ServerClick(object sender, EventArgs e)
        {
            Articulo articuloNuevo;
            ArticuloNegocio articuloNegocio;

            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                articuloNuevo = new Articulo();
                articuloNegocio = new ArticuloNegocio();

                VincularArticuloADatos(articuloNuevo);
                articuloNegocio.AñadirArticulo(articuloNuevo, out int idNuevo);
                articuloNuevo.Id = idNuevo;
                articuloNuevo.ImagenUrl = ObtenerImagenUrl(articuloNuevo.Id);
                articuloNegocio.ActualizarArticulo(articuloNuevo);

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoProductos.aspx?estado=1", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoProductos.aspx?estado=2");
            }
        }

        protected void EditarArticuloButton_ServerClick(object sender, EventArgs e)
        {
            Articulo articuloAEditar;
            ArticuloNegocio articuloNegocio;
            bool existeArticulo;

            if (!Page.IsValid) 
            {
                return;
            }

            if (!EsEdicion) 
            {
                return;
            }

            try
            {
                articuloNegocio = new ArticuloNegocio();
                int.TryParse(Request.QueryString["id"], out int idArticulo);
                articuloAEditar = articuloNegocio.ObtenerArticuloPorId(idArticulo);
                existeArticulo = articuloAEditar != null;
            
                if (!existeArticulo) 
                {
                    return;
                }

                VincularArticuloADatos(articuloAEditar);
                articuloAEditar.ImagenUrl = ObtenerImagenUrl(idArticulo);
                articuloNegocio.ActualizarArticulo(articuloAEditar);

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoProductos.aspx?estado=3", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoProductos.aspx?estado=4");
            }

        }

        protected void EliminarArticuloButton_ServerClick(object sender, EventArgs e)
        {
            if (!EsEdicion) 
            {
                return;
            }

            DebeConfirmarEliminacion = true;

            Session.Add("DebeConfirmarEliminacion", DebeConfirmarEliminacion);
        }

        protected void ConfirmarEliminacionButton_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio;
            bool existeArticulo;

            if (!Page.IsValid) 
            {
                return;
            }

            if (!EsEdicion)
            {
                return;
            }

            try
            {
                articuloNegocio = new ArticuloNegocio();
                int.TryParse(Request.QueryString["id"], out int idArticuloAEliminar);
                existeArticulo = articuloNegocio.ObtenerArticuloPorId(idArticuloAEliminar) != null;

                if (!existeArticulo)
                {
                    return;
                }

                articuloNegocio.EliminarArticulo(idArticuloAEliminar);

                Session.Add("VieneDeOperacion", true);
                Response.Redirect("ListadoProductos.aspx?estado=5", false);
            }
            catch (Exception)
            {
                Response.Redirect("ListadoProductos.aspx?estado=6");
            }
        }

        protected void CancelarEliminacionButton_Click(object sender, EventArgs e)
        {
            DebeConfirmarEliminacion = false;

            Session.Add("DebeConfirmarEliminacion", DebeConfirmarEliminacion);
        }

        protected void ProbarUrlButton_Click(object sender, EventArgs e)
        {
            NuevaImagen.ImageUrl = SetearRutaImagenActual(UrlImagenTextBox.Text);
        }

        //----------------------------------------------------------------- VALIDATORS ------------------------------------------------------------------------
        protected void DropDownListCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CustomValidator customValidator = (CustomValidator)source;

                if (customValidator.ID == "MarcasCustomValidator")
                {
                    args.IsValid = MarcasDropDownList.SelectedIndex != 0;
                }

                if (customValidator.ID == "CategoriasCustomValidator")
                {
                    args.IsValid = CategoriasDropDownList.SelectedIndex != 0;
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void RequiredCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (ImagenPorArchivo)
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = UrlImagenTextBox.Text != "";
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void SeleccionArchivoCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!ImagenPorArchivo)
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = ImagenLocalInput.PostedFile != null && ImagenLocalInput.PostedFile.FileName != "";
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void TipoArchivoCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (!ImagenPorArchivo)
                {
                    args.IsValid = true;
                    return;
                }

                args.IsValid = ImagenLocalInput.PostedFile.FileName.EndsWith(".jpg") || ImagenLocalInput.PostedFile.FileName.EndsWith(".png");
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void CodigoEliminarCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                if (EsEdicion)
                {
                    Articulo articuloEliminar;
                    ArticuloNegocio articuloNegocio = new ArticuloNegocio();
                    int.TryParse(Request.QueryString["id"], out int idEliminar);

                    articuloEliminar = articuloNegocio.ObtenerArticuloPorId(idEliminar);

                    args.IsValid = articuloEliminar.CodigoArticulo == CodigoEliminarTextBox.Text;
                }
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }
    }
}