<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compra.aspx.cs" Inherits="FerreteriaPro1.Compra" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Ventas</h3>
    <div class="row">
        <div class="col-md-12">
            <div class="input-group">
                <span class="input-group-addon">Fecha</span>
                <asp:TextBox ID="txtFecha" runat="server" placeholder="Ingrese fecha" CssClass="form-control" Enabled="false"></asp:TextBox>
            </div>
        </div>
    </div>
    <div class="row">        
        <div class="col-md-12">
            <div class="input-group">
                <span class="input-group-addon">Proveedores</span>
                <asp:DropDownList ID="cmbProveedores" runat="server" CssClass="form-control" Width="100%" OnSelectedIndexChanged="cmbProveedores_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-6">
            <div class="input-group">
                <span class="input-group-addon">Artículos</span>
                <asp:DropDownList ID="cmbArticulos" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
        </div>
        <div class="col-md-6">
            <div class="input-group">
                <span class="input-group-addon">Cantidad</span>
                <asp:TextBox ID="txtCantidad" runat="server" placeholder="Ingrese cantidad" CssClass="form-control" TextMode="Number"></asp:TextBox>
                <asp:Button ID="btnAgregarProducto" runat="server" CssClass="btn btn-primary" Text="Agregar" OnClick="btnAgregarProducto_Click" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    <strong><i class="fa fa-filter"></i>&nbsp;Venta de artículos</strong>
                </div>
                <div class="panel-body table-responsive">
                    <asp:GridView ID="dgvDetalleCompra" runat="server" class="table table-bordered table-condensed table-hover" AutoGenerateColumns="False" OnRowDeleting="dgvDetalleCompra_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="#" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblNumero" runat="server"
                                        Text="<%#(dgvDetalleCompra.PageSize * dgvDetalleCompra.PageIndex) + Container.DisplayIndex + 1%>">&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;  
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="center" Width="5%" CssClass="" />
                                <HeaderStyle CssClass=" info text-center" />
                            </asp:TemplateField>                            
                            <asp:BoundField AccessibleHeaderText="ID_PROVEEDOR" DataField="ID_PROVEEDOR" HeaderText="Proveedor">
                                <ItemStyle CssClass="hidden" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="info text-center hidden" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="ID_ARTICULO" DataField="ID_ARTICULO" HeaderText="Código">
                                <ItemStyle CssClass="" HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="info text-center" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="NOMBRE_ARTICULO" DataField="NOMBRE_ARTICULO" HeaderText="Descripción">
                                <ItemStyle CssClass="" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Left" CssClass="info" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="PRECIO_ARTICULO" DataField="PRECIO_ARTICULO" HeaderText="Precio">
                                <ItemStyle CssClass="text-right" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="info text-center" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="CANTIDAD_ARTICULO" DataField="CANTIDAD_ARTICULO" HeaderText="Cantidad">
                                <ItemStyle CssClass="text-right" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" CssClass="info text-center" />
                            </asp:BoundField>
                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="noShowLoader" DeleteText="<i class='glyphicon glyphicon-trash text-danger fa'></i>" AccessibleHeaderText="" HeaderText="Eliminar">
                                <ItemStyle CssClass="" HorizontalAlign="Center" Width="5%" />
                                <HeaderStyle CssClass="info text-center " />
                            </asp:CommandField>

                        </Columns>
                        <PagerStyle CssClass="pagination-ys" />
                    </asp:GridView>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Guardar venta" OnClick="btnGuardar_Click" />
                </div>
            </div>
        </div>
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
