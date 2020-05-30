<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ArticulosListado.aspx.cs" Inherits="FerreteriaPro1.ArticulosListado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Listado de artículos</h3>
     <div class="row">        
        <div class="col-lg-12">
            <input type="button" class="btn btn-primary" value="Agregar" onclick="location.href = 'Articulos.aspx';" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="table-responsive">
                <asp:GridView ID="dgvListado" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-bordered table-hover dataTable no-footer" role="grid">
                    <Columns>
                        <asp:BoundField AccessibleHeaderText="id_articulo" DataField="id_articulo" HeaderText="id articulo">
                            <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                            <HeaderStyle CssClass="text-center info hidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField AccessibleHeaderText="nombre_articulo" DataTextField="nombre_articulo" HeaderText="Nombre del articulo" DataNavigateUrlFields="id_articulo" DataNavigateUrlFormatString="Articulos.aspx?ida={0}&op=4">
                            <HeaderStyle CssClass="info" HorizontalAlign="Left" />
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:HyperLinkField>
                        <asp:BoundField AccessibleHeaderText="descripcion_articulo" DataField="descripcion_articulo" HeaderText="Direccion del articulo">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="unidad_medida" DataField="unidad_medida" HeaderText="Unidad medida">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="precio_articulo" DataField="precio_articulo" HeaderText="precio articulo">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="stock_articulo" DataField="stock_articulo" HeaderText="stock del articulo">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="nombre_proveedor" DataField="nombre_proveedor" HeaderText="Nombre proveedor">
                            <ItemStyle HorizontalAlign="Center" CssClass="" />
                            <HeaderStyle CssClass="text-center info" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle CssClass="pagination-ys" />
                    <PagerSettings Mode="NumericFirstLast" />
                </asp:GridView>
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
