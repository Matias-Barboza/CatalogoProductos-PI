<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CatalogoProductos_Web.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="products-container">
        <asp:Repeater runat="server" ID="RepeaterProductos">
            <ItemTemplate>
                <asp:LinkButton runat="server" CssClass="link-button-product">
                <div class="one-product-container border rounded-3 mb-3">
                    <div class="image-product-container border-image-product">
                        <img src="<%#Eval("ImagenUrl").ToString() == "" ?
                                    "https://static.vecteezy.com/system/resources/previews/007/126/739/original/question-mark-icon-free-vector.jpg" : Eval("ImagenUrl").ToString() %>"
                            class="img-fluid img-proyect" alt="Imagen producto"/>
                    </div>
                    <div class="info-product-container">
                        <span class="h5 badge-proyect"><%#Eval("Categoria.Descripcion")%></span>
                        <hr />
                        <h6><%#Eval("Nombre")%></h6>
                        <p class=""><%#Eval("Precio")%>$</p>
                    </div>
                </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </section>

</asp:Content>
