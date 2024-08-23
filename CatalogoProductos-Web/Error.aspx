<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="CatalogoProductos_Web.Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="products-container">

        <div class="jumbotron-proyect rounded-3 border shadow">
            <div class="container">
                <h2 class="display-5">UPS!</h2>
                <p id="MensajeErrorParagraph" class="lead" runat="server"></p>
                <hr class="my-4" />
                <a id="LinkPostError" class="btn btn-primary icon-link icon-link-hover arrow-container" runat="server">
                    <asp:Label ID="MensajeLinkPostErrorLabel" runat="server"></asp:Label>
                    <i class="bi bi-arrow-right"></i>
                </a>
            </div>
        </div>

    </section>

</asp:Content>
