<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoProductos_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="jumbotron-proyect rounded-3 border shadow">
            <div class="container">
                <h1 class="display-5">¡BIENVENIDO AL CATÁLOGO DIGITAL!</h1>
                <p class="lead">Visualizá nuestros productos con el botón a continuación.</p>
                <hr class="my-4" />
                <a href="Productos.aspx" class="btn btn-primary icon-link icon-link-hover arrow-container">Echar un vistazo...
                    <i class="bi bi-arrow-right"></i>
                </a>
            </div>
        </div>

    </section>

    <hr />

    <section class="section-proyect">

        <div class="container-title">
            <h2 class="display-6">ALGUNOS PRODUCTOS</h2>
        </div>

        <div class="jumbotron-products-proyect rounded-3 border shadow">
            <span class="jumbotron-products-icon icon-tv"><i class="bi bi-tv icon-link"></i></span>
            <span class="jumbotron-products-icon"><i class="bi bi-pc-display"></i></span>
            <span class="jumbotron-products-icon"><i class="bi bi-headset"></i></span>
            <span class="jumbotron-products-icon icon-phone"><i class="bi bi-phone"></i></span>
        </div>

        <div class="container-some-products">
            <asp:Repeater runat="server" ID="RepeaterAlgunosProductos">
                <ItemTemplate>
                    <div class="card card-proyect">
                        <asp:Image ImageUrl='<%#Eval("ImagenUrl").ToString() == "" ?
                                    "https://pngimg.com/uploads/box/box_PNG137.png" : Eval("ImagenUrl").ToString() %>'
                            CssClass="card-img-top img-card-proyect rounded-3" AlternateText="Imagen del producto" runat="server" />
                        <div class="card-body">
                            <h5 class="card-title"><%#Eval("Nombre")%></h5>
                            <hr class="hr-product-proyect"/>
                            <p class="card-text"><%#Eval("Descripcion")%></p>
                            <span class="card-price-link-proyect">
                                <a href='DetalleProducto.aspx?id=<%#Eval("Id") %>' class="btn btn-primary icon-link icon-link-hover arrow-container">Detalles
                                    <i class="bi bi-arrow-right"></i>
                                </a>
                                <p class="p-product-proyect p-card-product-proyect"><%#((decimal) Eval("Precio")).ToString("C")%></p>
                            </span>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <%if (NoHayProductosCargados)
          {%>
        <div class="jumbotron-proyect rounded-3 border shadow">
            <div class="container">
                <h1 class="display-5">LO SENTIMOS...</h1>
                <p class="lead">Por el momento no hay productos cargados.</p>
            </div>
        </div>
        <%}%>
    </section>

</asp:Content>
