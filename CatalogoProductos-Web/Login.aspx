<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CatalogoProductos_Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="login-container">
            <div class="login-elements rounded-3 border shadow">
                <h2 class="text-center title-login">LOGIN</h2>
                <hr />
                <h4 class="form-label mb-3">Usuario:</h4>
                <asp:TextBox runat="server" CssClass="form-control mb-2" ID="UsuarioTextBox"
                                            PlaceHolder="ejemplo@ejemplo.com"/>
                <asp:RequiredFieldValidator ErrorMessage="El usuario es obligatorio."
                                            ControlToValidate="UsuarioTextBox" Display="Dynamic"
                                            ID="UsuarioValidatorLogin" CssClass="validator"
                                            runat="server" />
                <h4 class="form-label mb-3">Contraseña:</h4>
                <asp:TextBox runat="server" TextMode="Password" CssClass="form-control mb-2" ID="PassTextBox"
                                            PlaceHolder="*********"/>
                <asp:RequiredFieldValidator ErrorMessage="La contraseña no puede estar vacía."
                                            ControlToValidate="PassTextBox" Display="Dynamic"
                                            ID="PassValidatorLogin" CssClass="validator"
                                            runat="server" />
                <hr />
                <asp:Button Text="Ingresar" runat="server" CssClass="btn btn-primary btn-lg mb-3 btn-login" />
                <p class="text-center">¿No estás registrado? Registrate <a href="FormularioRegistro.aspx">aquí</a>.</p>
            </div>
        </div>

    </section>

</asp:Content>
