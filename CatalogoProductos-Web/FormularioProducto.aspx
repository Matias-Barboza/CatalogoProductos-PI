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
                    <asp:RegularExpressionValidator ID="CodigoLongitudValidator" ErrorMessage="El código de artículo debe tener 3 caracteres como mínimo y 50 como máximo." Display="Dynamic"
                                        ControlToValidate="CodigoArticuloTextBox" CssClass="validator" ValidationExpression="^[a-zA-Z0-9]{3,50}$"
                                        ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RegularExpressionValidator ID="CodigoFormatoValidator" ErrorMessage="El código de artículo debe contener al menos una (1) letra y dos (2) números." Display="Dynamic"
                                        ControlToValidate="CodigoArticuloTextBox" CssClass="validator" ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d.*\d)[a-zA-Z0-9]{3,50}$"
                                        ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RequiredFieldValidator ID="CodigoObligatorioValidator" ErrorMessage="El código de artículo es obligatorio." Display="Dynamic"
                                                ControlToValidate="CodigoArticuloTextBox" CssClass="validator"
                                                ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:CustomValidator ID="CodigoRepetidoCustomValidator" ErrorMessage="El código de producto ya existe." Display="Dynamic"
                                         ControlToValidate="CodigoArticuloTextBox" CssClass="validator"
                                         ValidationGroup="OperationValidationGroup" OnServerValidate="CodigoRepetidoCustomValidator_ServerValidate" runat="server" />
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
                              <label class="form-check-label me-1">
                                Por URL
                              </label>
                            </div>
                            <div class="form-check">
                              <asp:RadioButton ID="SubirArchivoRadioButton" CssClass="span-checkbox-container" AutoPostBack="true"
                                               GroupName="OpcionesSubirImagen" runat="server" OnCheckedChanged="RadioButton_CheckedChanged" />
                              <label class="form-check-label me-1">
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
                                        CausesValidation="true" ValidationGroup="ProbarUrlValidation"
                                        CssClass="btn btn-outline-primary" runat="server" />
                        </div>

                        <div class="ms-2 mb-3">
                            <asp:RequiredFieldValidator ErrorMessage="La URL no puede estar vacía para probar." Display="Dynamic"
                                                        ValidationGroup="ProbarUrlValidation" ControlToValidate="UrlImagenTextBox"
                                                        CssClass="validator" runat="server" />
                            <asp:CustomValidator ID="RequiredCustomValidator" ErrorMessage="La URL de la imagen del artículo es obligatoria."
                                                 Display="Dynamic" ControlToValidate="UrlImagenTextBox" CssClass="validator"
                                                 OnServerValidate="RequiredCustomValidator_ServerValidate" ValidateEmptyText="true"
                                                 ValidationGroup="OperationValidationGroup" runat="server" />
                        </div>
                        <%}%>

                        <%-- Imagen local --%>
                        <%if (ImagenPorArchivo)
                        {%>
                        <div class="input-group mb-1">
                            <label class="input-group-text">Imagen local<span class="required-data">*</span></label>
                            <input type="file" class="form-control" id="ImagenLocalInput" accept=".jpg,.png" runat="server" AutoPostBack="true"/>
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
                        </div>
                        <%} %>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


            <h3>Previsualización de imagen</h3>
  
            <hr />

            <%-- Renderizado de imagen --%>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <div class="imgs-form-container mb-4">

                        <%if (EsEdicion)
                          { %>
                        <div class="img-form-container rounded-3 border shadow">
                            <label class="form-label h4">Imagen actual</label>
                            <hr class="hr-product-proyect" />
                            <div class="img-container justify-content-center">
                                <asp:Image ID="ActualImagen" CssClass="img-fluid"
                                            AlternateText="No se pudo establecer la ruta de imagen del artículo" runat="server" />
                            </div>
                        </div>
                        <%} %>

                        <div class="img-form-container rounded-3 border shadow">
                            <label class="form-label h4">Imagen nueva</label>
                            <hr class="hr-product-proyect" />
                            <div class="img-container justify-content-center">
                                <asp:Image ID="NuevaImagen" CssClass="img-fluid"
                                           AlternateText="No se pudo establecer la ruta de imagen del artículo" runat="server" />
                            </div>
                        </div>

                    </div>

                    <%if (DebeConfirmarEliminacion)
                        {%>

                    <div class="rounded-3 border border-danger px-3 pt-3 pb-4 mb-3">
                        <label class="form-label">Para eliminar, escribir el código de artículo:</label>
                        <div class="input-group">
                            <asp:TextBox ID="CodigoEliminarTextBox" CssClass="form-control border-danger" runat="server"></asp:TextBox>
                            <asp:Button ID="ConfirmarEliminacionButton" Text="Confirmar eliminación" CssClass="btn btn-danger"
                                        CausesValidation="true" ValidationGroup="EliminarValidationGroup"
                                        OnClick="ConfirmarEliminacionButton_Click" runat="server" />
                            <asp:Button ID="CancelarEliminacionButton" Text="Cancelar" CssClass="btn btn-outline-primary"
                                        OnClick="CancelarEliminacionButton_Click" runat="server" />
                        </div>

                        <div class="pt-2 ms-2">
                            <asp:RequiredFieldValidator ErrorMessage="Para confirmar eliminación debe escribir el código del artículo actual."
                                                        ControlToValidate="CodigoEliminarTextBox" CssClass="validator" Display="Dynamic"
                                                        ValidationGroup="EliminarValidationGroup" runat="server" />
                            <asp:CustomValidator ID="CodigoEliminarCustomValidator" ErrorMessage="El código de artículo no corresponde al esperado."
                                                 ControlToValidate="CodigoEliminarTextBox" CssClass="validator" Display="Dynamic"
                                                 OnServerValidate="CodigoEliminarCustomValidator_ServerValidate"
                                                 ValidationGroup="EliminarValidationGroup" runat="server" />
                        </div>
                    </div>

                    <%} %>

                    <%if (!DebeConfirmarEliminacion)
                        {%>
                    <div class="buttons-container pt-3 mb-3">

                            <%if (!EsEdicion)
                                {%>
                            <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                                    runat="server" onserverclick="AñadirArticuloButton_ServerClick" id="AñadirArticuloButton"
                                    class="btn btn-success pt-2 pb-2 ms-2 me-2">Añadir artículo
                                <i class="bi bi-plus-circle"></i>
                            </button>
                            <%}%>

                            <%if (EsEdicion && !DebeConfirmarEliminacion)
                                {%>
                            <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                                    runat="server" onserverclick="EditarArticuloButton_ServerClick" id="EditarArticuloButton"
                                    class="btn btn-warning pt-2 pb-2 ms-2 me-2">Editar artículo
                                <i class="bi bi-pencil-square"></i>
                            </button>

                            <button type="button"
                                    runat="server" onserverclick="EliminarArticuloButton_ServerClick" id="EliminarArticuloButton"
                                    class="btn btn-danger pt-2 pb-2 ms-2 me-2">Eliminar artículo
                                <i class="bi bi-trash3"></i>
                            </button>

                            <%}%>

                    </div>
                    <%}%>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>

    </section>

</asp:Content>
