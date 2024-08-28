<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioRegistro.aspx.cs" Inherits="CatalogoProductos_Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="register-container">

            <div class="register-elements rounded-3 border shadow">

                <h2 class="text-center title-login">REGISTRO</h2>
                <hr />

                <fieldset class="rounded-3 border fieldset-register shadow">
                    
                    <legend class="legend-form"><span class="legend-title">Información personal</span></legend>
                    
                    <h4 class="form-label mb-3">Nombre:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="NombreTextBox"
                                 PlaceHolder="Juan Diego" />
                    <asp:RequiredFieldValidator ID="NombreValidator" ErrorMessage="El nombre es un campo requerido."
                                                ControlToValidate="NombreTextBox" Display="Dynamic"
                                                CssClass="validator" runat="server" />
                    <h4 class="form-label mb-3">Apellido:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="ApellidoTextBox"
                                 PlaceHolder="Pérez" />
                    <asp:RequiredFieldValidator ID="ApellidoValidator" ErrorMessage="El apellido es un campo requerido."
                                                ControlToValidate="ApellidoTextBox" Display="Dynamic"
                                                CssClass="validator" runat="server" />

                </fieldset>

                <fieldset class="rounded-3 border fieldset-register shadow">
                    
                    <legend class="legend-form"><span class="legend-title">Información de la cuenta</span></legend>
                    
                    <h4 class="form-label mb-3">Email:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="UsuarioTextBox"
                                 PlaceHolder="ejemplo@ejemplo.com" />
                    <asp:RequiredFieldValidator ID="EmailValidator" ErrorMessage="El email es un campo requerido."
                                                ControlToValidate="UsuarioTextBox" Display="Dynamic"
                                                CssClass="validator" runat="server" />
                    <asp:RegularExpressionValidator ID="EmailValidoValidator" ErrorMessage="La información proporcionada no tiene estructura de correo electrónico."
                                                    ControlToValidate="UsuarioTextBox" CssClass="validator" Display="Dynamic"
                                                    ValidationExpression="^[\w\.-]+@[a-zA-Z\d\.-]+\.[a-zA-Z]{2,}$" runat="server" />
                    <asp:CustomValidator ID="UsuarioExistenteCustomValidator" ErrorMessage="El usuario ya existe." ControlToValidate="UsuarioTextBox"
                                         CssClass="validator" OnServerValidate="UsuarioExistenteCustomValidator_ServerValidate" Display="Dynamic" runat="server" />
                    <h4 class="form-label mb-3">Contraseña:</h4>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control mb-1" ID="PassTextBox"
                                 PlaceHolder="*********" />
                    <asp:RequiredFieldValidator ID="PasswordValidator" ErrorMessage="La contraseña es un campo requerido."
                                                ControlToValidate="PassTextBox" Display="Dynamic"
                                                CssClass="validator"  runat="server" />
                    <h4 class="form-label mb-3">Repita la contraseña:</h4>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control mb-1" ID="PassRepetidaTextBox"
                                 PlaceHolder="*********" />
                    <div class="validator-container">
                        <asp:RequiredFieldValidator ID="RepetirPasswordValidator" ErrorMessage="La contraseña es un campo requerido."
                                                    ControlToValidate="PassRepetidaTextBox"
                                                    Display="Dynamic" CssClass="validator" runat="server"/>
                        <asp:CompareValidator ID="PasswordsCoincidentesValidator" ErrorMessage="Ambas contraseñas deben coincidir."
                                              ControlToValidate="PassRepetidaTextBox" ControlToCompare="PassTextBox" 
                                              CssClass="validator" Operator="Equal" Type="String"
                                              Display="Dynamic" runat="server" />
                    </div>

                </fieldset>

                <hr />

                <asp:Button ID="RegistrarseButton" Text="Registrarse" OnClick="RegistrarseButton_Click"
                            CssClass="btn btn-primary btn-lg mb-3 btn-register" runat="server" />

            </div>

        </div>

    </section>

</asp:Content>
