<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Administracion.aspx.cs" Inherits="CatalogoProductos_Web.Administracion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect min-height-container justify-content-start align-items-center">

        <div class="jumbotron-proyect rounded-3 border shadow width-adj">

            <h2 class="text-center mb-0">
                Administración
                <i class="bi bi-gear"></i>
            </h2>

            <hr />

            <div class="d-flex flex-column">
                <a class="btn btn-primary btn-lg mt-3" title="Listado Productos"
                    href="ListadoProductos.aspx" >
                    Listado Productos
                    <i class="bi bi-list-ol"></i>
                </a>
                <a class="btn btn-primary btn-lg mt-3" title="Listado Marcas"
                    href="ListadoMarcas.aspx">
                    Listado Marcas
                    <i class="bi bi-list-ol"></i>
                </a>
                <a class="btn btn-primary btn-lg mt-3" title="Listado Categorias"
                    href="ListadoCategorias.aspx">
                    Listado Categorias
                    <i class="bi bi-list-ol"></i>
                </a>
            </div>
        </div>

        <div class="jumbotron-proyect rounded-3 border shadow width-adj">

            <h2 class="text-center mb-0">
                Acceso rápido
                <i class="bi bi-arrow-right-circle"></i>
            </h2>

            <hr />

            <div class="d-flex flex-column">
                <a class="btn btn-primary btn-lg mt-3" title="Añadir Producto"
                    href="FormularioProducto.aspx" >
                    Añadir Producto
                    <i class="bi bi-box-seam"></i>
                </a>
                <a class="btn btn-primary btn-lg mt-3" title="Añadir Marca"
                    href="FormularioMarca.aspx">
                    Añadir Marca
                    <i class="bi bi-node-plus"></i>
                </a>
                <a class="btn btn-primary btn-lg mt-3" title="Añadir Categoría"
                    href="FormularioCategoria.aspx">
                    Añadir Categoría
                    <i class="bi bi-node-plus"></i>
                </a>
            </div>
        </div>

    </section>

</asp:Content>
