﻿Public Class VentasResumenForm

    Private Sub VentasResumenForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ventas As New DataTable
        ventas = CAPA_LOGICA.Venta.Select_Ventas
        DGV_Ventas.DataSource = ventas
        Dim ids As New List(Of Integer)
        For Each row As DataRow In ventas.Rows
            ids.Add(row.Item("ID"))
        Next
        CMBX_Ventas.DataSource = ids

        Dim productosVentas As New DataTable
        productosVentas.Columns.Add("IDProducto")
        productosVentas.Columns.Add("Nombre")
        productosVentas.Columns.Add("Total Vendido")
        productosVentas.Columns.Add("Total Recaudado")

        Dim productos As New DataTable
        Dim items As New DataTable
        productos = CAPA_LOGICA.Producto.Select_Productos

        For Each row As DataRow In productos.Rows
            Dim rowVentas As DataRow = productosVentas.NewRow
            rowVentas.Item("IDProducto") = row.Item("ID")
            rowVentas.Item("Nombre") = row.Item("Nombre")
            productosVentas.Rows.Add(rowVentas)
            items = CAPA_LOGICA.VentaItem.GetRelatedToProd_Items(row.Item("ID"))
            DGV_VentasPorMes.DataSource = items
            Dim totalVendido As Integer = 0
            Dim totalRecaudado As Double = 0
            For Each itemVentaRow As DataRow In items.Rows
                totalVendido = totalVendido + itemVentaRow.Item("Cantidad")
                totalRecaudado = totalRecaudado + itemVentaRow.Item("PrecioTotal")
            Next
            rowVentas.Item("Total Vendido") = totalVendido
            rowVentas.Item("Total Recaudado") = totalRecaudado
        Next

        DGV_ProductosReporte.DataSource = productosVentas
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub CMBX_Ventas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CMBX_Ventas.SelectedIndexChanged
        Try
            Dim items As New DataTable
            items = CAPA_LOGICA.VentaItem.GetRelatedTo_Items(CInt(CMBX_Ventas.Text))
            items.Columns.Add("Nombre")
            For Each row As DataRow In items.Rows
                row.Item("Nombre") = CAPA_LOGICA.Producto.SelectByID_Productos(row.Item("IDProducto")).Item("Nombre")
            Next
            items.Columns.Remove("ID")
            items.Columns.Remove("IDProducto")
            items.Columns.Remove("IDVenta")
            DGV_Items.DataSource = items
        Catch ex As Exception
            MsgBox("Error al cargar los items de la venta seleccionada")
        End Try
    End Sub

    Private Sub BTN_Eliminar_Click(sender As Object, e As EventArgs) Handles BTN_Eliminar.Click
        CAPA_LOGICA.VentaItem.DeleteRelatedTo_Items(CInt(CMBX_Ventas.Text()))
        CAPA_LOGICA.Venta.Delete_Venta(CInt(CMBX_Ventas.Text()))
        Dim ventas As New DataTable
        ventas = CAPA_LOGICA.Venta.Select_Ventas
        DGV_Ventas.DataSource = ventas
        Dim ids As New List(Of Integer)
        For Each row As DataRow In ventas.Rows
            ids.Add(row.Item("ID"))
        Next
        CMBX_Ventas.DataSource = ids
        DGV_Items.DataSource = Nothing
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles BTN_Actualizar.Click
        Dim productosVentas As New DataTable
        productosVentas.Columns.Add("IDProducto")
        productosVentas.Columns.Add("Nombre")
        productosVentas.Columns.Add("Total Vendido")
        productosVentas.Columns.Add("Total Recaudado")

        Dim productos As New DataTable
        Dim items As New DataTable
        productos = CAPA_LOGICA.Producto.Select_Productos

        For Each row As DataRow In productos.Rows
            Dim rowVentas As DataRow = productosVentas.NewRow
            rowVentas.Item("IDProducto") = row.Item("ID")
            rowVentas.Item("Nombre") = row.Item("Nombre")
            productosVentas.Rows.Add(rowVentas)
            items = CAPA_LOGICA.VentaItem.GetRelatedToProd_Items(row.Item("ID"))
            DGV_VentasPorMes.DataSource = items
            Dim totalVendido As Integer = 0
            Dim totalRecaudado As Double = 0
            For Each itemVentaRow As DataRow In items.Rows
                totalVendido = totalVendido + itemVentaRow.Item("Cantidad")
                totalRecaudado = totalRecaudado + itemVentaRow.Item("PrecioTotal")
            Next
            rowVentas.Item("Total Vendido") = totalVendido
            rowVentas.Item("Total Recaudado") = totalRecaudado
        Next

        DGV_ProductosReporte.DataSource = productosVentas
    End Sub
End Class