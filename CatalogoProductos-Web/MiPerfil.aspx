<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="CatalogoProductos_Web.MiPerfil" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect products-container">

        <div class="rounded-3 shadow">
            <span class="badge-detail-proyect py-4">Datos de Mi perfil</span>
            <div class="d-flex">
                <div class="img-detail-container flex-column bg-light">
                    <div class="d-flex justify-content-center mb-3 p-4 rounded-3" style="max-height:400px">
                        <asp:Image ID="ImagenActualImage" CssClass="img-fluid" ImageUrl="imageurl" runat="server" />
                    </div>
                    <%if (DebeConfirmarEdicion)
                      {%>
                    <div class="container-fluid py-2">
                        <input type="file" class="form-control" id="ImagenPerfilInput" accept=".jpg,.png" runat="server" />
                    </div>
                    <%} %>
                    <asp:CustomValidator ID="TipoArchivoCustomValidator" OnServerValidate="TipoArchivoCustomValidator_ServerValidate"
                        CssClass="validator ms-2" ErrorMessage="El tipo de archivo elegido no es válido." Display="Dynamic"
                        ControlToValidate="ImagenPerfilInput" ValidationGroup="EdicionValidationGroup" ValidateEmptyText="false" runat="server" />
                </div>
                <div class="info-detail-container p-4">
                    <div class="mb-3">
                        <label class="form-label">Email:</label>
                        <asp:TextBox ID="EmailTextBox" CssClass="form-control" Enabled="false" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Nombre:</label>
                        <asp:TextBox ID="NombreActualTextBox" CssClass="form-control" Enabled="false" runat="server" />
                        <asp:RequiredFieldValidator CssClass="validator ms-2" Display="Dynamic" ValidationGroup="EdicionValidationGroup"
                                                    ErrorMessage="El nombre no puede estar vacío." ControlToValidate="NombreActualTextBox" runat="server" />
                    </div>
                    <div class="mb-3">
                        <label class="form-label">Apellido:</label>
                        <asp:TextBox ID="ApellidoActualTextBox" CssClass="form-control" Enabled="false" runat="server" />
                        <asp:RequiredFieldValidator CssClass="validator ms-2" Display="Dynamic" ValidationGroup="EdicionValidationGroup"
                            ErrorMessage="El apellido no puede estar vacío." ControlToValidate="ApellidoActualTextBox" runat="server" />
                    </div>
                </div>
            </div>
            <div class="d-flex justify-content-center mb-3 bg-light">
                <%if (!DebeConfirmarEdicion) 
                {%>
                <button type="button" onserverclick="EditarUsuarioButton_ServerClick" id="EditarUsuarioButton"
                        class="btn btn-warning pt-2 pb-2 ms-2 me-2" runat="server" >Editar información
                    <i class="bi bi-pencil-square"></i>
                </button>
                <%}
                  else 
                  {%>
                <asp:Button ID="QuitarFotoButton" CssClass="btn btn-secondary pt-2 pb-2 ms-2 me-2" OnClick="QuitarFotoButton_Click"
                            Text="Quitar foto de perfil" runat="server" />
                <button type="button" onserverclick="ConfirmarEdicionButton_ServerClick" id="ConfirmarEdicionButton"
                        class="btn btn-success pt-2 pb-2 ms-2 me-2" runat="server" validationGroup="EdicionValidationGroup" causesvalidation="true" >
                    Confirmar edición
                    <i class="bi bi-check-circle"></i>
                </button>
                <button type="button" onserverclick="CancelarEdicionButton_ServerClick" id="CancelarEdicionButton"
                        class="btn btn-danger pt-2 pb-2 ms-2 me-2" runat="server" >Cancelar edición
                    <i class="bi bi-x-circle"></i>
                </button>
                <%}%>
            </div>
        </div>

    </section>

</asp:Content>
