<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioRegistro.aspx.cs" Inherits="CatalogoProductos_Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="section-proyect">

        <div class="register-container">

            <div class="register-elements rounded-3 border shadow">

                <h2 class="text-center title-register">REGISTRO</h2>
                <hr />

                <fieldset class="rounded-3 border fieldset-register shadow">
                    
                    <legend class="legend-form"><span class="legend-title">Información de la cuenta</span></legend>
                    
                    <h4 class="form-label mb-3">Email:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="UsuarioTextBox"
                                 PlaceHolder="ejemplo@ejemplo.com" />
                    <asp:RequiredFieldValidator ErrorMessage="El email es un campo requerido."
                                                ControlToValidate="UsuarioTextBox"
                                                Display="Dynamic" CssClass="validator" runat="server" />
                    <h4 class="form-label mb-3">Contraseña:</h4>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control mb-1" ID="PassTextBox"
                                 PlaceHolder="*********" />
                    <asp:RequiredFieldValidator ErrorMessage="La contraseña es un campo requerido."
                                                ControlToValidate="PassTextBox" Display="Dynamic"
                                                CssClass="validator"  runat="server" />
                    <h4 class="form-label mb-3">Repita la contraseña:</h4>
                    <asp:TextBox runat="server" TextMode="Password" CssClass="form-control mb-1" ID="PassRepetidaTextBox"
                                 PlaceHolder="*********" />
                    <div class="container-validators">
                        <asp:RequiredFieldValidator ErrorMessage="La contraseña es un campo requerido."
                                                    ControlToValidate="PassRepetidaTextBox"
                                                    Display="Dynamic" CssClass="validator" runat="server"/>
                        <asp:CompareValidator ErrorMessage="Ambas contraseñas deben coincidir."
                                              ControlToValidate="PassRepetidaTextBox" ControlToCompare="PassTextBox" 
                                              CssClass="validator" Operator="Equal" Type="String"
                                              Display="Dynamic" runat="server"/>
                    </div>

                </fieldset>

                <fieldset class="rounded-3 border fieldset-register shadow">
                    
                    <legend class="legend-form"><span class="legend-title">Información personal</span></legend>
                    
                    <h4 class="form-label mb-3">Nombre:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="NombreTextBox"
                                 PlaceHolder="Juan Diego" />
                    <asp:RequiredFieldValidator ErrorMessage="El nombre es un campo requerido."
                                                ControlToValidate="NombreTextBox" Display="Dynamic"
                                                CssClass="validator" runat="server" />
                    <h4 class="form-label mb-3">Apellido:</h4>
                    <asp:TextBox runat="server" CssClass="form-control mb-1" ID="ApellidoTextBox"
                                 PlaceHolder="Pérez" />
                    <asp:RequiredFieldValidator ErrorMessage="El apellido es un campo requerido."
                                                ControlToValidate="ApellidoTextBox" Display="Dynamic"
                                                CssClass="validator" runat="server" />

                </fieldset>

                <hr />
                <asp:Button Text="Registrarse" runat="server" CssClass="btn btn-primary btn-lg mb-3 btn-register" />

            </div>

        </div>

    </section>

</asp:Content>
