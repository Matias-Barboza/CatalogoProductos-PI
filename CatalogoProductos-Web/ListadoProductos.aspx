<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="CatalogoProductos_Web.ListadoProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <h2 class="text-center">Listado de productos</h2>
        
        <hr />

        <asp:GridView ID="ProductosGridView" AutoGenerateColumns="false" CssClass="table table-striped" runat="server"
            DataKeyNames="Id" OnRowCommand="ProductosGridView_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" Visible="false" />
                <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" ItemStyle-VerticalAlign="Middle"/>
                <asp:BoundField DataField="CodigoArticulo" HeaderText="Código" ItemStyle-VerticalAlign="Middle"/>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" ItemStyle-VerticalAlign="Middle"/>
                <asp:BoundField DataField="Marca.Descripcion" HeaderText="Marca" ItemStyle-VerticalAlign="Middle"/>
                <asp:BoundField DataField="Precio" HeaderText="Precio" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C3}"/>
                <asp:ButtonField ButtonType="Link" HeaderText="Acción" AccessibleHeaderText="Acción"
                    ItemStyle-CssClass="has-icon" Text="&#xF4CA;" ItemStyle-HorizontalAlign="center" CommandName="EditarProducto" />
            </Columns>
        </asp:GridView>

    </section>

</asp:Content>
