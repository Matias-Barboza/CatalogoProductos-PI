﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="CatalogoProductos_Web.ListadoProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect min-height-container justify-content-start">

        <%if (VieneDeOperacion)
          {%>
        <div class="mt-2 mb-2">
            <asp:Label ID="MensajeOperacionLabel" runat="server" />
        </div>
        <%} %>

        <%if (NoHayProductosCargados)
            {%>
        <div class="jumbotron-proyect jumbotron-no-list-grid rounded-3 border shadow">
            <div class="container">
                <h2 class="display-5">NO SE ENCONTRARON PRODUCTOS CARGADOS</h2>
                <p class="lead">Puede agregarlos en cualquier momento.</p>
                <hr class="my-4" />
                <a href="FormularioProducto.aspx" class="btn btn-primary icon-link icon-link-hover arrow-container">Agregar producto ahora...
                    <i class="bi bi-arrow-right"></i>
                </a>
            </div>
        </div>
        <%}
        else
        {%>
        <div class="jumbotron-proyect jumbotron-list-grid rounded-3 border-shadow">

            <div class="position-relative title-addoperation-container">

                <h2 class="text-center mb-0">
                    Listado de productos
                    <i class="bi bi-clipboard"></i>
                </h2>

                <a class="btn btn-primary btn-lg btn-back-adm" title="Volver a Administración"
                    href="Administracion.aspx" >
                    <i class="bi bi-arrow-left"></i>
                </a>

                <a class="btn btn-primary btn-lg btn-add-operation" title="Añadir Producto"
                    href="FormularioProducto.aspx" >
                    Añadir Producto
                    <i class="bi bi-box-seam"></i>
                </a>

            </div>

            <hr />

            <asp:UpdatePanel ID="UpdatePanelPreGridView" ClientIDMode="Static" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="ProductosGridView" AutoGenerateColumns="false" CssClass="table table-striped" runat="server"
                        DataKeyNames="Id" AllowPaging="true" PageSize="10" PagerStyle-CssClass="pagination-proyect" PagerStyle-HorizontalAlign="Center"
                        OnPageIndexChanging="ProductosGridView_PageIndexChanging" OnRowCommand="ProductosGridView_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="#" ItemStyle-Font-Bold="true" HeaderStyle-CssClass="table-dark">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Id" Visible="false" />
                            <asp:BoundField DataField="Categoria.Descripcion" HeaderText="Categoría" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="CodigoArticulo" HeaderText="Código" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Nombre" HeaderText="Nombre" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Marca.Descripcion" HeaderText="Marca" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" />
                            <asp:BoundField DataField="Precio" HeaderText="Precio" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C}" />
                            <asp:ButtonField ButtonType="Link" HeaderText="Acción" HeaderStyle-CssClass="table-dark" AccessibleHeaderText="Acción"
                                ItemStyle-CssClass="has-icon" Text="&#xF4CA;" ItemStyle-HorizontalAlign="center" CommandName="EditarProducto" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
        <%}%>

    </section>

</asp:Content>
