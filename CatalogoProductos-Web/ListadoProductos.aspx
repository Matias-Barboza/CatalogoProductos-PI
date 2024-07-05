<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="CatalogoProductos_Web.ListadoProductos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <h2 class="text-center">Listado de productos</h2>
        
        <hr />

        <asp:GridView ID="ProductosGridView" AutoGenerateColumns="false" CssClass="table table-striped" runat="server">
            <Columns>
                <asp:BoundField DataField="CodigoArticulo" HeaderText="Código"/>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre"/>
                <asp:BoundField DataField="Marca.Descripcion" HeaderText="Marca"/>
                <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría"/>
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C3}"/>
                <asp:ButtonField ButtonType="Button" HeaderText="Acción" AccessibleHeaderText="Acción" ItemStyle-CssClass="bi bi-pencil-square btn-default" ItemStyle-HorizontalAlign="Center" />
            </Columns>
        </asp:GridView>

    </section>

</asp:Content>
