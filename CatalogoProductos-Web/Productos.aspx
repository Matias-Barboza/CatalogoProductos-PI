<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="CatalogoProductos_Web.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="products-container">
        <asp:Repeater runat="server" ID="RepeaterProductos">
            <ItemTemplate>
                <asp:LinkButton runat="server" CssClass="link-button-product">
                <div class="one-product-container rounded-3 mb-4">
                    <div class="image-product-container border-image-product">
                        <img src="<%#Eval("ImagenUrl").ToString() == "" ?
                                    "https://pngimg.com/uploads/box/box_PNG137.png" : Eval("ImagenUrl").ToString() %>"
                            class="img-fluid img-proyect" alt="Imagen del producto"/>
                    </div>
                    <div class="info-product-container">
                        <span class="h5 badge-proyect"><%#Eval("Categoria.Descripcion")%></span>
                        <hr class="hr-product-proyect"/>
                        <h6 class="h6-product-proyect"><%#Eval("Nombre")%></h6>
                        <p class="p-product-proyect"><%#((decimal) Eval("Precio")).ToString("C")%></p>
                    </div>
                </div>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:Repeater>
    </section>

</asp:Content>
