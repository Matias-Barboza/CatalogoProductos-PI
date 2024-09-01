<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisFavoritos.aspx.cs" Inherits="CatalogoProductos_Web.MisFavoritos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect min-height-container justify-content-start">

        <div class="container-title my-0">
            <h2 class="display-6">MIS FAVORITOS</h2>
        </div>

        <hr class="my-0" />

        <div class="container-some-products">

            <asp:Repeater ID="FavoritosRepeater" runat="server">
                <ItemTemplate>
                    <div class="card card-proyect card-fav">
                        <asp:Image ImageUrl='<%#Eval("ImagenUrl").ToString() == "" ?
                                    CatalogoProductos_negocio.ArticuloNegocio.PLACEHOLDER_IMAGEN_ARTICULO : Eval("ImagenUrl").ToString() %>'
                            CssClass="card-img-top img-card-proyect rounded-3" AlternateText="Imagen del producto" onerror="ErrorCargaImagenProducto(this)" runat="server" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <asp:Label ID="IdArticuloLabel" Text='<%#Eval("Id") %>' Visible="false" runat="server" />
                            <hr class="hr-product-proyect"/>
                            <span class="card-price-link-proyect">
                                <a href='DetalleProducto.aspx?id=<%#Eval("Id") %>' class="btn btn-primary icon-link icon-link-hover arrow-container">Detalles
                                    <i class="bi bi-arrow-right"></i>
                                </a>
                                <button type="button" runat="server" onserverclick="FavoritoButton_ServerClick" id="FavoritoButton"
                                        class="btn btn-default">
                                    <i id="FavIcon" class="bi bi-heart-fill nofav-icon-product-list" runat="server"></i>
                                </button>
                            </span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

            <%if (NoHayFavoritosGuardados)
              {%>
            <div class="jumbotron-proyect rounded-3 border shadow">
                <div class="container">
                    <h2 class="display-5">UPS!...</h2>
                    <p class="lead">Por el momento no tienes favoritos guardados.</p>
                    <hr class="my-4" />
                    <a href="Productos.aspx" class="btn btn-primary icon-link icon-link-hover arrow-container">Mirar productos...
                        <i class="bi bi-arrow-right"></i>
                    </a>
                </div>
            </div>
            <%}%>

        </div>

    </section>

</asp:Content>
