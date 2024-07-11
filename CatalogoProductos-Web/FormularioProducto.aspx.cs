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

        protected void Page_Load(object sender, EventArgs e)
        {
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

            CodigoArticuloTextBox.Attributes["placeholder"] = articuloACargar.CodigoArticulo;
            NombreArticuloTextBox.Attributes["placeholder"] = articuloACargar.Nombre;
            DescripcionArticuloTextBox.Attributes["placeholder"] = articuloACargar.Descripcion;
            MarcasDropDownList.Items.FindByText(articuloACargar.Marca.Descripcion).Selected = true;
            CategoriasDropDownList.Items.FindByText(articuloACargar.Categoria.Descripcion).Selected = true;
            PrecioArticuloTextBox.Attributes["placeholder"] = articuloACargar.Precio.ToString("C");
            UrlImagenTextBox.Attributes["placeholder"] = articuloACargar.ImagenUrl;

            ActualImagen.ImageUrl = articuloACargar.ImagenUrl;
        }

        protected void AñadirArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void EditarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void EliminarArticuloButton_ServerClick(object sender, EventArgs e)
        {

        }

        protected void ProbarUrlButton_Click(object sender, EventArgs e)
        {
            NuevaImagen.ImageUrl = UrlImagenTextBox.Text;
        }

        protected void DropDownListCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                CustomValidator customValidator = (CustomValidator) source;

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
    }
}