<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="CatalogoProductos_Web.FormularioProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="current-data-container rounded-3 border shadow">
            
            <h2 class="text-center">Formulario de Producto</h2>

            <hr />

            <%-- Código de articulo --%>
            <div class="mb-3">
                
                <div class="input-group mb-1">
                    <span class="input-group-text">Código de artículo<span class="required-data"> *</span></span>
                    <asp:TextBox ID="CodigoArticuloTextBox" CssClass="form-control" placeholder="Ej: S23" runat="server"></asp:TextBox>
                </div>

                <div class="ms-2">
                    <asp:RegularExpressionValidator ErrorMessage="El código de artículo debe tener 3 caracteres como mínimo y 50 como máximo." Display="Dynamic"
                                        ControlToValidate="CodigoArticuloTextBox" CssClass="validator" ValidationExpression="^[a-zA-Z0-9]{3,50}$"
                                        ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="El código de artículo debe contener al menos una (1) letra y dos (2) números." Display="Dynamic"
                                        ControlToValidate="CodigoArticuloTextBox" CssClass="validator" ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d.*\d)[a-zA-Z0-9]{3,50}$"
                                        ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RequiredFieldValidator ErrorMessage="El código de artículo es obligatorio." Display="Dynamic"
                                                ControlToValidate="CodigoArticuloTextBox" CssClass="validator"
                                                ValidationGroup="OperationValidationGroup" runat="server" />
                </div>

            </div>

            <%-- Nombre de articulo --%>
            <div class="mb-3">
                
                <div class="input-group mb-1">
                    <span class="input-group-text">Nombre de artículo<span class="required-data"> *</span></span>
                    <asp:TextBox ID="NombreArticuloTextBox" CssClass="form-control" placeholder="Ej: Galaxy S23" runat="server"></asp:TextBox>
                </div>

                <div class="ms-2">
                    <asp:RequiredFieldValidator ErrorMessage="El nombre de artículo es obligatorio." Display="Dynamic"
                            ControlToValidate="NombreArticuloTextBox" CssClass="validator" runat="server"
                            ValidationGroup="OperationValidationGroup"/>
                </div>

            </div>

            <%-- Descripción --%>
            <div class="mb-3">
                
                <div class="input-group mb-1">
                    <span class="input-group-text">Descripción de artículo</span>
                    <asp:TextBox ID="DescripcionArticuloTextBox" CssClass="form-control" TextMode="MultiLine"
                                 MaxLength="150" placeholder="Ej: Celular última generación..." runat="server"></asp:TextBox>
                </div>

                <div class="ms-2">
                    <p class="form-text">La descripción no es un campo obligatorio mas sí recomendable.</p>
                </div>

            </div>

            <%-- Marca y Categoría --%>
            <div class="brand-category-container mb-3">

                <%-- Marca --%>
                <div class="w-100 me-3">

                    <div class="input-group mb-1">
                        <label class="input-group-text">Marca<span class="required-data"> *</span></label>
                        <asp:DropDownList ID="MarcasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <asp:CustomValidator ID="MarcasCustomValidator" ErrorMessage="La selección de una marca es obligatoria."
                                         Display="Dynamic" ControlToValidate="MarcasDropDownList" CssClass="validator ms-2"
                                         OnServerValidate="DropDownListCustomValidator_ServerValidate"
                                         ValidationGroup="OperationValidationGroup" runat="server" />

                </div>

                <%-- Categoría --%>
                <div class="w-100">

                    <div class="input-group mb-1">
                        <label class="input-group-text">Categoría<span class="required-data"> *</span></label>
                        <asp:DropDownList ID="CategoriasDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <asp:CustomValidator ID="CategoriasCustomValidator" ErrorMessage="La selección de una categoría es obligatoria."
                                         Display="Dynamic" ControlToValidate="CategoriasDropDownList" CssClass="validator ms-2"
                                         OnServerValidate="DropDownListCustomValidator_ServerValidate"
                                         ValidationGroup="OperationValidationGroup" runat="server" />

                </div>

            </div>

            <%-- Precio --%>
            <div class="mb-3">

                <div class="input-group mb-1">
                    <span class="input-group-text">Precio de artículo<span class="required-data">*</span></span>
                    <span class="input-group-text">$</span>
                    <asp:TextBox ID="PrecioArticuloTextBox" CssClass="form-control" placeholder="Ej: 150.000,00; 100000" runat="server"></asp:TextBox>
                </div>

                <div class="ms-2">
                    <asp:RequiredFieldValidator ErrorMessage="El precio del artículo es obligatorio." Display="Dynamic"
                                                ControlToValidate="PrecioArticuloTextBox" CssClass="validator"
                                                ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RegularExpressionValidator ErrorMessage="El formato debe ser como: '100', '1000', '1.000', '1500,25'."
                                                    Display="Dynamic" ValidationExpression="^(?:\d{1,3}(?:\.\d{3})*|\d+)(?:,\d{1,2})?$"
                                                    ControlToValidate="PrecioArticuloTextBox" CssClass="validator"
                                                    ValidationGroup="OperationValidationGroup" runat="server" />
                </div>

            </div>

            <h3>Carga de imagen del artículo</h3>

            <hr />

            <div class="mb-3">
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>

                        <%-- Opcionalidad --%>
                        <div class="radio-buttons-container mb-3">
                            <div class="form-check">
                              <asp:RadioButton ID="UrlRadioButton" CssClass="span-checkbox-container" AutoPostBack="true"
                                               GroupName="OpcionesSubirImagen" runat="server" OnCheckedChanged="RadioButton_CheckedChanged" />
                              <label class="form-check-label me-1" for="flexRadioDefault1">
                                Por URL
                              </label>
                            </div>
                            <div class="form-check">
                              <asp:RadioButton ID="SubirArchivoRadioButton" CssClass="span-checkbox-container" AutoPostBack="true"
                                               GroupName="OpcionesSubirImagen" runat="server" OnCheckedChanged="RadioButton_CheckedChanged" />
                              <label class="form-check-label me-1" for="flexRadioDefault1">
                                Subir archivo
                              </label>
                            </div>
                        </div>

                        <%-- URL imagen --%>

                        <%if (!ImagenPorArchivo)
                            {%>
                        <div class="input-group mb-1">
                            <span class="input-group-text">URL de imagen del artículo<span class="required-data">*</span></span>
                            <asp:TextBox ID="UrlImagenTextBox" CssClass="form-control" TextMode="Url"
                                            placeholder="Ej: https://example.com" runat="server"></asp:TextBox>
                            <asp:Button ID="ProbarUrlButton" Text="Probar URL" OnClick="ProbarUrlButton_Click"
                                        CausesValidation="true"
                                        CssClass="btn btn-outline-primary" runat="server" />
                        </div>
                        
                        <div class="ms-2 mb-3">
                            <asp:RequiredFieldValidator ErrorMessage="La URL no puede estar vacía para probar." Display="Dynamic"
                                                        ValidationGroup="ProbarUrlValidation" ControlToValidate="UrlImagenTextBox"
                                                        CssClass="validator" runat="server" />
                            <asp:CustomValidator ID="RequiredCustomValidator" ErrorMessage="La URL de la imagen del artículo es obligatoria."
                                                 Display="Dynamic" ControlToValidate="UrlImagenTextBox" CssClass="validator"
                                                 OnServerValidate="RequiredCustomValidator_ServerValidate"
                                                 ValidationGroup="OperationValidationGroup" runat="server" />
                        </div>
                        <%}
                          else
                          {%>
                        <%-- Imagen local --%>
                        <div class="input-group mb-1">
                            <label class="input-group-text" for="inputGroupFile02">Imagen local<span class="required-data">*</span></label>
                            <input type="file" class="form-control" id="ImagenLocalInput" accept=".jpg,.png" runat="server"/>
                            <asp:Button ID="ProbarImagenButton" Text="Previsualizar imagen" CssClass="btn btn-outline-primary" OnClick="ProbarImagenButton_Click"
                                        causesvalidation="true" validationgroup="ProbarValidationGroup" runat="server" />
                        </div>

                        <div class="ms-2">
                            <asp:CustomValidator ID="SeleccionArchivoCustomValidator" ErrorMessage="La selección de archivo no puede estar vacío."
                                                 Display="Dynamic" ControlToValidate="ImagenLocalInput" ValidateEmptyText="true"
                                                 OnServerValidate="SeleccionArchivoCustomValidator_ServerValidate" CssClass="validator"
                                                 ValidationGroup="OperationValidationGroup" runat="server" />
                            <asp:CustomValidator ID="TipoArchivoCustomValidator" ErrorMessage="Los tipos de archivos aceptado son: '.jpg, .png'."
                                                 Display="Dynamic" ControlToValidate="ImagenLocalInput"
                                                 OnServerValidate="TipoArchivoCustomValidator_ServerValidate" CssClass="validator"
                                                 ValidationGroup="OperationValidationGroup" runat="server" />
                            <asp:CustomValidator ID="CustomValidator1" ErrorMessage="La selección de archivo no puede estar vacío para probar."
                                                 Display="Dynamic" ControlToValidate="ImagenLocalInput" ValidateEmptyText="true"
                                                 OnServerValidate="SeleccionArchivoCustomValidator_ServerValidate" CssClass="validator"
                                                 ValidationGroup="ProbarValidationGroup" runat="server" />
                            <asp:CustomValidator ID="CustomValidator2" ErrorMessage="Los tipos de archivos aceptado son: '.jpg, .png'."
                                                 Display="Dynamic" ControlToValidate="ImagenLocalInput"
                                                 OnServerValidate="TipoArchivoCustomValidator_ServerValidate" CssClass="validator"
                                                 ValidationGroup="ProbarValidationGroup" runat="server" />
                        </div>
                        <%} %>

                    </ContentTemplate>
                </asp:UpdatePanel>
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
                    <div class="img-container">
                        <asp:Image ID="ActualImagen" ImageUrl="https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1"
                            CssClass="img-fluid" runat="server" AlternateText="No se pudo establecer la ruta de imagen del artículo" />
                    </div>
                </div>
                <%} %>

                <div class="img-form-container rounded-3 border shadow">
                    <label class="form-label h4">Imagen nueva</label>
                    <hr class="hr-product-proyect" />
                    <div class="img-container">
                        <asp:Image ID="NuevaImagen" ImageUrl="https://i0.wp.com/casagres.com.ar/wp-content/uploads/2022/09/placeholder.png?ssl=1"
                                    CssClass="img-fluid" runat="server" AlternateText="No se pudo establecer la ruta de imagen del artículo" />
                    </div>
                </div>

            </div>

            <div class="buttons-container pt-3 mb-3">

                <%if (!EsEdicion)
                    {%>
                <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                        runat="server" onserverclick="AñadirArticuloButton_ServerClick" id="AñadirArticuloButton"
                        class="btn btn-success pt-2 pb-2 ms-2 me-2">Añadir artículo
                    <i class="bi bi-plus-circle"></i>
                </button>
                <%}%>
                <%else
                  {%>
                <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                        runat="server" onserverclick="EditarArticuloButton_ServerClick" id="EditarArticuloButton"
                        class="btn btn-warning pt-2 pb-2 ms-2 me-2">Editar artículo
                    <i class="bi bi-pencil-square"></i>
                </button>
                <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                        runat="server" onserverclick="EliminarArticuloButton_ServerClick" id="EliminarArticuloButton"
                        class="btn btn-danger pt-2 pb-2 ms-2 me-2">Eliminar artículo
                    <i class="bi bi-trash3"></i>
                </button>
                <%}%>
            </div>

        </div>

    </section>

</asp:Content>
