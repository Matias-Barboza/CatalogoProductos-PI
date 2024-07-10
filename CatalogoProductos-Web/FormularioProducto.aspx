<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="CatalogoProductos_Web.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">


        <div class="current-data-container rounded-3 border shadow">
            
            <h2 class="text-center">Formulario de Producto</h2>

            <hr />

            <%-- Código de articulo --%>
            <div class="input-group mb-3">
                <span class="input-group-text" >Código de artículo</span>
                <asp:TextBox ID="CodigoArticuloTextBox" CssClass="form-control" placeholder="Ej: S23" runat="server"></asp:TextBox>
            </div>

            <%-- Nombre de articulo --%>
            <div class="input-group mb-3">
                <span class="input-group-text" >Nombre de artículo</span>
                <asp:TextBox ID="NombreArticuloTextBox" CssClass="form-control" placeholder="Ej: Galaxy S23" runat="server"></asp:TextBox>
            </div>

            <%-- Descripción --%>
            <div class="input-group mb-3">
                <span class="input-group-text" >Descripción de artículo</span>
                <asp:TextBox ID="DescripcionArticuloTextBox" CssClass="form-control" TextMode="MultiLine"
                             MaxLength="150" placeholder="Ej: Celular última generación..." runat="server"></asp:TextBox>
            </div>

            <%-- Marca y Categoría --%>
            <div class="brand-category-container mb-3">

                <%-- Marca --%>
                <div class="input-group me-3">
                    <label class="input-group-text">Marca</label>
                    <asp:DropDownList ID="MarcasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>

                <%-- Categoría --%>
                <div class="input-group">
                    <label class="input-group-text">Categoría</label>
                    <asp:DropDownList ID="CategoriasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>

            </div>

            <%-- Precio --%>
            <div class="input-group mb-3">
                <span class="input-group-text" >Precio de artículo</span>
                <span class="input-group-text">$</span>
                <asp:TextBox ID="PrecioArticuloTextBox" CssClass="form-control" placeholder="Ej: 150.000,00" runat="server"></asp:TextBox>
            </div>

            <div class="mb-3">
                <%-- URL imagen --%>
                <div class="input-group mb-1">
                    <span class="input-group-text" >URL de imagen del artículo</span>
                    <asp:TextBox ID="UrlImagenTextBox" CssClass="form-control" TextMode="Url"
                                 placeholder="Ej: https://example.com" runat="server"></asp:TextBox>
                    <asp:Button ID="ProbarUrlButton" Text="Probar URL" OnClick="ProbarUrlButton_Click"
                                CausesValidation="true" ValidationGroup="ProbarUrlValidation"
                                CssClass="btn btn-outline-primary" runat="server" />
                </div>

                <div class="ms-2">
                    <asp:RequiredFieldValidator ErrorMessage="La URL no puede estar vacía." CssClass="validator"
                                                Display="Dynamic" ValidationGroup="ProbarUrlValidation" 
                                                ControlToValidate="UrlImagenTextBox" runat="server" />
                </div>

            </div>

            <h3>Previsualización de imagen</h3>

            <hr />

            <%-- Renderizado de imagen --%>
            <div class="imgs-form-container mb-3">

                <%if (EsEdicion)
                  { %>
                <div class="img-form-container rounded-3 border shadow">
                    <label class="form-label h4">Imagen actual</label>
                    <hr class="hr-product-proyect" />
                    <asp:Image ID="ActualImage" ImageUrl="https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1"
                                CssClass="img-fluid" runat="server" AlternateText="Imagen actual del artículo" />
                </div>
                <%} %>

                <div class="img-form-container rounded-3 border shadow">
                    <label class="form-label h4">Imagen nueva</label>
                    <hr class="hr-product-proyect" />
                    <asp:Image ID="NuevaImagen" ImageUrl="https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1"
                                CssClass="img-fluid" runat="server" AlternateText="Imagen nueva del artículo" />
                </div>

            </div>

            <div class="buttons-container pt-3 mb-3">

                <%if (!EsEdicion)
                    {%>
                <button type="button" runat="server" onserverclick="AñadirArticuloButton_ServerClick" id="AñadirArticuloButton"
                        class="btn btn-success pt-2 pb-2 ms-2 me-2">Añadir artículo
                    <i class="bi bi-plus-circle"></i>
                </button>
                <%}%>
                <%else
                  {%>
                <button type="button" runat="server" onserverclick="EditarArticuloButton_ServerClick" id="EditarArticuloButton"
                        class="btn btn-warning pt-2 pb-2 ms-2 me-2">Editar artículo
                    <i class="bi bi-pencil-square"></i>
                </button>
                <button type="button" runat="server" onserverclick="EliminarArticuloButton_ServerClick" id="EliminarArticuloButton"
                        class="btn btn-danger pt-2 pb-2 ms-2 me-2">Eliminar artículo
                    <i class="bi bi-trash3"></i>
                </button>
                <%}%>
            </div>

        </div>

    </section>

</asp:Content>
