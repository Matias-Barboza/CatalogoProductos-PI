<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoProductos.aspx.cs" Inherits="CatalogoProductos_Web.ListadoProductos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="jumbotron-proyect rounded-3 border shadow">

            <h2 class="text-center mb-0">
                Operaciones de administración
                <i class="bi bi-gear"></i>
            </h2>

            <hr />

            <div class="d-flex justify-content-evenly">
                <a class="btn btn-success btn-lg" title="Añadir Producto" disabled="true"
                    href="FormularioProducto.aspx" >
                    Añadir Producto
                    <i class="bi bi-box-seam"></i>
                </a>
                <a class="btn btn-success btn-lg" title="Añadir Marca" disabled="true">
                    Añadir Marca
                    <i class="bi bi-node-plus"></i>
                </a>
                <a class="btn btn-success btn-lg" title="Añadir Categoría" disabled="true">
                    Añadir Categoría
                    <i class="bi bi-node-plus"></i>
                </a>
            </div>
        </div>

        <div class="jumbotron-proyect rounded-3 border-shadow" style="height: 477px;">

            <h2 class="text-center mb-0">
                Listado de productos
                <i class="bi bi-clipboard"></i>
            </h2>

            <hr />

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="ProductosGridView" AutoGenerateColumns="false" CssClass="table table-striped" runat="server"
                        DataKeyNames="Id" AllowPaging="true" PageSize="5" PagerStyle-CssClass="pagination-proyect" PagerStyle-HorizontalAlign="Center" 
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
                            <asp:BoundField DataField="Precio" HeaderText="Precio" HeaderStyle-CssClass="table-dark" ItemStyle-VerticalAlign="Middle" DataFormatString="{0:C3}" />
                            <asp:ButtonField ButtonType="Link" HeaderText="Acción" HeaderStyle-CssClass="table-dark" AccessibleHeaderText="Acción"
                                ItemStyle-CssClass="has-icon" Text="&#xF4CA;" ItemStyle-HorizontalAlign="center" CommandName="EditarProducto" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </section>

</asp:Content>
