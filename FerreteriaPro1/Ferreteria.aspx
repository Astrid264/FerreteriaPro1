<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ferreteria.aspx.cs" Inherits="FerreteriaPro1.Ferreteria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Ferreterías</h3>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                <asp:TextBox runat="server" ID="txtNombres" CssClass="form-control" placeholder="nombre completo"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-phone"></i></span>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" placeholder="telefono" TextMode="Number"></asp:TextBox>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
     <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <div class="input-group">
                <span class="input-group-addon"><i class="glyphicon glyphicon-education"></i></span>
                <asp:DropDownList runat="server" ID="cmbTipoFerreteria" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="col-lg-3"></div>
    </div>
     <div class="row">
        <div class="col-lg-3"></div>
        <div class="col-lg-6">
            <asp:Button runat="server" ID="btnGuardar" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click"></asp:Button>
        </div>
        <div class="col-lg-3"></div>
    </div>
    <div class="row">

        <% 
            if (_MensajeError != "")
            {
        %>
        <div class="alert alert-danger">
            <%=_MensajeError %>
        </div>
        <%
            }
            if (_MensajeSatisfactorio != "")
            {
        %>
        <div class="alert alert-success">
            <%=_MensajeSatisfactorio %>
        </div>
        <%
            }

        %>
    </div>
</asp:Content>