<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="CatalogoProductos_Web.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="rounded-3 shadow product-detail-div">

            <span class="badge-detail-proyect">
                <asp:Label runat="server" ID="TituloProductoLabel"/>
                <button type="button" runat="server" onserverclick="FavoritoButton_ServerClick" id="FavoritoButton"
                    class="btn btn-default" title="Marcar como favorito">
                    <i id="FavIcon" class="bi bi-heart-fill fav-icon-product" runat="server"></i>
                </button>
            </span>

            <div class="img-info-container">

                <div class="img-detail-container">
                    <asp:Image runat="server" CssClass="img-fluid" ID="ImagenProducto" AlternateText="Imagen del producto" />
                </div>

                <div class="info-detail-container">
                    <span class=" background-h4-details h4">Producto</span>
                    <asp:Label runat="server" ID="NombreLabel" CssClass="label-detail-proyect"/>
                    <span class="background-h4-details h4">Descripción</span>
                    <asp:Label runat="server" ID="DescripcionLabel" CssClass="label-detail-proyect"/>
                    <span class=" background-h4-details h4">Marca</span>
                    <asp:Label runat="server" ID="DescripcionMarcaLabel" CssClass="label-detail-proyect"/>
                    <span class=" background-h4-details h4">Categoría</span>
                    <asp:Label runat="server" ID="DescripcionCategoriaLabel" CssClass="label-detail-proyect"/>
                    <span class=" background-h4-details h4">Precio</span>
                    <asp:Label runat="server" ID="PrecioLabel" CssClass="label-detail-proyect"/>
                </div>

            </div>

        </div>

        <div class="container-title-others">
            <h2 class="display-6">OTROS PRODUCTOS</h2>
        </div>

        <hr class="hr-product-proyect"/>

        <div class="container-some-products">
            <asp:Repeater ID="RepeaterAlgunosProductos" runat="server">
                <ItemTemplate>
                    <div class="card card-proyect">
                        <asp:Image ImageUrl='<%#Eval("ImagenUrl").ToString() == "" ?
                            CatalogoProductos_negocio.ArticuloNegocio.PLACEHOLDER_IMAGEN_ARTICULO : Eval("ImagenUrl").ToString() %>'
                            CssClass="card-img-top img-card-proyect rounded-3" AlternateText="Imagen del producto" runat="server" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <hr class="hr-product-proyect" />
                            <p class="card-text"><%#Eval("Descripcion")%></p>
                            <span class="card-price-link-proyect">
                                <a href='DetalleProducto.aspx?id=<%#Eval("Id") %>' class="btn btn-primary icon-link icon-link-hover arrow-container">Detalles
                            <i class="bi bi-arrow-right"></i>
                                </a>
                                <p class="p-product-proyect p-card-product-proyect"><%#((decimal) Eval("Precio")).ToString("C3")%></p>
                            </span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

    </section>

</asp:Content>
