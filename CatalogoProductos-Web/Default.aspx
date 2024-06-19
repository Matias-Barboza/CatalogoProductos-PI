<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CatalogoProductos_Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="jumbotron-proyect rounded-3 border shadow">
            <div class="container">
                <h1 class="display-5">¡BIENVENIDO AL CATÁLOGO DIGITAL!</h1>
                <p class="lead">Visualiza nuestros productos con el botón a continuación.</p>
                <hr class="my-4" />
                <a href="#" class="btn btn-primary icon-link icon-link-hover arrow-container">
                    Echar un vistazo...
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
        <div class="container-some-products">
            <%for (int i = 0; i < 6; i++)
                {%>
            <div class="card card-proyect">
                <img src="..." class="card-img-top" alt="...">
                <div class="card-body">
                    <h5 class="card-title">Card title</h5>
                    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
                    <a href="#" class="btn btn-primary icon-link icon-link-hover arrow-container">
                        Go somewhere
                        <i class="bi bi-arrow-right"></i>
                    </a>
                </div>
            </div>
            <%}%>
        </div>

    </section>

</asp:Content>
