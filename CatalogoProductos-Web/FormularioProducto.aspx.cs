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
        public bool ImagenPorArchivo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpPostedFile pf = ImagenLocalInput.PostedFile;

            //Si viene vacio debe navegar a pagina de error
            if (!IsPostBack) 
            {
                CargarDesplegables();
            }

            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int id)) 
            {
                EsEdicion = true;
                CargarDatosDelProducto(id);
            }

            if (Session["ImagenPorArchivo"] != null) 
            {
                ImagenPorArchivo = (bool)Session["ImagenPorArchivo"];
            }
        }

        public void CargarDesplegables() 
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            List<Marca> fuenteMarcas = marcaNegocio.ObtenerMarcas();
            List<Categoria> fuenteCategorias = categoriaNegocio.ObtenerCategorias();

            VincularADataSource(MarcasDropDownList, fuenteMarcas.ToList<object>());
            VincularADataSource(CategoriasDropDownList, fuenteCategorias.ToList<object>());
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
            PrecioArticuloTextBox.Text = articuloACargar.Precio.ToString("C");
            UrlImagenTextBox.Text = articuloACargar.ImagenUrl;

            ActualImagen.ImageUrl = articuloACargar.ImagenUrl;
        }

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

        protected void ProbarUrlButton_Click(object sender, EventArgs e)
        {
            NuevaImagen.ImageUrl = UrlImagenTextBox.Text;
        }

        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;

            if (!checkBox.Checked) 
            {
                return;
            }

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

        protected void RequiredCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = UrlImagenTextBox.Text != "" && !ImagenPorArchivo;
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
                args.IsValid = ImagenLocalInput.PostedFile != null;
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
                args.IsValid = ImagenLocalInput.PostedFile.FileName.EndsWith(".jpg") || ImagenLocalInput.PostedFile.FileName.EndsWith(".png");
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void AñadirArticuloButton_ServerClick(object sender, EventArgs e)
        {
            Articulo articuloNuevo;
            ArticuloNegocio articuloNegocio;

            if (!Page.IsValid)
            {
                return;
            }

            articuloNuevo = new Articulo();
            articuloNegocio = new ArticuloNegocio();

            articuloNuevo = VincularArticuloADatos(articuloNuevo);
            articuloNegocio.AñadirArticulo(articuloNuevo);
        }

        protected void EditarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void EliminarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        public Articulo VincularArticuloADatos(Articulo articulo) 
        {
            articulo.CodigoArticulo = CodigoArticuloTextBox.Text;
            articulo.Nombre = NombreArticuloTextBox.Text;
            articulo.Descripcion = DescripcionArticuloTextBox.Text;
            articulo.Marca.Id = Convert.ToInt32(MarcasDropDownList.SelectedValue);
            articulo.Categoria.Id = Convert.ToInt32(CategoriasDropDownList.SelectedValue);
            //articulo.ImagenUrl = ObtenerImagenUrl(articulo.Id);
            articulo.Precio = Convert.ToDecimal(PrecioArticuloTextBox.Text);

            return articulo;
        }

        public string ObtenerImagenUrl(int idArticulo) 
        {
            string ruta = null;

            if (ImagenPorArchivo) 
            {
                string rutaACarpetaImagenes = Server.MapPath("./Imagenes/");
                ruta = $"{rutaACarpetaImagenes}imagenArticulo{idArticulo}.jpg";

                ImagenLocalInput.PostedFile.SaveAs(ruta);
            }
            else 
            {
                ruta = UrlImagenTextBox.Text;
            }

            return ruta;
        }

        protected void ProbarImagenButton_Click(object sender, EventArgs e)
        {
            if (ImagenLocalInput.PostedFile != null) 
            {
                NuevaImagen.ImageUrl = ImagenLocalInput.PostedFile.FileName;
            }
        }
    }
}