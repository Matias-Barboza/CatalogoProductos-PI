<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CatalogoProductos_Web.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="products-container">

        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <asp:Repeater runat="server" ID="RepeaterProductos">
                    <ItemTemplate>
                        <a class="link-button-product" href='DetalleProducto.aspx?id=<%#Eval("Id") %>'>
                        <div class="one-product-container rounded-3 mb-4">
                            <div class="image-product-container border-image-product">
                                <img src="<%#Eval("ImagenUrl").ToString() == "" ?
                                            "https://pngimg.com/uploads/box/box_PNG137.png" : Eval("ImagenUrl").ToString() %>"
                                    class="img-fluid img-proyect" alt="Imagen del producto"/>
                            </div>
                            <div class="info-product-container">
                                <span class="h5 badge-proyect"><%#Eval("Categoria.Descripcion")%>
                                    <button type="button" runat="server" onserverclick="FavoritoButton_ServerClick" id="FavoritoButton"
                                            class="btn btn-default">
                                        <i class="bi bi-heart-fill fav-icon-product"></i>
                                    </button>
                                </span>
                                <hr class="hr-product-proyect"/>
                                <h6 class="h7-product-proyect"><%#Eval("Marca.Descripcion")%></h6>
                                <h6 class="h6-product-proyect"><%#Eval("Nombre")%></h6>
                                <p class="p-product-proyect"><%#((decimal) Eval("Precio")).ToString("C")%></p>
                            </div>
                        </div>
                        </a>
                    </ItemTemplate>
                </asp:Repeater>

                <%if (NoHayProductosCargados)
                  {%>
                <div class="jumbotron-proyect rounded-3 border shadow">
                    <div class="container">
                        <h1 class="display-5">LO SENTIMOS...</h1>
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
