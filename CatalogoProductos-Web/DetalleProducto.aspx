<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="CatalogoProductos_Web.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="products-container rounded-3 shadow-proyect product-detail-div">
            <div class="img-detail-container">
                <asp:Image runat="server" CssClass="img-fluid" ID="ImagenProducto"/>
            </div>

            <div class="info-detail-container">
                <h4>Producto</h4>
                <asp:Label runat="server" ID="NombreLabel"/>
                <h4>Descripción</h4>
                <asp:Label runat="server" ID="DescripcionLabel"/>
                <h4>Marca</h4>
                <asp:Label runat="server" ID="DescripcionMarcaLabel"/>
                <h4>Categoría</h4>
                <asp:Label runat="server" ID="DescripcionCategoriaLabel"/>
                <h4>Precio</h4>
                <asp:Label runat="server" ID="PrecioLabel"/>
            </div>
        </div>

    </section>

</asp:Content>
