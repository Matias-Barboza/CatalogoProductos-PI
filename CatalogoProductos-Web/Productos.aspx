<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CatalogoProductos_Web.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="min-height-container section-proyect">

        <asp:UpdatePanel ID="UpdatePanelProductos" ClientIDMode="Static" runat="server">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="RepeaterProductos" >

                    <ItemTemplate>
                        <a class="link-button-product" href='DetalleProducto.aspx?id=<%#Eval("Id") %>'>
                        <div class="one-product-container rounded-3 mb-4">
                            <div class="image-product-container border-image-product">
                                <asp:Image ImageUrl='<%#Eval("ImagenUrl").ToString() == "" ?
                                            CatalogoProductos_negocio.ArticuloNegocio.PLACEHOLDER_IMAGEN_ARTICULO : Eval("ImagenUrl").ToString() %>'
                                    CssClass="img-fluid img-proyect" AlternateText="Imagen del producto" runat="server"/>
                            </div>
                        </a>
                            <div class="info-product-container">
                                <span class="h5 badge-proyect"><%#Eval("Categoria.Descripcion")%>
                                    <button type="button" runat="server" onserverclick="FavoritoButton_ServerClick" id="FavoritoButton"
                                            class="btn btn-default btn-fav-proyect">
                                        <i id="FavIcon" class="bi bi-heart-fill fav-icon-product" runat="server"></i>
                                    </button>
                                </span>
                                <a class="link-button-product" href='DetalleProducto.aspx?id=<%#Eval("Id") %>'>
                                    <hr class="hr-product-proyect"/>
                                    <asp:Label ID="IdArticuloLabel" Visible="false" Text='<%#Eval("Id") %>' runat="server" />
                                    <h6 class="h7-product-proyect"><%#Eval("Marca.Descripcion")%></h6>
                                    <h6 class="h6-product-proyect"><%#Eval("Nombre")%></h6>
                                    <p class="p-product-proyect"><%#CatalogoProductos_negocio.ArticuloNegocio.PrecioFormateado(((decimal) Eval("Precio")))%></p>
                                </a>
                            </div>
                        </div>
                        
                    </ItemTemplate>
                </asp:Repeater>

                <%if (NoHayProductosCargados)
                    {%>
                <div class="jumbotron-proyect rounded-3 border shadow">
                    <div class="container">
                        <h2 class="display-5">LO SENTIMOS...</h2>
                        <p class="lead">Por el momento no hay productos cargados.</p>
                        <hr class="my-4" />
                        <a href="Default.aspx" class="btn btn-primary icon-link icon-link-hover arrow-container">Volver al home...
                            <i class="bi bi-arrow-right"></i>
                        </a>
                    </div>
                </div>
                <%}%>
            </ContentTemplate>
        </asp:UpdatePanel>

    </section>

</asp:Content>
