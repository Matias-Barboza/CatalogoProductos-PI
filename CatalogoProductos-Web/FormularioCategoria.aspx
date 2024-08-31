<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioCategoria.aspx.cs" Inherits="CatalogoProductos_Web.FormularioCategoria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect min-height-container justify-content-start">

        <div class="current-data-container rounded-3 border shadow">
    
            <h2 class="text-center">Formulario de Marca</h2>

            <hr />

            <%-- Descripción de categoría --%>
            <div class="mb-3">
        
                <div class="input-group mb-1">
                    <span class="input-group-text">Nombre<span class="required-data"> *</span></span>
                    <asp:TextBox ID="DescripcionTextBox" CssClass="form-control" placeholder="Ej: Audio" runat="server"></asp:TextBox>
                </div>

                <div class="ms-2">
                    <p class="form-text">Tenga en cuenta la información del desplegable donde se indican las categorías existentes.</p>
                </div>

                <div class="ms-2">
                    <asp:RegularExpressionValidator ID="DescripcionLongitudValidator" ErrorMessage="El nombre de la categoría debe tener 1 caracter como mínimo y 50 como máximo." Display="Dynamic"
                                        ControlToValidate="DescripcionTextBox" CssClass="validator" ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑ0-9]{1,50}$"
                                        ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:RequiredFieldValidator ID="DescripcionObligatoriaValidator" ErrorMessage="El nombre de la categoría es obligatoria." Display="Dynamic"
                                                ControlToValidate="DescripcionTextBox" CssClass="validator"
                                                ValidationGroup="OperationValidationGroup" runat="server" />
                    <asp:CustomValidator ID="DescripcionCategoriaRepetidaCustomValidator" ErrorMessage="La categoría ya existe." Display="Dynamic"
                                         ControlToValidate="DescripcionTextBox" CssClass="validator"
                                         ValidationGroup="OperationValidationGroup" OnServerValidate="DescripcionCategoriaRepetidaCustomValidator_ServerValidate" runat="server" />
                </div>

            </div>

            <%-- Categorias existentes --%>
            <div class="mb-3">

                <div class="input-group mb-1">
                    <label class="input-group-text">Categorias existentes</label>
                    <asp:DropDownList ID="CategoriasExistentesDropDownList" CssClass="form-select" runat="server"></asp:DropDownList>
                </div>

            </div>

            <div class="buttons-container pt-3 mb-3">

                <%if (!EsEdicion)
                    {%>
                <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                        runat="server" onserverclick="AñadirCategoriaButton_ServerClick" id="AñadirCategoriaButton"
                        class="btn btn-success pt-2 pb-2 ms-2 me-2">Añadir categoría
                    <i class="bi bi-plus-circle"></i>
                </button>
                <%}%>

                <%if (EsEdicion)
                    {%>
                <button type="button" causesvalidation="true" validationgroup="OperationValidationGroup"
                        runat="server" onserverclick="EditarCategoriaButton_ServerClick" id="EditarCategoriaButton"
                        class="btn btn-warning pt-2 pb-2 ms-2 me-2">Editar categoría
                    <i class="bi bi-pencil-square"></i>
                </button>
                <%}%>

            </div>

        </div>

    </section>

</asp:Content>
